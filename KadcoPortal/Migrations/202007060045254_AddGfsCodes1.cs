namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGfsCodes1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GFSCodes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CodeNumber = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.CollectedBills", "GFS_CodeId");
            AddForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes");
            DropIndex("dbo.CollectedBills", new[] { "GFS_CodeId" });
            DropTable("dbo.GFSCodes");
        }
    }
}
