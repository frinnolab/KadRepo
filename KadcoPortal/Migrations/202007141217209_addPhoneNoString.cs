﻿namespace KadcoPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhoneNoString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CollectedBills", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AddColumn("dbo.CollectedBills", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
