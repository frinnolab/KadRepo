namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGfsTypeToBill : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollectedBills", "GFS_CodeId", c => c.Int());
            AddColumn("dbo.CollectedBills", "gFS_id", c => c.Int());
            CreateIndex("dbo.CollectedBills", "gFS_id");
            AddForeignKey("dbo.CollectedBills", "gFS_id", "dbo.GFSCodes", "id");
            DropColumn("dbo.CollectedBills", "GFSCodeStr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollectedBills", "GFSCodeStr", c => c.String());
            DropForeignKey("dbo.CollectedBills", "gFS_id", "dbo.GFSCodes");
            DropIndex("dbo.CollectedBills", new[] { "gFS_id" });
            DropColumn("dbo.CollectedBills", "gFS_id");
            DropColumn("dbo.CollectedBills", "GFS_CodeId");
        }
    }
}
