namespace ASM_Tools.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tool", "team", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tool", "team");
        }
    }
}
