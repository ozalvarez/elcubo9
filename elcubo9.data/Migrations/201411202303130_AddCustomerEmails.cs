namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerEmails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerEmails",
                c => new
                    {
                        CustomerEmailID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CustomerEmailID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerEmails", "CustomerID", "dbo.Customers");
            DropIndex("dbo.CustomerEmails", new[] { "CustomerID" });
            DropTable("dbo.CustomerEmails");
        }
    }
}
