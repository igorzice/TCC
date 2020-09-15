using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LocalizaVan.Models;

namespace LocalizaVan.Models { 

    public class RastreioContext : DbContext
    {
    public RastreioContext(DbContextOptions<RastreioContext> options) : base(options)
    {
    }

    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Localizacao> Localizacoes { get; set; }
    public DbSet<Viagem> Viagens { get; set; }
    public DbSet<Itinerario> Itinerarios { get; set; }
    }
}
