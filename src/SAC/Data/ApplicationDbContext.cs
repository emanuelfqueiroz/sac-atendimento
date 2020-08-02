using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SAC.Models;

namespace SAC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var protocolo = modelBuilder.Entity<Protocolo>();
            protocolo.HasIndex(a => a.DigitroId).IsUnique(); ;

            var cliente = modelBuilder.Entity<Cliente>();
            cliente.HasIndex(a => a.Nome);
            cliente.HasIndex(a => a.Email);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Protocolo> Protocolos { get; set; }

        public DbSet<SAC.Models.Motivo> Motivo { get; set; }

        public DbSet<SAC.Models.Assunto> Assunto { get; set; }

        public DbSet<SAC.Models.Setor> Setor { get; set; }

        public DbSet<SAC.Models.Cliente> Cliente { get; set; }
    }
}
