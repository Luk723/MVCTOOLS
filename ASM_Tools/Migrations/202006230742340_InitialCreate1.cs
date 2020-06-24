namespace ASM_Tools.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "team", c => c.Int(nullable: false));
            AddColumn("dbo.Employee", "Manager_ID", c => c.Int());
            CreateIndex("dbo.Employee", "Manager_ID");
            AddForeignKey("dbo.Employee", "Manager_ID", "dbo.Employee", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "Manager_ID", "dbo.Employee");
            DropIndex("dbo.Employee", new[] { "Manager_ID" });
            DropColumn("dbo.Employee", "Manager_ID");
            DropColumn("dbo.Employee", "team");
        }
    }
}
