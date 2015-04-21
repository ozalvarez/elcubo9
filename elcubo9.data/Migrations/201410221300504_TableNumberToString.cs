namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNumberToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "TableNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TableNumber", c => c.Int(nullable: false));
        }
    }
}
