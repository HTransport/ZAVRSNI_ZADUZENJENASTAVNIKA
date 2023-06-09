using System;
using System.Data.Entity;
using System.Linq;

namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Your context has been configured to use a 'ApplicationDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ZAVRSNI_ZADUZENJENASTAVNIKA.Models.ApplicationDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ApplicationDbContext' 
        // connection string in the application configuration file.
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Nastavnik> Nastavnici { get; set; }
        public virtual DbSet<SkolskaGodina> SkolskeGodine { get; set; }
        public virtual DbSet<Predmet> Predmeti { get; set; }
        public virtual DbSet<Razred> Razredi { get; set; }
        public virtual DbSet<Zamjena> Zamjene { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}