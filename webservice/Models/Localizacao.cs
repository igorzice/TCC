using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaVan.Models
{
    public class Localizacao
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DataHora { get; set; }
        public int VeiculoId { get; set; }

        public Veiculo Veiculo { get; set; }
    }
}
