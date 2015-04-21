namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderMenuAdditionalIDInOrderMenuAdditional : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderMenuAdditionals");
            AddColumn("dbo.OrderMenuAdditionals", "OrderMenuAdditionalID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderMenuAdditionals", "OrderMenuAdditionalID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OrderMenuAdditionals");
            DropColumn("dbo.OrderMenuAdditionals", "OrderMenuAdditionalID");
            AddPrimaryKey("dbo.OrderMenuAdditionals", new[] { "OrderMenuID", "AdditionalItemID" });
        }
    }
}
