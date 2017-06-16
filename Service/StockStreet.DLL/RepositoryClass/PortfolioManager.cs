using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockStreet.DLL.RepositoryInterface;

namespace StockStreet.DLL.RepositoryClass
{
    public class PortfolioManager : Repository<Order>, IPortfolioManager
    {
        public PortfolioManager(DbContext context) : base(context)
        {
        }
        StockStInternalEntities db = new StockStInternalEntities();

        public void SendToTrader(IEnumerable<int> x)
        {
            Order o = new Order();
            foreach (int item in x)
            {
                o = db.Orders.Find(item);
                o.orderStatus = "Open";
                db.SaveChanges();
            }

        }

        public void DeleteOrders(IEnumerable<int> x)
        {
            Order o = new Order();
            OrderDetail od = new OrderDetail();
            foreach(int item in x)
            {
                var data = from n in db.Orders
                     join m in db.OrderDetails on n.orderId equals m.orderId
                     join b in db.Blocks on m.blockId equals b.blockId
                     where b.blockStatus == "Open" && m.orderId == item
                     select m;

                foreach(var item1 in data.ToList())
                {
                    db.OrderDetails.Remove((OrderDetail)item1);

                    var data2 = (from n in db.Orders
                                where n.orderId == item1.orderId
                                select n).FirstOrDefault();
                    db.Orders.Remove((Order)data2);
                    db.SaveChanges();
                }

                
            }
        }

        public IEnumerable<Order> ViewPendingOrders(string id)
        {
            var x = from n in db.Orders
                    join m in db.Portfolios
                    on n.portfolioId equals m.portfolioId
                    where (n.orderStatus == "Open" || n.orderStatus == "New") && m.userName == id
                    select n;
            return x.ToList();
        }

        public IEnumerable<Order> ViewExecutedOrders(string id)
        {
            var x = from n in db.Orders
                    join m in db.Portfolios
                    on n.portfolioId equals m.portfolioId
                    where n.orderStatus == "Executed" && m.userName == id
                    select n;
            return x.ToList();
        }

        public IEnumerable<Order> GetOrdersForAPortfolio(int id)
        {
            var x = from n in db.Orders
                    where n.portfolioId == id
                    select n;

            return x.ToList();
        }

        public IEnumerable<Portfolio> GetPortfolios(string pmid)
        {
            var x = from n in db.Portfolios
                    where pmid == n.userName
                    select n;
            return x.ToList();
        }

        public IEnumerable<UserDetail> GetTrader()
        {
            var x = from n in db.UserDetails
                    where n.role == "ET"
                    select n;

            return x.ToList();
        }

    }
}
