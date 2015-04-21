namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMenuAdditionalIDinMenuAdditional : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MenuAdditionals");
            AddColumn("dbo.MenuAdditionals", "MenuAdditionalID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MenuAdditionals", "MenuAdditionalID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MenuAdditionals");
            DropColumn("dbo.MenuAdditionals", "MenuAdditionalID");
            AddPrimaryKey("dbo.MenuAdditionals", new[] { "MenuID", "AdditionalID" });
        }
    }
}
