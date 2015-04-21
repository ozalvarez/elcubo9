namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddisabledtomenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Disabled", c => c.Boolean(nullable: false,defaultValue:false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Disabled");
        }
    }
}
