namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderMenuID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderMenuAdditionals", new[] { "OrderID", "MenuID" }, "dbo.OrderMenus");
            DropForeignKey("dbo.OrderMenuAdditionals", "OrderMenuID", "dbo.OrderMenus");
            DropIndex("dbo.OrderMenuAdditionals", new[] { "OrderID", "MenuID" });
            RenameColumn(table: "dbo.OrderMenuAdditionals", name: "OrderID", newName: "OrderMenuID");
            DropPrimaryKey("dbo.OrderMenuAdditionals");
            DropPrimaryKey("dbo.OrderMenus");
            AddColumn("dbo.OrderMenus", "OrderMenuID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderMenuAdditionals", new[] { "OrderMenuID", "AdditionalItemID" });
            AddPrimaryKey("dbo.OrderMenus", "OrderMenuID");
            CreateIndex("dbo.OrderMenuAdditionals", "OrderMenuID");
            AddForeignKey("dbo.OrderMenuAdditionals", "OrderMenuID", "dbo.OrderMenus", "OrderMenuID", cascadeDelete: true);
            DropColumn("dbo.OrderMenuAdditionals", "MenuID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderMenuAdditionals", "MenuID", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderMenuAdditionals", "OrderMenuID", "dbo.OrderMenus");
            DropIndex("dbo.OrderMenuAdditionals", new[] { "OrderMenuID" });
            DropPrimaryKey("dbo.OrderMenus");
            DropPrimaryKey("dbo.OrderMenuAdditionals");
            DropColumn("dbo.OrderMenus", "OrderMenuID");
            AddPrimaryKey("dbo.OrderMenus", new[] { "OrderID", "MenuID" });
            AddPrimaryKey("dbo.OrderMenuAdditionals", new[] { "OrderID", "MenuID", "AdditionalItemID" });
            RenameColumn(table: "dbo.OrderMenuAdditionals", name: "OrderMenuID", newName: "OrderID");
            CreateIndex("dbo.OrderMenuAdditionals", new[] { "OrderID", "MenuID" });
            AddForeignKey("dbo.OrderMenuAdditionals", "OrderMenuID", "dbo.OrderMenus", "OrderMenuID", cascadeDelete: true);
            AddForeignKey("dbo.OrderMenuAdditionals", new[] { "OrderID", "MenuID" }, "dbo.OrderMenus", new[] { "OrderID", "MenuID" }, cascadeDelete: true);
        }
    }
}
