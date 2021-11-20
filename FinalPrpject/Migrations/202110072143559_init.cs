namespace FinalPrpject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        price = c.Double(nullable: false),
                        Cattegory_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Cattegory_ID)
                .Index(t => t.Cattegory_ID);
            
            CreateTable(
                "dbo.ReposotryItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        Items_ID = c.Int(),
                        Reposotry_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Items", t => t.Items_ID)
                .ForeignKey("dbo.Reposotries", t => t.Reposotry_ID)
                .Index(t => t.Items_ID)
                .Index(t => t.Reposotry_ID);
            
            CreateTable(
                "dbo.Reposotries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReposotryItems", "Reposotry_ID", "dbo.Reposotries");
            DropForeignKey("dbo.ReposotryItems", "Items_ID", "dbo.Items");
            DropForeignKey("dbo.Items", "Cattegory_ID", "dbo.Categories");
            DropIndex("dbo.ReposotryItems", new[] { "Reposotry_ID" });
            DropIndex("dbo.ReposotryItems", new[] { "Items_ID" });
            DropIndex("dbo.Items", new[] { "Cattegory_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Reposotries");
            DropTable("dbo.ReposotryItems");
            DropTable("dbo.Items");
            DropTable("dbo.Categories");
        }
    }
}
