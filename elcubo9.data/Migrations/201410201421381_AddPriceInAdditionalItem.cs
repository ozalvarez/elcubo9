namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceInAdditionalItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdditionalItems", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdditionalItems", "Price");
        }
    }
}
