using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stock_Street.UI.Controllers
{
    public class BrokerController : Controller
    {
        // GET: Broker
        public ActionResult BrokerIndex()
        {
            return View();
        }
        public ActionResult BrokerUpdateMarketData()
        {
            return View();
        }
        public ActionResult BrokerPendingBlocks()
        {
            return View();
        }
        public ActionResult BrokerExecutedBlocks()
        {
            return View();
        }
        public ActionResult BrokerAddMarketData()
        {
            return View();
        }
        public ActionResult BrokerEditMarketData()
        {
            return View();
        }
        public ActionResult BrokerPartialBlocks()
        {
            return View();
        }
    }
}