using KadcoPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KadcoPortal.Controllers.API
{
    public class ControlnumController : ApiController
    {
        private DBConnect DB;

        public ControlnumController()
        {
            DB = new DBConnect();
        }


        [HttpGet]
        //api/Controlnum
        public IEnumerable<ControlNo> GetAllControlNums()
        {
            var bills = DB.ControlNumbers.ToList();

            return bills;
        }

        //api/controlnum/1

        [HttpGet]
        public ControlNo GetControlNum(int id)
        {
            var controlNum = DB.ControlNumbers.SingleOrDefault(c => c.Id == id);

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return controlNum;
        }

        [HttpPost]
        public ControlNo CreateContolNum(ControlNo controlNum)
        {

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            DB.ControlNumbers.Add(controlNum);
            DB.SaveChanges();

            return controlNum;

        }

        //api/controlnum/1
        [HttpPut]
        public ControlNo EditControl(int id, ControlNo controlNo)
        {
            var controlInDb = DB.ControlNumbers.SingleOrDefault(c => c.Id == id);

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            controlInDb.ControlNo_ = controlNo.ControlNo_;

            DB.SaveChanges();

            return controlNo;
        }
        






    }
}
