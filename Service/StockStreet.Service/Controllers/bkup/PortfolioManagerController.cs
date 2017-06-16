using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockStreet.DLL.RepositoryClass;
using StockStreet.DLL;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using StockStreet.Service.CustomFilterRepo;

namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ProcessExceptionFilterAttribute]
    public class PortfolioManagerController : ApiController
    {
        StockStInternalEntities db = new StockStInternalEntities();

        [ResponseType(typeof(PortfolioManager))]
        public IEnumerable<UserDetail> GetTraderdetails()
        {
            PortfolioManager P = new PortfolioManager(db);
            return P.GetTrader();
        }

        // GET: api/PortfolioManager
        [ResponseType(typeof(PortfolioManager))]
        public IEnumerable<Order> GetPending(string id = null)
        {//Returns all pending (Open or new) orders for this PM. 

            if (id != null)
            {
                PortfolioManager P = new PortfolioManager(db);
                return P.ViewPendingOrders(id);
            }
            else {
                throw new ProcessException("ID is null");
            }
        }

        [ResponseType(typeof(PortfolioManager))]
        public IEnumerable<Order> GetExecuted(string id = null)
        {//Returns all pending (Open or new) orders for this PM. 

            if (id != null)
            {
                PortfolioManager P = new PortfolioManager(db);
                return P.ViewExecutedOrders(id);
            }
            else
            {
                throw new ProcessException("ID is null");
            }
        }

        // GET: api/PortfolioManager/5
        [ResponseType(typeof(PortfolioManager))]
        public IEnumerable<Order> GetPortfolioOrders(int id)
        {
            PortfolioManager P = new PortfolioManager(db);
            return P.GetOrdersForAPortfolio(id);
        }

        [ResponseType(typeof(PortfolioManager))]
        public IEnumerable<Portfolio> GetPortfolios(string pmid = null)
        {
            if (pmid != null)
            {
                PortfolioManager P = new PortfolioManager(db);
                return P.GetPortfolios(pmid);
            }
            else {
                throw new ProcessException("ID is null");
            }
        }

        [ResponseType(typeof(PortfolioManager))]
        public IEnumerable<Market> GetMarketData(string symbol = null)
        {
            if (symbol != null)
            {
                Repository<Market> P = new Repository<Market>(db);
                return P.GetAll();
            }
            else {
                throw new ProcessException("Symbol is null");
            }
        }

        // POST: api/PortfolioManager
        [HttpPost]
        [ResponseType(typeof(PortfolioManager))]
        public void CreateOrder([FromBody] Order O = null)
        {
            if (O != null)
            {
                PortfolioManager P = new PortfolioManager(db);
                P.Add(O);
            }
            else {
                throw new ProcessException("Order contains null attribute");
            }
        }

        [HttpPost]
        [ResponseType(typeof(PortfolioManager))]
        public void CreatePortfolio([FromBody] Portfolio Port = null)
        {
            // PortfolioManager P = new PortfolioManager(db);
            if (Port != null)
            {
                Repository<Portfolio> P = new Repository<Portfolio>(db);
                P.Add(Port);
            }
            else {
                throw new ProcessException("Portfolio contains null attributes");
            }
           
        }

        [HttpPost]
        [ResponseType(typeof(PortfolioManager))]
        public void SendOrder(IEnumerable<int> list = null)
        {
            if (list != null)
            {
                PortfolioManager P = new PortfolioManager(db);
                P.SendToTrader(list);
            }
            else {
                throw new ProcessException("Null List passed");
            }
        }

        // PUT: api/PortfolioManager/5
        [HttpPut]
        [ResponseType(typeof(PortfolioManager))]
        public void EditOrder([FromBody]Order O = null)
        {
            if (O != null)
            {
                PortfolioManager P = new PortfolioManager(db);
                P.Edit(O);
            }
            else {
                throw new ProcessException("Order contains null attributes");
            }
        }


        // DELETE: api/PortfolioManager/5
        [HttpPost]
        [ResponseType(typeof(PortfolioManager))]
        public void DeleteOrders(IEnumerable<int> list = null)
        {
            if (list != null)
            {
                PortfolioManager P = new PortfolioManager(db);
                P.DeleteOrders(list);
            }
            else
            {
                throw new ProcessException("Null List passed");
            }
        }
    }
}
