namespace elcubo9.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsymboltocustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Symbol", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Symbol");
        }
    }
}
