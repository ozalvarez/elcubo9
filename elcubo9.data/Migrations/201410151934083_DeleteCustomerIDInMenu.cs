namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCustomerIDInMenu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Menus", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Menus", new[] { "CustomerID" });
            DropColumn("dbo.Menus", "CustomerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menus", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Menus", "CustomerID");
            AddForeignKey("dbo.Menus", "CustomerID", "dbo.Customers", "CustomerID", cascadeDelete: true);
        }
    }
}
