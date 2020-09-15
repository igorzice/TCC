using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaVan.Models
{
    public class Itinerario
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public List<Viagem> Viagens { get; set; }
    }
}
