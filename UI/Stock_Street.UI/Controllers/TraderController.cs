using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stock_Street.UI.Controllers
{
    public class TraderController : Controller
    {
        // GET: Trader
        public ActionResult TraderIndex()
        {
            return View();
        }
        public ActionResult TraderOpenBlocks()
        {
            return View();
        }
        public ActionResult TraderAllocatedBlocks()
        {
            return View();
        }
        public ActionResult TraderOpenOrders()
        {
            return View();
        }
        public ActionResult TraderEditBlocks()
        {
            return View();
        }
    }
}