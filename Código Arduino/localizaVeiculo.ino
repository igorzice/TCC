#include <Arduino.h>
#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClientSecure.h>

#include <SoftwareSerial.h>
#include <TinyGPS++.h>

#include <ArduinoJson.h>

//PLACA nodeMCU -- mapeamento
#define D0 16
#define D1 5
#define D2 4    //RX (receptor do GPS)
#define D3 0    //led verde
#define D4 2    //led vermelho
#define D5 14
#define D6 12
#define D7 13
#define D8 15

#define veiculoId 1      //identificação do veículo
#define GPS_Serial 9600

TinyGPSPlus gps;

SoftwareSerial gpsSerial(D2, D1);

class Localizacao {
  public:
    float latitude = -22.429855; 
    float longitude = -45.448155;
    String dataHora = String("1990-01-01T00:00:00");
};

const char* URI = "https://localiza-veiculo.azurewebsites.net/api/localizacoes";

void setup() {
  pinMode(D3, OUTPUT); //led verde
  pinMode(D4, OUTPUT); //led vermelho

  Serial.begin(GPS_Serial);      // configura monitor serial 115200 Bps
  gpsSerial.begin(GPS_Serial);
  Serial.println("entrou no setup");
  //WiFi.disconnect();         // desconecta rede WIFI
  WiFi.mode(WIFI_STA);       // configura rede no modo estacao
  delay(100);                // atraso de 100 milisegundos
}

void loop() {
  String ssid = identificarRedeAberta();  //Retorna o nome da rede aberta com melhor sinal

  if (ssid != "") {  //se encontrou alguma rede aberta
    conectaRedeWifi(ssid);   //Conecta à rede idenfificada
    while (WiFi.status() == WL_CONNECTED) {
      Localizacao* dados = obterDadosGPS();
      if (dados != NULL) {
        transmitir(*dados, ssid);
      } else {
        Serial.println("Nenhum sinal de GPS válido.");
      }
      //delay(1000);
      piscarLedVermelho(); //já inclui um delay de 1 segundo
    }
    ledVermelho(LOW); //não conectado
    ledVerde(LOW); //reseta led verde para desligado
    Serial.println("A conexão foi perdida!");
  }
  delay(100);
}

/*--- VERIFICA REDES WIFI ABERTAS --- (retorna ssid da rede ou vazio)*/
String identificarRedeAberta() {
  Serial.println("Procurando redes WiFi...");

  int countWifi = WiFi.scanNetworks(); //Retorna a quantidade de redes wifi disponíveis
  int openWifi = 0; //Variável para contar quantas redes abertas foram encontradas

  //Exibe as redes wifi encontradas
  if (countWifi > 0) {
    Serial.println(String(countWifi) + " redes encontradas.\n");
    //delay(500);
    for (int i = 0; i < countWifi; ++i) {
      imprimeDadosWifi(i);
      if (WiFi.encryptionType(i) == ENC_TYPE_NONE) {
        openWifi++;
      }
    }
  } else {
    Serial.println("Nenhuma rede encontrada.");
    return "";
  }

  //Exibe os dados da rede wifi aberta com melhor sinal
  if (openWifi > 0) {
    for (int i = 0; i < countWifi; ++i) {
      if (WiFi.encryptionType(i) == ENC_TYPE_NONE) {
        Serial.println("\nRede aberta com melhor sinal:");
        imprimeDadosWifi(i);
        return WiFi.SSID(i);
      }
    }
  } else {
    Serial.println("\nNenhuma rede aberta encontrada.\n");
    return "";
  }
}

/*--- EFETUA CONEXÃO COM REDE WIFI ---*/
void conectaRedeWifi(String ssid) {
  char wifiSSID[50];
  ssid.toCharArray(wifiSSID, 50);

  //WiFi.disconnect();    //Fecha conexão aberta
  WiFi.persistent(false);
  WiFi.disconnect(true);
  WiFi.begin(wifiSSID); //Efetua a conexão com a rede wifi aberta

  Serial.print("Conectando no WiFi " + ssid + ".");
  int tentativas = 0;

  while (WiFi.status() != WL_CONNECTED) {
    Serial.print(".");
    tentativas++;
    if (tentativas == 5) break; //Mantém aguardando conexão por 5 segundos
    delay(1000);
  }

  if (WiFi.status() == WL_CONNECTED) {
    Serial.println("\nWiFi conectado!\n");
  } else {
    Serial.println("\nNao foi possivel conectar.\n");
  }

}

void imprimeDadosWifi(int i) {
  Serial.print(i + 1);
  Serial.print(": ");
  Serial.print(WiFi.SSID(i));
  Serial.print("\t(");
  Serial.print(WiFi.RSSI(i));
  Serial.print(")\t");
  //Serial.print("\tMAC:");
  //Serial.print(WiFi.BSSIDstr(i));
  //Serial.print("\tCANAL: ");
  //Serial.println(WiFi.channel(i));
  Serial.println((WiFi.encryptionType(i) == ENC_TYPE_NONE) ? " Aberta" : " Fechada");
  delay(10);
}

Localizacao* obterDadosGPS() {
  Localizacao* dados = NULL;
  while (gpsSerial.available() > 0) {
    if (gps.encode(gpsSerial.read()) && gps.location.isValid()) {
      imprimeDadosGPS();
      dados = new Localizacao();
      dados->latitude = gps.location.lat();
      dados->longitude = gps.location.lng();
      dados->dataHora = formataDataHora();
      Serial.println("Dados Obtidos: Lat:" + String(dados->latitude) + "  Lon:" + String(dados->longitude) + "  DataHora:" + String(dados->dataHora));
    } else {
      //para imprimir os dados brutos recebidos do GPS
      //char c = gpsSerial.read();
      //Serial.write(c);
    }
  }

  if (dados == NULL) {
    dados = simularGPS();
  }

  return dados;
}

Localizacao* simularGPS() {
  return new Localizacao(); //pegará o valor padrão definido na classe (usar para debug)
}

void imprimeDadosGPS() {
  Serial.print(F("Location: "));
  if (gps.location.isValid()) {
    Serial.print(gps.location.lat(), 6);
    Serial.print(F(","));
    Serial.print(gps.location.lng(), 6);
  } else {
    Serial.print(F("INVALID"));
  }

  Serial.print(F("  Date/Time: "));
  if (gps.date.isValid()) {
    Serial.print(gps.date.month());
    Serial.print(F("/"));
    Serial.print(gps.date.day());
    Serial.print(F("/"));
    Serial.print(gps.date.year());
  } else {
    Serial.print(F("INVALID"));
  }

  Serial.print(F(" "));
  if (gps.time.isValid()) {
    if (gps.time.hour() < 10) Serial.print(F("0"));
    Serial.print(gps.time.hour());
    Serial.print(F(":"));
    if (gps.time.minute() < 10) Serial.print(F("0"));
    Serial.print(gps.time.minute());
    Serial.print(F(":"));
    if (gps.time.second() < 10) Serial.print(F("0"));
    Serial.print(gps.time.second());
    Serial.print(F("."));
    if (gps.time.centisecond() < 10) Serial.print(F("0"));
    Serial.print(gps.time.centisecond());
  } else {
    Serial.print(F("INVALID"));
  }
  Serial.print("  Satélites: ");
  Serial.println(gps.satellites.value());
}

String formataDataHora() {
  if (gps.date.isValid() && gps.time.isValid() && gps.date.year()>=2020) {
    String ano = String(gps.date.year());
    String mes = String(gps.date.month());
    String dia = String (gps.date.day());
    String hora = String(gps.time.hour());
    String minuto = String(gps.time.minute());
    String segundo = String(gps.time.second());
    return ano + "-" + mes + "-" + dia + "T" + hora + ":" + minuto + ":" + segundo;
  }
  return "1990-01-01T00:00:00";
}

void transmitir(Localizacao dados, String ssid) {
  Serial.println("Enviando dados para o servidor...");

  const int capacity = JSON_OBJECT_SIZE(5);
  StaticJsonDocument<capacity> doc;
  //JsonObject obj = doc.as<JsonObject>();
  doc["latitude"] = dados.latitude;
  doc["longitude"] = dados.longitude;
  doc["dataHora"] = dados.dataHora.c_str();
  doc["veiculoId"] = veiculoId;
  //String ssidHora = String(ssid + obterDatahora());
  doc["ssid"] = ssid.c_str();

  // Serialize JSON document
  String json = "";
  serializeJson(doc, json);
  Serial.println("Json: " + json);

  std::unique_ptr<BearSSL::WiFiClientSecure>client(new BearSSL::WiFiClientSecure);
  client->setInsecure();
  HTTPClient http;

  http.begin(*client, "https://localiza-veiculo.azurewebsites.net/api/localizacoes");

  // Requisição POST http para envio de dados em json:
  http.addHeader("Content-Type", "application/json");
  int codigo = http.POST(json);

  if (codigo == 201) { //transmitido com sucesso ao servidor
    Serial.println("Dados enviados com sucesso!");
    Serial.println(http.getString());
    piscarLedVerde();
  } else {
    Serial.print("Erro ao transmitir (código de resposta: " + String(codigo) + ")");
    Serial.println();
    ledVerde(LOW);
  }

}

void ledVerde(int SINAL) {
  digitalWrite(D3, SINAL);
}

void ledVermelho(int SINAL) {
  digitalWrite(D4, SINAL);
}

void piscarLedVermelho() {
  // pisca 10 vezes durante 1 segundo
  for (int i = 0; i < 10; i++) {
    ledVermelho(HIGH);
    delay(100);
    ledVermelho(LOW);
    delay(100);
  }
}

void piscarLedVerde() {
  // pisca 10 vezes durante 1 segundo
  for (int i = 0; i < 10; i++) {
    ledVerde(HIGH);
    delay(100);
    ledVerde(LOW);
    delay(100);
  }
}
