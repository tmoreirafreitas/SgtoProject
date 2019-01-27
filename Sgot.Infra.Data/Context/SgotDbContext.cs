using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sgot.Domain.Entities;
using Sgot.Infra.Data.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sgot.Infra.Data.Context
{
    public class SgotDbContext : DbContext
    {        
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<Lente> Lentes { get; set; }
        public DbSet<Oculos> Oculos { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }        

        public SgotDbContext()
        {            
            //Database.EnsureCreated();
        }

        public SgotDbContext(DbContextOptions<SgotDbContext> options) : base(options)
        {
            //Database.EnsureCreated();   // Create the database
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");                                    
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new FaturaMap());
            modelBuilder.ApplyConfiguration(new LenteMap());
            modelBuilder.ApplyConfiguration(new OculosMap());
            modelBuilder.ApplyConfiguration(new ParcelaMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());
            base.OnModelCreating(modelBuilder);            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder
                .UseNpgsql(config.GetConnectionString("DefaultConnection"));
        }             
        
    }
}