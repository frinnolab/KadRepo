using KadcoMain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KadcoMain.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationDbContext DB = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}