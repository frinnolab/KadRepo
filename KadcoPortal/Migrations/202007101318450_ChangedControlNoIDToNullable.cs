namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedControlNoIDToNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CollectedBills", "ControlNoID", "dbo.ControlNoes");
            DropIndex("dbo.CollectedBills", new[] { "ControlNoID" });
            AlterColumn("dbo.CollectedBills", "ControlNoID", c => c.Int());
            CreateIndex("dbo.CollectedBills", "ControlNoID");
            AddForeignKey("dbo.CollectedBills", "ControlNoID", "dbo.ControlNoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectedBills", "ControlNoID", "dbo.ControlNoes");
            DropIndex("dbo.CollectedBills", new[] { "ControlNoID" });
            AlterColumn("dbo.CollectedBills", "ControlNoID", c => c.Int(nullable: false));
            CreateIndex("dbo.CollectedBills", "ControlNoID");
            AddForeignKey("dbo.CollectedBills", "ControlNoID", "dbo.ControlNoes", "Id", cascadeDelete: true);
        }
    }
}
