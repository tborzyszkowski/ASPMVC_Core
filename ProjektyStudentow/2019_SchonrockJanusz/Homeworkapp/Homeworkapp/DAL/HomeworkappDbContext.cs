using Homeworkapp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Homeworkapp.DAL
{
    public class HomeworkappDbContext : DbContext
    {
        public HomeworkappDbContext() : base("HomeworkappDbContext") { }

        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<Zjazd> Zjazdy { get; set; }
        public DbSet<Zadanie> Zadania { get; set; }
        public DbSet<Kolokwium> Kolokwia { get; set; }
        public DbSet<Egzamin> Egzaminy { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}