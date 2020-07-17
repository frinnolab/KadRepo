namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editInputsToCollectedBill : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CollectedBills", "ControlNoID", "dbo.ControlNoes");
            DropForeignKey("dbo.CollectedBills", "ExchangeRateId", "dbo.ExchangeRates");
            DropForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes");
            DropForeignKey("dbo.CollectedBills", "PaymentCodeId", "dbo.PaymentCodes");
            DropIndex("dbo.CollectedBills", new[] { "ExchangeRateId" });
            DropIndex("dbo.CollectedBills", new[] { "GFS_CodeId" });
            DropIndex("dbo.CollectedBills", new[] { "PaymentCodeId" });
            DropIndex("dbo.CollectedBills", new[] { "ControlNoID" });
            AddColumn("dbo.CollectedBills", "ExchangeRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.CollectedBills", "GFS_Code", c => c.String());
            AddColumn("dbo.CollectedBills", "PaymentCode", c => c.String());
            AddColumn("dbo.CollectedBills", "ControlNo", c => c.String());
            DropColumn("dbo.CollectedBills", "ExchangeRateId");
            DropColumn("dbo.CollectedBills", "GFS_CodeId");
            DropColumn("dbo.CollectedBills", "PaymentCodeId");
            DropColumn("dbo.CollectedBills", "ControlNoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollectedBills", "ControlNoID", c => c.Int());
            AddColumn("dbo.CollectedBills", "PaymentCodeId", c => c.Int());
            AddColumn("dbo.CollectedBills", "GFS_CodeId", c => c.Int());
            AddColumn("dbo.CollectedBills", "ExchangeRateId", c => c.Int());
            DropColumn("dbo.CollectedBills", "ControlNo");
            DropColumn("dbo.CollectedBills", "PaymentCode");
            DropColumn("dbo.CollectedBills", "GFS_Code");
            DropColumn("dbo.CollectedBills", "ExchangeRate");
            CreateIndex("dbo.CollectedBills", "ControlNoID");
            CreateIndex("dbo.CollectedBills", "PaymentCodeId");
            CreateIndex("dbo.CollectedBills", "GFS_CodeId");
            CreateIndex("dbo.CollectedBills", "ExchangeRateId");
            AddForeignKey("dbo.CollectedBills", "PaymentCodeId", "dbo.PaymentCodes", "Id");
            AddForeignKey("dbo.CollectedBills", "GFS_CodeId", "dbo.GFSCodes", "id");
            AddForeignKey("dbo.CollectedBills", "ExchangeRateId", "dbo.ExchangeRates", "Id");
            AddForeignKey("dbo.CollectedBills", "ControlNoID", "dbo.ControlNoes", "Id");
        }
    }
}
