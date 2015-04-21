namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInitTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalItems",
                c => new
                    {
                        AdditionalItemID = c.Int(nullable: false, identity: true),
                        AdditionalID = c.Int(nullable: false),
                        AdditionalItemName = c.String(),
                    })
                .PrimaryKey(t => t.AdditionalItemID)
                .ForeignKey("dbo.Additionals", t => t.AdditionalID, cascadeDelete: true)
                .Index(t => t.AdditionalID);
            
            CreateTable(
                "dbo.Additionals",
                c => new
                    {
                        AdditionalID = c.Int(nullable: false, identity: true),
                        AdditionalName = c.String(),
                    })
                .PrimaryKey(t => t.AdditionalID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Enabled = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        CustomerImg = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.CustomerUserRols",
                c => new
                    {
                        CustomerID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        CustomerUserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerID, t.UserID, t.CustomerUserType })
                .ForeignKey("dbo.CustomerUsers", t => new { t.CustomerID, t.UserID }, cascadeDelete: true)
                .Index(t => new { t.CustomerID, t.UserID });
            
            CreateTable(
                "dbo.CustomerUsers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CustomerID, t.UserID })
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.MenuAdditionals",
                c => new
                    {
                        MenuID = c.Int(nullable: false),
                        AdditionalID = c.Int(nullable: false),
                        MenuAdditionalName = c.String(),
                        Required = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuID, t.AdditionalID })
                .ForeignKey("dbo.Additionals", t => t.AdditionalID, cascadeDelete: true)
                .ForeignKey("dbo.Menus", t => t.MenuID, cascadeDelete: true)
                .Index(t => t.MenuID)
                .Index(t => t.AdditionalID);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        MenuID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Title = c.String(),
                        Subtitle = c.String(),
                        MenuTagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.MenuTags", t => t.MenuTagID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.MenuTagID);
            
            CreateTable(
                "dbo.MenuTags",
                c => new
                    {
                        MenuTagID = c.Int(nullable: false, identity: true),
                        MenuTagName = c.String(),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuTagID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: false)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.OrderMenuAdditionals",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        MenuID = c.Int(nullable: false),
                        AdditionalItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.MenuID, t.AdditionalItemID })
                .ForeignKey("dbo.AdditionalItems", t => t.AdditionalItemID, cascadeDelete: true)
                .ForeignKey("dbo.OrderMenus", t => new { t.OrderID, t.MenuID }, cascadeDelete: true)
                .Index(t => new { t.OrderID, t.MenuID })
                .Index(t => t.AdditionalItemID);
            
            CreateTable(
                "dbo.OrderMenus",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        MenuID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        AdditionalInfo = c.String(),
                    })
                .PrimaryKey(t => new { t.OrderID, t.MenuID })
                .ForeignKey("dbo.Menus", t => t.MenuID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.MenuID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        TableNumber = c.Int(nullable: false),
                        DateOrder = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderMenuAdditionals", new[] { "OrderID", "MenuID" }, "dbo.OrderMenus");
            DropForeignKey("dbo.OrderMenus", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderMenus", "MenuID", "dbo.Menus");
            DropForeignKey("dbo.OrderMenuAdditionals", "AdditionalItemID", "dbo.AdditionalItems");
            DropForeignKey("dbo.MenuAdditionals", "MenuID", "dbo.Menus");
            DropForeignKey("dbo.Menus", "MenuTagID", "dbo.MenuTags");
            DropForeignKey("dbo.MenuTags", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Menus", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.MenuAdditionals", "AdditionalID", "dbo.Additionals");
            DropForeignKey("dbo.CustomerUserRols", new[] { "CustomerID", "UserID" }, "dbo.CustomerUsers");
            DropForeignKey("dbo.CustomerUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerUsers", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.AdditionalItems", "AdditionalID", "dbo.Additionals");
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.OrderMenus", new[] { "MenuID" });
            DropIndex("dbo.OrderMenus", new[] { "OrderID" });
            DropIndex("dbo.OrderMenuAdditionals", new[] { "AdditionalItemID" });
            DropIndex("dbo.OrderMenuAdditionals", new[] { "OrderID", "MenuID" });
            DropIndex("dbo.MenuTags", new[] { "CustomerID" });
            DropIndex("dbo.Menus", new[] { "MenuTagID" });
            DropIndex("dbo.Menus", new[] { "CustomerID" });
            DropIndex("dbo.MenuAdditionals", new[] { "AdditionalID" });
            DropIndex("dbo.MenuAdditionals", new[] { "MenuID" });
            DropIndex("dbo.CustomerUsers", new[] { "UserID" });
            DropIndex("dbo.CustomerUsers", new[] { "CustomerID" });
            DropIndex("dbo.CustomerUserRols", new[] { "CustomerID", "UserID" });
            DropIndex("dbo.AdditionalItems", new[] { "AdditionalID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderMenus");
            DropTable("dbo.OrderMenuAdditionals");
            DropTable("dbo.MenuTags");
            DropTable("dbo.Menus");
            DropTable("dbo.MenuAdditionals");
            DropTable("dbo.CustomerUsers");
            DropTable("dbo.CustomerUserRols");
            DropTable("dbo.Customers");
            DropTable("dbo.Additionals");
            DropTable("dbo.AdditionalItems");
        }
    }
}
