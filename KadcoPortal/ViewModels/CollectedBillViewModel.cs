using KadcoPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KadcoPortal.ViewModels
{
    public class CollectedBillViewModel
    {
        public CollectedBill collectedBill { get; set; }
        public IEnumerable<CollectedBill> collectedBills { get; set; }
        public IEnumerable<GFSCode> gFSCodes { get; set; }


        public CollectedBillViewModel()
        {
            collectedBill = new CollectedBill();
        }


    }
}