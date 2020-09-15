using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaVan.Models
{
    public class Viagem
    {
        public int Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public bool Finalizada { get; set; }
        public int VeiculoId { get; set; }
        public int ItinerarioId { get; set; }

        public Veiculo Veiculo { get; set; }
        public Itinerario Itinerario { get; set; }
    }
}
