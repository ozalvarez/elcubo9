namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_received_to_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Received", c => c.Boolean(nullable: false, defaultValue:false));
            AddColumn("dbo.Orders", "DateReceived", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DateReceived");
            DropColumn("dbo.Orders", "Received");
        }
    }
}
