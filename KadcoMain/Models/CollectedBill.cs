using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KadcoMain.Models
{
    public class CollectedBill
    {

        public int id { get; set; }

        public DateTime? CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BillDate { get; set; }

        [Display(Name = "Payer Name")]
        public string PayerName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Exchange Rate")]
        public Currency Currency { get; set; }
        public int? Currency_id { get; set; }

        [Display(Name = "GFS Code")]
        public GFSCode gFS { get; set; }
        public int? GFS_CodeId { get; set; }
        public string GFSCodeStr { get; set; }


        [Display(Name = "GFS Description")]
        public string GFS_Description { get; set; }


        //[Display(Name = "Payment Code")]
        public string PaymentCode { get; set; }


        [Display(Name = "Control No.")]
        public string ControlNo { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Reference { get; set; }

        public string CreatedBy { get; set; }

        public string Description { get; set; }
    }
}