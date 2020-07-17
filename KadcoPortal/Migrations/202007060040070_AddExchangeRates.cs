namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExchangeRates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExchangeRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        Rate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.CollectedBills", "ExchangeRateId");
            AddForeignKey("dbo.CollectedBills", "ExchangeRateId", "dbo.ExchangeRates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectedBills", "ExchangeRateId", "dbo.ExchangeRates");
            DropIndex("dbo.CollectedBills", new[] { "ExchangeRateId" });
            DropTable("dbo.ExchangeRates");
        }
    }
}
