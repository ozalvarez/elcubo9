namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePrintInCustomerUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CustomerUsers", "Print");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerUsers", "Print", c => c.Boolean(nullable: false));
        }
    }
}
