namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addblocktouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Block", c => c.Boolean(nullable: false, defaultValue:false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Block");
        }
    }
}
