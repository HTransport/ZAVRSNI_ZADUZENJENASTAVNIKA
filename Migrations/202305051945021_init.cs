namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nastavniks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        OIB = c.Int(nullable: false),
                        Kategorija = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.OIB, unique: true);
            
            CreateTable(
                "dbo.Predmets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Kategorija = c.String(),
                        NastavnikId = c.Int(nullable: false),
                        RazredId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nastavniks", t => t.NastavnikId, cascadeDelete: true)
                .ForeignKey("dbo.Razreds", t => t.RazredId, cascadeDelete: true)
                .Index(t => t.NastavnikId)
                .Index(t => t.RazredId);
            
            CreateTable(
                "dbo.Razreds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Razina = c.Int(nullable: false),
                        Oznaka = c.String(),
                        SkolskaGodinaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SkolskaGodinas", t => t.SkolskaGodinaId, cascadeDelete: true)
                .Index(t => t.SkolskaGodinaId);
            
            CreateTable(
                "dbo.SkolskaGodinas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Godina = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Godina, unique: true);
            
            CreateTable(
                "dbo.Zamjenas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumOd = c.DateTime(nullable: false),
                        DatumDo = c.DateTime(nullable: false),
                        NastavnikId = c.Int(nullable: false),
                        PredmetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nastavniks", t => t.NastavnikId, cascadeDelete: false)
                .ForeignKey("dbo.Predmets", t => t.PredmetId, cascadeDelete: true)
                .Index(t => t.NastavnikId)
                .Index(t => t.PredmetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zamjenas", "PredmetId", "dbo.Predmets");
            DropForeignKey("dbo.Zamjenas", "NastavnikId", "dbo.Nastavniks");
            DropForeignKey("dbo.Predmets", "RazredId", "dbo.Razreds");
            DropForeignKey("dbo.Razreds", "SkolskaGodinaId", "dbo.SkolskaGodinas");
            DropForeignKey("dbo.Predmets", "NastavnikId", "dbo.Nastavniks");
            DropIndex("dbo.Zamjenas", new[] { "PredmetId" });
            DropIndex("dbo.Zamjenas", new[] { "NastavnikId" });
            DropIndex("dbo.SkolskaGodinas", new[] { "Godina" });
            DropIndex("dbo.Razreds", new[] { "SkolskaGodinaId" });
            DropIndex("dbo.Predmets", new[] { "RazredId" });
            DropIndex("dbo.Predmets", new[] { "NastavnikId" });
            DropIndex("dbo.Nastavniks", new[] { "OIB" });
            DropTable("dbo.Zamjenas");
            DropTable("dbo.SkolskaGodinas");
            DropTable("dbo.Razreds");
            DropTable("dbo.Predmets");
            DropTable("dbo.Nastavniks");
        }
    }
}
