namespace Homeworkapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Przedmiot", "Nazwa", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Przedmiot", "Nazwa", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
