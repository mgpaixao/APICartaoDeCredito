using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICartaoDeCredito.Models;
using Microsoft.EntityFrameworkCore;

namespace APICartaoDeCredito.DataBase
{

    public class ApiContex : DbContext

    {
        public ApiContex(DbContextOptions<ApiContex> options) : base(options)
        {
            
        }
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ClienteDados>()
                .HasAlternateKey(c => c.CardNumber);
                }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ClienteDados>()
                .HasKey(c => c.CardNumber);
        }
        public DbSet<ClienteDados> ClientesDb { get; set; } //Criando um metodo acessivel para nosso banco de dados.
    }
}
