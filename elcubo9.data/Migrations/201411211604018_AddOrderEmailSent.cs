namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderEmailSent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderEmailSents",
                c => new
                    {
                        CustomerEmailID = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerEmailID, t.OrderID })
                .ForeignKey("dbo.CustomerEmails", t => t.CustomerEmailID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: false)
                .Index(t => t.CustomerEmailID)
                .Index(t => t.OrderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderEmailSents", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderEmailSents", "CustomerEmailID", "dbo.CustomerEmails");
            DropIndex("dbo.OrderEmailSents", new[] { "OrderID" });
            DropIndex("dbo.OrderEmailSents", new[] { "CustomerEmailID" });
            DropTable("dbo.OrderEmailSents");
        }
    }
}
