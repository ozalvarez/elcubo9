namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceInMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Price");
        }
    }
}
