namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGFtoBill : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollectedBills", "GFS_CodeId", c => c.Int());
            CreateIndex("dbo.CollectedBills", "GFS_CodeId");
            AddForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes", "id");
            DropColumn("dbo.CollectedBills", "GFS_Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollectedBills", "GFS_Code", c => c.String());
            DropForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes");
            DropIndex("dbo.CollectedBills", new[] { "GFS_CodeId" });
            DropColumn("dbo.CollectedBills", "GFS_CodeId");
        }
    }
}
