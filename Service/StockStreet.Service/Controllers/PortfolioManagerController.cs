using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockStreet.DLL.RepositoryClass;
using StockStreet.DLL;
using System.Web.Http.Cors;
using StockStreet.DLL.EntityClass;

namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PortfolioManagerController : ApiController
    {
        StockStInternalEntities db = new StockStInternalEntities();
        TokenBasedAuth t = new TokenBasedAuth();

        public IEnumerable<UserDetail> GetTraderdetails()//NO
        {
            PortfolioManager P = new PortfolioManager(db);
            return P.GetTrader();
        }

        public Order GetOrderDetails(int orderId)
        {
            PortfolioManager P = new PortfolioManager(db);
            return P.Get(orderId);
        }
        // GET: api/PortfolioManager
        public IEnumerable<Order> GetPending(string token)//done
        {//Returns all pending (Open or new) orders for this PM. 
            string pmid = t.Decode(token);
            PortfolioManager P = new PortfolioManager(db);
            return P.ViewPendingOrders(pmid);
        }

        public IEnumerable<Order> GetExecuted(string token)//not implemented in angular
        {//Returns all pending (Open or new) orders for this PM. 
            string pmid = t.Decode(token);
            PortfolioManager P = new PortfolioManager(db);
            return P.ViewExecutedOrders(pmid);
        }

        // GET: api/PortfolioManager/5
        public IEnumerable<Order> GetPortfolioOrders(int id)
        {
            PortfolioManager P = new PortfolioManager(db);
            return P.GetOrdersForAPortfolio(id);
        }
        
        public IEnumerable<Portfolio> GetPortfolios(string token)//done
        {
            string pmid = t.Decode(token);
            PortfolioManager P = new PortfolioManager(db);
            return P.GetPortfolios(pmid);
        }

        public IEnumerable<Market> GetMarketData(string symbol)//NO
        {
            Repository<Market> P = new Repository<Market>(db);
            return P.GetAll();
        }

        // POST: api/PortfolioManager
        [HttpPost]
        public void CreateOrder([FromBody] Order O)
        {
            PortfolioManager P = new PortfolioManager(db);
            P.Add(O);
        }

        [HttpPost]
        public void CreatePortfolio([FromBody] Portfolio Port)//done
        {
           // PortfolioManager P = new PortfolioManager(db);
            Port.userName = t.Decode(Port.userName);
            Repository<Portfolio> P = new Repository<Portfolio>(db);
            P.Add(Port);
           
        }

        [HttpPost]
        public void SendOrder(IEnumerable<int> list)
        {
            PortfolioManager P = new PortfolioManager(db);
            P.SendToTrader(list);
        }

        // PUT: api/PortfolioManager/5
        [HttpPost]
        public void EditOrder([FromBody]Order O)
        {
            PortfolioManager P = new PortfolioManager(db);
            P.Edit(O);
        }


        // DELETE: api/PortfolioManager/5
        [HttpPost]
        public void DeleteOrders(IEnumerable<int> list)
        {
            PortfolioManager P = new PortfolioManager(db);
            P.DeleteOrders(list);
        }
    }
}
