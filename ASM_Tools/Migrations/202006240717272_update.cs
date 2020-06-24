namespace ASM_Tools.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeViewModel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CheckBoxViewModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToolName = c.String(),
                        Checked = c.Boolean(nullable: false),
                        EmployeeViewModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeViewModel", t => t.EmployeeViewModel_ID)
                .Index(t => t.EmployeeViewModel_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckBoxViewModel", "EmployeeViewModel_ID", "dbo.EmployeeViewModel");
            DropIndex("dbo.CheckBoxViewModel", new[] { "EmployeeViewModel_ID" });
            DropTable("dbo.CheckBoxViewModel");
            DropTable("dbo.EmployeeViewModel");
        }
    }
}
