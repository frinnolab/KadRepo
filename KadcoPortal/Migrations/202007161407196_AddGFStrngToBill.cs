namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGFStrngToBill : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes");
            DropIndex("dbo.CollectedBills", new[] { "GFS_CodeId" });
            AddColumn("dbo.CollectedBills", "GFSCodeStr", c => c.String());
            DropColumn("dbo.CollectedBills", "GFS_CodeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollectedBills", "GFS_CodeId", c => c.Int());
            DropColumn("dbo.CollectedBills", "GFSCodeStr");
            CreateIndex("dbo.CollectedBills", "GFS_CodeId");
            AddForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes", "id");
        }
    }
}
