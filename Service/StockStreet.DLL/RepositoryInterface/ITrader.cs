using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockStreet.DLL.RepositoryInterface;

namespace StockStreet.DLL.RepositoryInterface
{
    public interface ITrader<TEntity> : IRepository<TEntity> where TEntity:class
    {
        IEnumerable<Order> ViewOrders(int? id);
        //IEnumerable<Order> ViewOrders();
    }
}
