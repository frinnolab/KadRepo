using KadcoMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KadcoMain.ViewModels
{
    public class CollectedBillViewModel
    {

        public CollectedBill collectedBill { get; set; }
        public List<CollectedBill> collectedBills { get; set; }
        public List<GFSCode> gFSCodes { get; set; }

        public List<Currency> Currencies { get; set; }

        public List<PaymentCode> PaymentCodes { get; set; }



        public CollectedBillViewModel()
        {
            collectedBill = new CollectedBill();
        }

    }
}