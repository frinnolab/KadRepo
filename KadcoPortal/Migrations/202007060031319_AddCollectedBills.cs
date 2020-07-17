namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCollectedBills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CollectedBills",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        BillDate = c.DateTime(nullable: false),
                        PayerName = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        ExchangeRateId = c.Int(nullable: false),
                        GFS_CodeId = c.Int(nullable: false),
                        PaymentCodeId = c.Int(nullable: false),
                        ControlNoID = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentDate = c.DateTime(nullable: false),
                        Reference = c.String(),
                        CreatedBy = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CollectedBills");
        }
    }
}
