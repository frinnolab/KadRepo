using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KadcoPortal.Models
{
    public class DBConnect:DbContext
    {
        public DBConnect()
        {

        }

        public DbSet<CollectedBill> CollectedBills { get; set; }

        public DbSet<ControlNo> ControlNumbers { get; set; }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        public DbSet<GFSCode> GFSCodes { get; set; }

        public DbSet<PaymentCode> PaymentCodes { get; set; }
    }
}