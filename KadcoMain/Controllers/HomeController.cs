using ACCPAC.Advantage;
using KadcoMain.Models;
using KadcoMain.Services;
using KadcoMain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KadcoMain.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext DB; 
        public HomeController()
        {
            DB = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var bills = DB.CollectedBills.OrderByDescending(m => m.id).Take(3).ToList();
            var gfCodes = DB.GFSCodes.ToList();
            var currencies = DB.Currencies.ToList();
            var billViewModel = new CollectedBillViewModel
            {
                collectedBills = bills,
                gFSCodes = gfCodes,
                Currencies = currencies

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
        private int _billId, gfs_id, currency_id, _phoneNumber, gfEdit;

        private decimal totalAmount, _amount, exchangeRate;

        private string gCode, gfCode_;

        //Sage init Fields
        private Session session;
        private DBLink mDBLinkCmpRW;
        private View arInvoiceBatch;
        private View arInvoiceHeader;
        private View arInvoiceDetail;
        private View arInvoicePaymentSchedules;
        private View arInvoiceHeaderOptFields;
        private View arInvoiceDetailOptFields;
        private View arCus;
        private View ARRECEIPTS3batch;
        private View ARRECEIPTS3header;
        private View ARRECEIPTS3detail1;
        private View ARRECEIPTS3detail2;
        private View ARRECEIPTS3detail3;
        private View ARRECEIPTS3detail4;
        private View ARRECEIPTS3detail5;
        private View ARRECEIPTS3detail6;
        private View ARPAYMPOST2;
        private SqlDataAdapter da;
        private SqlConnection conn;
        private SqlCommand cmd;
        DataSet ds0 = null;
        DataSet ds1 = null;
        
        //Sage fields End
        [HttpPost]
        public ActionResult CreateNewBill(FormCollection bill_)
        {
            var billID = bill_["hiddenID"];

            //parse ints
            int.TryParse(billID, out _billId);
            int.TryParse(bill_["collectedBill.GFS_CodeId"], out gfs_id);
            int.TryParse(bill_["collectedBill.Currency_id"], out currency_id);

            decimal.TryParse(bill_["collectedBill.Amount"], out _amount);
            decimal.TryParse(bill_["collectedBill.TotalAmount"], out totalAmount);
            decimal.TryParse(bill_["collectedBill.ExchangeRate"], out exchangeRate);


            var newBill = new CollectedBill();
            var gfsCodes = DB.GFSCodes.ToList();


            if (gfs_id > 0)
            {
                gCode = DB.GFSCodes.SingleOrDefault(c => c.id == gfs_id).CodeNumber;
            }
            else
            {
                gCode = "Non.";
            }


            if (_billId <= 0)
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
                newBill.Currency_id = currency_id;
                newBill.TotalAmount = totalAmount;
                newBill.PaymentDate = DateTime.Now;
                var a = 0;
                DB.CollectedBills.Add(newBill);
                
            }
            else
            {
                int dbID, curr_id;
                int.TryParse(billID, out dbID);

                int.TryParse(bill_["collectedBill.Currency_id"], out curr_id);

                int.TryParse(bill_["collectedBill.GFS_CodeId"], out gfEdit);

                decimal.TryParse(bill_["collectedBill.Amount"], out _amount);
                decimal.TryParse(bill_["collectedBill.TotalAmount"], out totalAmount);
                decimal.TryParse(bill_["collectedBill.ExchangeRate"], out exchangeRate);

                //Convert Bill ID to Int

                var bill = DB.CollectedBills.Single(m => m.id == dbID);

                var currencyID = DB.Currencies.Single(c => c.Id == curr_id);

                bill.PayerName = bill_["collectedBill.PayerName"];
                bill.PhoneNumber = bill_["collectedBill.PhoneNumber"];

                if ((gfEdit != null) && (gfEdit > 0))
                {
                    gfCode_ = DB.GFSCodes.SingleOrDefault(c => c.id == gfEdit).CodeNumber;
                }
                else
                {
                    gfCode_ = "Nill";
                }


                bill.CreatedDate = DateTime.Now;
                bill.BillDate = DateTime.Parse(bill_["collectedBill.BillDate"]);
                bill.GFS_CodeId = gfEdit;
                bill.GFSCodeStr = gfCode_;
                bill.GFS_Description = bill_["collectedBill.GFS_Description"];
                bill.PaymentCode = bill_["collectedBill.PaymentCode"];
                bill.Description = bill_["collectedBill.Description"];
                bill.ControlNo = bill_["collectedBill.ControlNo"];
                bill.Amount = _amount;
                bill.Currency_id = curr_id;
                bill.TotalAmount = totalAmount;
                bill.PaymentDate = DateTime.Now;

                DB.Entry(bill).State = EntityState.Modified;
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

        public ActionResult GetCurrRate(int id)
        {
            var a = 0;
            var currRate = DB.Currencies.SingleOrDefault(m => m.Id == id).Rate.ToString();

            a = 1;

            return Json(currRate, JsonRequestBehavior.AllowGet);
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

            return RedirectToAction("Index");
        }

        public ActionResult SaveToSage()
        {
            var bills = DB.CollectedBills.ToList();
            var result = "";
            if (bills.Count <= 0)
            {
                result = "No Pending Bills";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //SageLink
            DoCreateInvoice();

            result = "Successfully Posted to Sage";
            return Json(result, JsonRequestBehavior.AllowGet);
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

        public void DoCreateInvoice()
        {
            // Create, initialize and open a session.
            session = new Session();
            session.Init("", "XY", "XY1000", "65A");
            session.Open("ADMIN", "admin", "TSTDAT", DateTime.Today, 0);

            // Open a database link.
            mDBLinkCmpRW = session.OpenDBLink(DBLinkType.Company, DBLinkFlags.ReadWrite);

            // Open the A/R Invoice Entry Views
            arInvoiceBatch = mDBLinkCmpRW.OpenView("AR0031");
            arInvoiceHeader = mDBLinkCmpRW.OpenView("AR0032");
            arInvoiceDetail = mDBLinkCmpRW.OpenView("AR0033");
            arInvoicePaymentSchedules = mDBLinkCmpRW.OpenView("AR0034");
            arInvoiceHeaderOptFields = mDBLinkCmpRW.OpenView("AR0402");
            arInvoiceDetailOptFields = mDBLinkCmpRW.OpenView("AR0401");
            arCus = mDBLinkCmpRW.OpenView("AR0024");

            // Compose the Batch, Header and Detail views together.
            arInvoiceBatch.Compose(new View[] { arInvoiceHeader });
            arInvoiceHeader.Compose(new View[] { arInvoiceBatch, arInvoiceDetail, arInvoicePaymentSchedules, arInvoiceHeaderOptFields });
            arInvoiceDetail.Compose(new View[] { arInvoiceHeader, arInvoiceBatch, arInvoiceDetailOptFields });
            arInvoicePaymentSchedules.Compose(new View[] { arInvoiceHeader });
            arInvoiceHeaderOptFields.Compose(new View[] { arInvoiceHeader });
            arInvoiceDetailOptFields.Compose(new View[] { arInvoiceDetail });

            //OpenAndComposeViews();
            try
            {

                string connectionString = @"Data Source=FRINNOLAB\FRINNOSQLSERVER;Database=kad_test;Trusted_Connection=True;";
                //string connectionString = "Data Source=DESKTOP-031JB5E;Initial Catalog=AuwsaDB;Persist Security Info=True;User ID=sa;Password=Duxte_123;";
                conn = new SqlConnection(connectionString);
                SqlCommand cmd0 = new SqlCommand("select Distinct [Billing_Details],[AccountNo],[Bill_Date],[Bill_Ref_Number],[CATEGORY]  from [kad_test].[dbo].[BILLINTGITEM_FINAL_VIEW]", conn);
                SqlCommand cmd1 = new SqlCommand("select [Bill_Ref_Number],[ITEMNO],[ITEMS],[VALUE] from  [kad_test].[dbo].[BILLINTGITEM_FINAL_VIEW]", conn);
                //conn.ConnectionString = connectionString + "Connection Timeout=30;Connection Lifetime=0;Min Pool Size=0;Max Pool Size=100;Pooling=true;";
                conn.Open();

                var a = 0;
                SqlDataAdapter adapt0 = new SqlDataAdapter(cmd0);
                SqlDataAdapter adapt1 = new SqlDataAdapter(cmd1);
                DataSet cds0 = new DataSet();
                DataSet cds1 = new DataSet();
                adapt0.Fill(cds0, "BILLINTGITEM_FINAL_VIEW");
                adapt1.Fill(cds1, "BILLINTGITEM_FINAL_VIEW");


                var crprchobj0 = cds0;
                var crprchobj = cds1;
                conn.Close();

                // 1. RecordCreate batch to get the next available key for the batch.
                arInvoiceBatch.RecordCreate(ViewRecordCreate.Insert);

                // 2. Set the fields in the batch.
                arInvoiceBatch.Fields.FieldByName("BTCHDESC").SetValue("Billing Batch ", false);

                // 3. Update batch. (The batch record is already inserted by the RecordGenerate call, in step 1.)
                arInvoiceBatch.Update();

                // 4. RecordCreate header to get the next available key for the header. The key for the batch in the header view is assumed to be set already.
                //arInvoiceHeader.RecordCreate(ViewRecordCreate.DelayKey);
                ///////////////////////////

                if (crprchobj0.Tables["BILLINTGITEM_FINAL_VIEW"].Rows.Count > 0)
                {

                    foreach (DataRow d in crprchobj0.Tables["BILLINTGITEM_FINAL_VIEW"].Rows)
                    {

                        object InvHdRef = d["Bill_Ref_Number"];
                        string BILLref = Convert.ToString(InvHdRef);

                        arInvoiceHeader.RecordCreate(ViewRecordCreate.DelayKey);
                        // 5. Set the fields in the header.
                        arInvoiceHeader.Fields.FieldByName("INVCDESC").SetValue(d["Billing_Details"], false);
                        arInvoiceHeader.Fields.FieldByName("IDCUST").SetValue(d["AccountNo"], false);
                        arInvoiceHeader.Fields.FieldByName("DATEINVC").SetValue(d["Bill_Date"], false);
                        arInvoiceHeader.Fields.FieldByName("IDINVC").SetValue(d["Bill_Ref_Number"], false);
                        arInvoiceHeader.Fields.FieldByName("SPECINST").SetValue(d["CATEGORY"], false);

                        foreach (DataRow e in crprchobj.Tables["BILLINTGITEM_FINAL_VIEW"].Rows)
                        {
                            object InvDetRef = e["Bill_Ref_Number"];
                            string Dbillref = Convert.ToString(InvDetRef);

                            if (BILLref == Dbillref)
                            {

                                // 6. RecordCreate detail to initialize the fields.
                                arInvoiceDetail.RecordCreate(ViewRecordCreate.NoInsert);

                                // 7. Set zero (or last detail number) into the detail key field to insert from the start.
                                arInvoiceDetail.Fields.FieldByName("CNTLINE").SetValue(0, false);

                                // 8. Set values in the other detail fields.
                                arInvoiceDetail.Fields.FieldByName("IDITEM").SetValue(e["ITEMNO"], false);
                                arInvoiceDetail.Fields.FieldByName("AMTPRIC").SetValue(e["VALUE"], false);

                                // 9. Insert detail.
                                arInvoiceDetail.Insert();   // Insert the detail line (only in memory at this point).

                                //Messages.Add("Invoice Saved Successfully");
                                //copyErrors();   // Copy any warnings and other messages.

                            }

                        }
                        //  arInvoiceDetail.Update();   
                        // 10.5 Register the changes for the detail.
                        arInvoiceHeader.Insert(); // 11. Insert header. (This will do a Post of the details.)                                            

                    }

                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                SageLogger.TraceService(ex.Message + ex.InnerException);

            }
            catch (Exception ex)
            {
                //MyErrorHandler(ex);
                Console.WriteLine(ex.Message + ex.InnerException);
                SageLogger.TraceService(ex.Message + ex.InnerException);
            }
        }
    }
}