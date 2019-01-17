namespace Homeworkapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Egzamin",
                c => new
                    {
                        EgzaminID = c.Int(nullable: false, identity: true),
                        PrzedmiotID = c.Int(nullable: false),
                        Termin = c.DateTime(nullable: false),
                        Opis = c.String(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.EgzaminID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID, cascadeDelete: true)
                .Index(t => t.PrzedmiotID);
            
            CreateTable(
                "dbo.Przedmiot",
                c => new
                    {
                        PrzedmiotID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.PrzedmiotID);
            
            CreateTable(
                "dbo.Kolokwium",
                c => new
                    {
                        KolokwiumID = c.Int(nullable: false, identity: true),
                        PrzedmiotID = c.Int(nullable: false),
                        Termin = c.DateTime(nullable: false),
                        Opis = c.String(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.KolokwiumID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID, cascadeDelete: true)
                .Index(t => t.PrzedmiotID);
            
            CreateTable(
                "dbo.Zadanie",
                c => new
                    {
                        ZadanieID = c.Int(nullable: false, identity: true),
                        ZjazdID = c.Int(nullable: false),
                        PrzedmiotID = c.Int(nullable: false),
                        Opis = c.String(nullable: false),
                        Url = c.String(),
                        Termin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ZadanieID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID, cascadeDelete: true)
                .ForeignKey("dbo.Zjazd", t => t.ZjazdID, cascadeDelete: true)
                .Index(t => t.ZjazdID)
                .Index(t => t.PrzedmiotID);
            
            CreateTable(
                "dbo.Zjazd",
                c => new
                    {
                        ZjazdID = c.Int(nullable: false, identity: true),
                        Numer = c.String(nullable: false, maxLength: 3),
                        Dzien1 = c.DateTime(nullable: false),
                        Dzien2 = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ZjazdID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zadanie", "ZjazdID", "dbo.Zjazd");
            DropForeignKey("dbo.Zadanie", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Kolokwium", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Egzamin", "PrzedmiotID", "dbo.Przedmiot");
            DropIndex("dbo.Zadanie", new[] { "PrzedmiotID" });
            DropIndex("dbo.Zadanie", new[] { "ZjazdID" });
            DropIndex("dbo.Kolokwium", new[] { "PrzedmiotID" });
            DropIndex("dbo.Egzamin", new[] { "PrzedmiotID" });
            DropTable("dbo.Zjazd");
            DropTable("dbo.Zadanie");
            DropTable("dbo.Kolokwium");
            DropTable("dbo.Przedmiot");
            DropTable("dbo.Egzamin");
        }
    }
}
