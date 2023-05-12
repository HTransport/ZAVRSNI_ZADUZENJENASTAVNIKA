namespace ZAVRSNI_ZADUZENJENASTAVNIKA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Nastavniks", new[] { "OIB" });
            AlterColumn("dbo.Nastavniks", "OIB", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Nastavniks", "OIB", c => c.Int(nullable: false));
            CreateIndex("dbo.Nastavniks", "OIB", unique: true);
        }
    }
}
