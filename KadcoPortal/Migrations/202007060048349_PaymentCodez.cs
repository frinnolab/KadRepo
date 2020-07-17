namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentCodez : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.CollectedBills", "PaymentCodeId");
            AddForeignKey("dbo.CollectedBills", "PaymentCodeId", "dbo.PaymentCodes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectedBills", "PaymentCodeId", "dbo.PaymentCodes");
            DropIndex("dbo.CollectedBills", new[] { "PaymentCodeId" });
            DropTable("dbo.PaymentCodes");
        }
    }
}
