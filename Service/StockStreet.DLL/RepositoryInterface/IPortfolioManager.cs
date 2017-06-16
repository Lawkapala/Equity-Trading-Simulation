using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockStreet.DLL.RepositoryInterface
{
    public interface IPortfolioManager : IRepository<Order> 
    {
        void SendToTrader(IEnumerable<int> x);

        IEnumerable<Order> ViewPendingOrders(string id);

        IEnumerable<Order> ViewExecutedOrders(string id);

        IEnumerable<Order> GetOrdersForAPortfolio(int id);

        void DeleteOrders(IEnumerable<int> id);

        IEnumerable<UserDetail> GetTrader();
    }
}
