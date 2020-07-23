using KadcoPortal.Models;
using KadcoPortal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KadcoPortal.Controllers
{
    public class HomeController : Controller
    {
        private DBConnect DB;

        public HomeController()
        {
            DB = new DBConnect();
        }
        public ActionResult Index()
        {
            var bills = DB.CollectedBills.OrderByDescending(m => m.id).Take(3).ToList();
            var gfCodes = DB.GFSCodes.ToList();
            var billViewModel = new CollectedBillViewModel
            {
                collectedBills = bills,
                gFSCodes = gfCodes

            };

            return View(billViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //PROCESS BILLS
        private int _billId, gfs_id, _phoneNumber, gfEdit;

        private decimal totalAmount, _amount, exchangeRate;

        private string gCode, gfCode_;

        [HttpPost]
        public ActionResult CreateNewBill(FormCollection bill_)
        {
            var billID = bill_["hiddenID"];

            //parse ints
            int.TryParse(billID, out _billId);
            int.TryParse(bill_["collectedBill.GFS_CodeId"], out gfs_id);

            decimal.TryParse(bill_["collectedBill.Amount"], out _amount);
            decimal.TryParse(bill_["collectedBill.ExchangeRate"], out exchangeRate);


            var newBill = new CollectedBill();
            var gfsCodes = DB.GFSCodes.ToList();


            if ((gfs_id != null) && (gfs_id > 0 ))
            {
                gCode = DB.GFSCodes.SingleOrDefault(c => c.id == gfs_id).CodeNumber;
            }
            else
            {
                gCode = "Non.";
            }


            if ((_billId == null) || (_billId <= 0))
            {
                //Add New Bill To DB

                newBill.PayerName = bill_["collectedBill.PayerName"];
                newBill.PhoneNumber = bill_["collectedBill.PhoneNumber"];
                newBill.CreatedDate = DateTime.Now;
                newBill.BillDate = DateTime.Parse(bill_["collectedBill.BillDate"]);
                newBill.GFSCodeStr = gCode;
                newBill.GFS_CodeId = gfs_id;
                newBill.PaymentCode = bill_["collectedBill.PaymentCode"];
                newBill.Description = bill_["collectedBill.Description"];
                newBill.Amount = _amount;
                newBill.ExchangeRate = exchangeRate;
                newBill.TotalAmount = _amount * exchangeRate;
                var a = 0;
                DB.CollectedBills.Add(newBill);
            }
            else
            {
                int dbID;
                int.TryParse(billID, out dbID);

                int.TryParse(bill_["collectedBill.GFS_CodeId"], out gfEdit);

                decimal.TryParse(bill_["collectedBill.Amount"], out _amount);
                decimal.TryParse(bill_["collectedBill.ExchangeRate"], out exchangeRate);

                //Convert Bill ID to Int

                var bill = DB.CollectedBills.Single(m => m.id == dbID);

                bill.PayerName = bill_["collectedBill.PayerName"];
                bill.PhoneNumber = bill_["collectedBill.PhoneNumber"];

                if ((gfEdit != null) && (gfEdit > 0))
                {
                    gfCode_ = DB.GFSCodes.SingleOrDefault(c => c.id == gfEdit).CodeNumber;
                }
                else {
                    gfCode_ = "Nill";
                }
                


                bill.CreatedDate = DateTime.Now;
                bill.BillDate = DateTime.Parse(bill_["collectedBill.BillDate"]);
                bill.GFS_CodeId = gfEdit;
                bill.GFSCodeStr = gfCode_;
                bill.PaymentCode = bill_["collectedBill.PaymentCode"];
                bill.Description = bill_["collectedBill.Description"];
                bill.ControlNo = bill_["collectedBill.ControlNo"];
                bill.Amount = _amount;
                bill.ExchangeRate = exchangeRate;
                bill.TotalAmount = _amount * exchangeRate;
            
            }
            DB.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var bill = DB.CollectedBills.SingleOrDefault(c => c.id == id);
            var gfCodes = DB.GFSCodes.ToList();

            var editBillvm = new CollectedBillViewModel
            {
                collectedBill = bill,
                gFSCodes = gfCodes
            };


            
            return View(editBillvm);
        }

        public ActionResult GetControlNo(int id)
        {
            //init Control source
            
            var ctrId = 0;
            Random ctrNum = new Random();

            ctrId = ctrNum.Next(99);

            var result = DB.ControlNumbers.Where(m => m.Id == ctrId).SingleOrDefault();

            var controlNumberUpdate = DB.CollectedBills.Where(c => c.id == id).Single();

            controlNumberUpdate.ControlNo = result.ControlNo_;
            DB.SaveChanges();

            var dbNum = 0;

            //if (controlNumberUpdate.ControlNo != null)
            //{
            //    var a = 0;
            //    return Json(controlNumberUpdate, JsonRequestBehavior.AllowGet);
            //}

            return RedirectToAction("Index");
        }

        public ActionResult DeleteBill(int id)
        {
            var bill = DB.CollectedBills.Find(id);
            DB.CollectedBills.Remove(bill);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}