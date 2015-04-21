namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerInOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CustomerID");
            AddForeignKey("dbo.Orders", "CustomerID", "dbo.Customers", "CustomerID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropColumn("dbo.Orders", "CustomerID");
        }
    }
}
