using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KadcoMain.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public double Rate { get; set; }
    }
}