using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stock_Street.UI.Controllers
{
    public class PMController : Controller
    {
        // GET: PM
        public ActionResult PMIndex()
        {
            return View();
        }
        public ActionResult PMPortfolios()
        {
            return View();
        }
        public ActionResult PMCreatePortfolio()
        {
            return View();
        }
        public ActionResult PMCreateOrder()
        {
            return View();
        }
        public ActionResult PMPendingOrders()
        {
            return View();
        }
        public ActionResult PMExecutedOrders()
        {
            return View();
        }
        public ActionResult PMEditPendingOrders()
        {
            return View();
        }
        public ActionResult PMAmendPendingOrders()
        {
            return View();
        }
    }
}