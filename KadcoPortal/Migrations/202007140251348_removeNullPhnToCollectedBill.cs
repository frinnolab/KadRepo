namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeNullPhnToCollectedBill : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.CollectedBills", "PhoneNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.CollectedBills", "PhoneNumber", c => c.Int());
        }
    }
}
