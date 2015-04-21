namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerInAdditional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Additionals", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Additionals", "CustomerID");
            AddForeignKey("dbo.Additionals", "CustomerID", "dbo.Customers", "CustomerID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Additionals", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Additionals", new[] { "CustomerID" });
            DropColumn("dbo.Additionals", "CustomerID");
        }
    }
}
