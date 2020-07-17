namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddControlNos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ControlNoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ControlNo_ = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.CollectedBills", "ControlNoID");
            AddForeignKey("dbo.CollectedBills", "ControlNoID", "dbo.ControlNoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectedBills", "ControlNoID", "dbo.ControlNoes");
            DropIndex("dbo.CollectedBills", new[] { "ControlNoID" });
            DropTable("dbo.ControlNoes");
        }
    }
}
