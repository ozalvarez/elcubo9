namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrintInCustomerUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerUsers", "Print", c => c.Boolean(nullable: false,defaultValue:false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerUsers", "Print");
        }
    }
}
