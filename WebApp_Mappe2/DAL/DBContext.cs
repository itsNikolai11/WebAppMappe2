using Microsoft.EntityFrameworkCore;
using System;


namespace WebApp_Mappe2.DAL
{
    public class Ruter
    {
        public int Id { get; set; }

        virtual public Destinasjoner FraDestinasjon { get; set; }
        virtual public Destinasjoner TilDestinasjon { get; set; }
        public int PrisBarn { get; set; }
        public int PrisVoksen { get; set; }

    }
    public class Avganger
    {
        public int Id { get; set; }
        public DateTime AvgangTid { get; set; }
        virtual public Ruter RuteNr { get; set; }
        virtual public Ruter FraDestinasjon { get; set; }
        virtual public Ruter TilDestinasjon { get; set; }
    }

    public class Destinasjoner
    {
        public int Id { get; set; }
        public string Sted { get; set; }
        public string Land { get; set; }
    }
    public class Brukere
    {
        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
    }
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Ruter> Ruter { get; set; }
        public DbSet<Avganger> Avganger { get; set; }
        public DbSet<Destinasjoner> Destinasjoner { get; set; }
        public DbSet<Brukere> Brukere { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
