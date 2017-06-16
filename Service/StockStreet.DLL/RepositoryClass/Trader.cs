using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StockStreet.DLL.RepositoryInterface;

namespace StockStreet.DLL.RepositoryClass
{
    public class Trader<TEntity> : Repository<TEntity>, ITrader<TEntity> where TEntity : class
    {
        StockStInternalEntities ctx = new StockStInternalEntities();

        public Trader(StockStInternalEntities context) : base(context)
        {
            ctx = context;
        }

        public Block GetBlock(int? blockId)
        {
            return ctx.Blocks.Where(x => x.blockId == blockId).ToList().FirstOrDefault();
        }

        public IEnumerable<Order> ViewOrders(int? blockId)
        {            
            List<int> r = (from n in ctx.OrderDetails
                    where n.blockId == blockId
                    select n.orderId).ToList();

            List<Order> res = new List<Order>();

            foreach (int item in r)
            {
                res.AddRange(ctx.Orders.Where(x => x.orderId == item));
            }

            return res;
        }

        public IEnumerable<Order> ViewOrders(string username)
        {
            return ctx.Orders.Where(x => x.orderStatus == "Open" && x.userName == username).ToList();
        }
        
        public IEnumerable<Block> ViewBlocksOpen(string username)
        {
            return ctx.Blocks.Where(x => x.blockStatus == "Open" && x.userName == username).ToList();
        }

        public IEnumerable<Block> ViewBlocksAllocated(string username)
        {
            var y = ctx.Blocks.Where(x => x.blockStatus == "Allocated" || x.blockStatus == "Partial");
            var z = y.Where(x => x.userName == username);
            return z;
        }

        

        public StockStInternalEntities InternalContext
        {
            get { return Context as StockStInternalEntities; }
        }
    }
}
