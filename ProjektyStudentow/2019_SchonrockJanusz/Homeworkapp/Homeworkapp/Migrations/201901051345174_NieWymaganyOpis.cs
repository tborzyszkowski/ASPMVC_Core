namespace Homeworkapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NieWymaganyOpis : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Egzamin", "Opis", c => c.String());
            AlterColumn("dbo.Kolokwium", "Opis", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Kolokwium", "Opis", c => c.String(nullable: false));
            AlterColumn("dbo.Egzamin", "Opis", c => c.String(nullable: false));
        }
    }
}
