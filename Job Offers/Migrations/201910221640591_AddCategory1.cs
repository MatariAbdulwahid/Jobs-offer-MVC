namespace Job_Offers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CategoryDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CategoryDescription", c => c.Int(nullable: false));
        }
    }
}
