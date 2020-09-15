using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaVan.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public String Motorista { get; set; }
        public int Capacidade { get; set; }

        public List<Viagem> Viagens { get; set; }
        public List<Localizacao> Localizacoes { get; set; }
    }
}
