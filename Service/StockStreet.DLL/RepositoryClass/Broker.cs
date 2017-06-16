using StockStreet.DLL.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using StockStreet.DLL.EntityClass;

namespace StockStreet.DLL.RepositoryClass
{
    public class Broker<TEntity> : Repository<TEntity>, IBroker<TEntity> where TEntity : class
    {
        StockStExternalEntities1 ctx = new StockStExternalEntities1();

        public Broker(StockStExternalEntities1 context) : base(context)
        {
            ctx = context;
        }

        public List<SearchMarket> GetAllMarket()
        {
            
            var x = from n in ctx.BrokerSecurities
                    select new SearchMarket { price = n.tradePrice, symbName = n.securityName,symbol=n.securitySymbol };            
            return x.ToList();
        }

        public SearchMarket GetPrice(string id)
        {            
            var x = (from n in ctx.BrokerSecurities
                    where n.securitySymbol.Equals(id)
                     select new SearchMarket { price = n.tradePrice, symbName = n.securityName, symbol = n.securitySymbol }).FirstOrDefault();            
            return x;

        }


        public List<ExternalBlock> OpenBlocks()
        {
            var x = from n in ctx.ExternalBlocks
                    where n.blockStatus.Equals("Open")
                     select n;
            return x.ToList();
        }

        public List<ExternalBlock> HistoryBlocks()
        {
            var x = from n in ctx.ExternalBlocks
                    where (n.blockStatus != "Open" && n.blockStatus!="Partially")
                    select n;
            return x.ToList();
        }

        public List<ExternalBlock> ViewPartial()
        {
            var x = from n in ctx.ExternalBlocks
                    where (n.blockStatus == "Partially")
                    select n;
            return x.ToList();
        }

        public List<TradeExecution> HistoryBasedOnBlockId(int id)
        {
            var x = from n in ctx.TradeExecutions
                    where n.blockId.Equals(id)
                    select n;
            return x.ToList();
        }



    }
}
