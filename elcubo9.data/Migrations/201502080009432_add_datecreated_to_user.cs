namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_datecreated_to_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateCreated");
        }
    }
}
