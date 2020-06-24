namespace ASM_Tools.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        salutation = c.Int(nullable: false),
                        JoinedDate = c.DateTime(nullable: false),
                        JobTitle = c.String(),
                        Department = c.String(),
                        CompanyEmail = c.String(),
                        ResumePath = c.String(),
                        DisplayPhotoPath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ToolToEmployee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        ToolID = c.Int(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.Tool", t => t.ToolID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.ToolID);
            
            CreateTable(
                "dbo.Tool",
                c => new
                    {
                        ToolID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Tag = c.String(),
                        CoverImagePath = c.String(),
                        GalleryPath = c.String(),
                        DocumentationPath = c.String(),
                        InstallationPath = c.String(),
                        VideoPath = c.String(),
                    })
                .PrimaryKey(t => t.ToolID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToolToEmployee", "ToolID", "dbo.Tool");
            DropForeignKey("dbo.ToolToEmployee", "EmployeeID", "dbo.Employee");
            DropIndex("dbo.ToolToEmployee", new[] { "ToolID" });
            DropIndex("dbo.ToolToEmployee", new[] { "EmployeeID" });
            DropTable("dbo.Tool");
            DropTable("dbo.ToolToEmployee");
            DropTable("dbo.Employee");
        }
    }
}
