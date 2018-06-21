namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Name", c => c.String());
            AddColumn("dbo.Eats", "Name", c => c.String());
            AddColumn("dbo.Salaries", "Name", c => c.String());
            AddColumn("dbo.Transports", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transports", "Name");
            DropColumn("dbo.Salaries", "Name");
            DropColumn("dbo.Eats", "Name");
            DropColumn("dbo.Cars", "Name");
        }
    }
}
