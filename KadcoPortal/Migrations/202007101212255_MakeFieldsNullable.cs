namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeFieldsNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CollectedBills", "ExchangeRateId", "dbo.ExchangeRates");
            DropForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes");
            DropForeignKey("dbo.CollectedBills", "PaymentCodeId", "dbo.PaymentCodes");
            DropIndex("dbo.CollectedBills", new[] { "ExchangeRateId" });
            DropIndex("dbo.CollectedBills", new[] { "GFS_CodeId" });
            DropIndex("dbo.CollectedBills", new[] { "PaymentCodeId" });
            AlterColumn("dbo.CollectedBills", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.CollectedBills", "BillDate", c => c.DateTime());
            AlterColumn("dbo.CollectedBills", "PhoneNumber", c => c.Int());
            AlterColumn("dbo.CollectedBills", "ExchangeRateId", c => c.Int());
            AlterColumn("dbo.CollectedBills", "GFS_CodeId", c => c.Int());
            AlterColumn("dbo.CollectedBills", "PaymentCodeId", c => c.Int());
            CreateIndex("dbo.CollectedBills", "ExchangeRateId");
            CreateIndex("dbo.CollectedBills", "GFS_CodeId");
            CreateIndex("dbo.CollectedBills", "PaymentCodeId");
            AddForeignKey("dbo.CollectedBills", "ExchangeRateId", "dbo.ExchangeRates", "Id");
            AddForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes", "id");
            AddForeignKey("dbo.CollectedBills", "PaymentCodeId", "dbo.PaymentCodes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectedBills", "PaymentCodeId", "dbo.PaymentCodes");
            DropForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes");
            DropForeignKey("dbo.CollectedBills", "ExchangeRateId", "dbo.ExchangeRates");
            DropIndex("dbo.CollectedBills", new[] { "PaymentCodeId" });
            DropIndex("dbo.CollectedBills", new[] { "GFS_CodeId" });
            DropIndex("dbo.CollectedBills", new[] { "ExchangeRateId" });
            AlterColumn("dbo.CollectedBills", "PaymentCodeId", c => c.Int(nullable: false));
            AlterColumn("dbo.CollectedBills", "GFS_CodeId", c => c.Int(nullable: false));
            AlterColumn("dbo.CollectedBills", "ExchangeRateId", c => c.Int(nullable: false));
            AlterColumn("dbo.CollectedBills", "PhoneNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.CollectedBills", "BillDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CollectedBills", "CreatedDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.CollectedBills", "PaymentCodeId");
            CreateIndex("dbo.CollectedBills", "GFS_CodeId");
            CreateIndex("dbo.CollectedBills", "ExchangeRateId");
            AddForeignKey("dbo.CollectedBills", "PaymentCodeId", "dbo.PaymentCodes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes", "id", cascadeDelete: true);
            AddForeignKey("dbo.CollectedBills", "ExchangeRateId", "dbo.ExchangeRates", "Id", cascadeDelete: true);
        }
    }
}
