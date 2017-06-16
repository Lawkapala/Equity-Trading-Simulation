using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockStreet.DLL.EntityClass;

namespace StockStreet.DLL.RepositoryInterface
{
    public interface IBroker<TEntity> : IRepository<TEntity> where TEntity: class
    {
        SearchMarket GetPrice(string symb);
        List<SearchMarket> GetAllMarket();
    }
}
