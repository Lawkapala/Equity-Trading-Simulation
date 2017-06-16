using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockStreet.DLL;
using StockStreet.DLL.RepositoryClass;
using StockStreet.DLL.EntityClass;
using Newtonsoft.Json.Linq;
using System.Web.Http.Cors;

namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TraderBlocksController : ApiController
    {
        StockStInternalEntities ctx = new StockStInternalEntities();        
        TokenBasedAuth t = new TokenBasedAuth();

        static string username;

        static void dec(string token)
        {
            TokenBasedAuth t = new TokenBasedAuth();
            username = t.Decode(token);
        }
        

        [HttpGet]
        [Route("api/TraderBlocks/GetOpenBlocks/{token?}")]
        public IEnumerable<Block> GetOpenBlocks(string token = null)
        {
            string username = t.Decode(token);
            IEnumerable<Block> s = null;
            if (username != null)
            {
                Trader<Block> t = new Trader<Block>(ctx);
                return t.ViewBlocksOpen(username);
            }
            else
                return s ?? Enumerable.Empty<Block>();
        }

        [HttpGet]
        [Route("api/TraderBlocks/GetBlock/{blockId:int?}")]
        public Block GetBlock(int? blockId = null)
        {
            if (blockId != null)
            {
                Trader<Block> t = new Trader<Block>(ctx);
                return t.GetBlock(blockId);
            }     
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }   

        [HttpGet]
        [Route("api/TraderBlocks/GetExecutedBlocks/{token?}")]
        public IEnumerable<Block> Executed(string token = null)
        {
            string username = t.Decode(token);
            IEnumerable<Block> s = null;
            if (username != null)
            {
                Trader<Block> t = new Trader<Block>(ctx);
                return t.ViewBlocksAllocated(username);
            }
            else
                return s ?? Enumerable.Empty<Block>() ;
        }

        [HttpGet]
        [Route("api/TraderBlocks/SendForExecution/")]
        public HttpResponseMessage SendForExecution(List<Block> block)
        {
            Trader<Block> tbl = new Trader<Block>(ctx);
            Trader<Order> tod = new Trader<Order>(ctx);

            try
            {
                foreach (var item in block)
                {
                    Block obj = new Block();
                    Order obj2 = new Order();

                    obj.blockStatus = "SentForExecution";
                    tbl.Edit(obj);

                    OrderDetail od = new OrderDetail();

                    List<OrderDetail> x = (from n in ctx.OrderDetails
                                           where n.blockId == item.blockId
                                           select n).ToList();

                    foreach (var it in x)
                    {
                        obj2 = ctx.Orders.Find(it.orderId);
                        obj2.orderStatus = "SentForExecution";
                        tod.Edit(obj2);
                    }

                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }        
        }

        // POST: api/TraderBlocks
        [HttpPost]
        [Route("api/TraderBlocks/AddToNewBlock")]
        public HttpResponseMessage AddToNewBlock([FromBody]List<int> orderID)
        {
            Trader<Block> t = new Trader<Block>(ctx);
            Trader<OrderDetail> od = new Trader<OrderDetail>(ctx);
            Trader<Order> o1 = new Trader<Order>(ctx);
            OrderDetail o = new OrderDetail();
            try
            {
                Block bl = new Block();
                int count = 0;
                var b = ctx.Orders.Find(orderID[0]);
                var c = b.orderType;

                foreach (var item in orderID)
                {
                    var x = ctx.Orders.Find(item);                    

                    if (count == 0)
                    {
                        bl.price = x.price;
                        bl.userName = x.userName;
                        bl.symbol = x.symbol;
                        bl.side = x.side;
                        bl.stopPrice = x.stopPrice;
                        bl.totalQuantity = x.totalQuantity;
                        bl.openQuantity = x.openQuantity;
                        bl.executedQuantity = 0;
                        bl.blockStatus = "Open";
                        bl.createTStamp = DateTime.Now;
                        bl.orderType = x.orderType;

                        count++;
                    }
                    else if (x.orderType == bl.orderType && bl.symbol == x.symbol && bl.side == x.side)
                    {
                        if (x.orderType == "Market")
                            bl.price = x.price;
                        else if (x.orderType == "Limit")
                        {
                            if (x.side == "Buy")
                            {
                                if(bl.price > x.price)
                                    bl.price = x.price;

                                if(bl.stopPrice < x.stopPrice)
                                    bl.stopPrice = x.stopPrice;
                            }
                            else if (x.side == "Sell")
                            {
                                if (bl.price < x.price)
                                    bl.price = x.price;

                                if (bl.stopPrice > x.stopPrice)
                                    bl.stopPrice = x.stopPrice;
                            }

                        }
                        bl.totalQuantity += x.totalQuantity;
                        bl.openQuantity = bl.totalQuantity;

                    }
                    else
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }
                    
                }

                t.Add(bl);
                int id = ctx.Blocks.Max(x => x.blockId);
                foreach (var item in orderID)
                {
                    o.blockId = id;
                    o.orderId = item;
                    od.Add(o);
                    Order obj = new Order();
                    obj = ctx.Orders.Find(item);
                    obj.orderStatus = "Blocked";
                    o1.Edit(obj);
                }
               return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);

            }
        }
        //Remaining Work
        [HttpPost]
        [Route("api/TraderBlocks/AddToExistingBlock")]
        public HttpResponseMessage AddToExistingBlock([FromBody]List<int> Ids)
        { 
            Trader<Order> obj = new Trader<Order>(ctx);
            Trader<Block> obj1 = new Trader<Block>(ctx);
            Trader<OrderDetail> obj2 = new Trader<OrderDetail>(ctx);
            OrderDetail o = new OrderDetail();

            try
            {
                //Block bl = new Block();
                var a = ctx.Blocks.Find(Ids[0]);

                 for (int i=1;i<Ids.Count;i++)
                {
                    var x = ctx.Orders.Find(Ids[i]);
                    
                    if (a.orderType == x.orderType && a.symbol == x.symbol && a.side == x.side)
                    {
                        if (a.orderType == "Market")
                            a.price = a.price;
                        else if (a.orderType == "Limit")
                        {
                            if (x.side == "Buy")
                            {
                                if (a.price > x.price)
                                    a.price = x.price;

                                if (a.stopPrice < x.stopPrice)
                                    a.stopPrice = x.stopPrice;
                            }
                            else if (x.side == "Sell")
                            {
                                if (a.price < x.price)
                                    a.price = x.price;

                                if (a.stopPrice > x.stopPrice)
                                    a.stopPrice = x.stopPrice;
                            }

                        }
                        a.totalQuantity += x.totalQuantity;
                        a.openQuantity = a.totalQuantity;

                    }
                    else
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }

                }

                obj1.Edit(a);
                for (int i=1;i<Ids.Count;i++)
                {
                    o.blockId = a.blockId;
                    o.orderId = Ids[i];
                    obj2.Add(o);
                    Order ord = new Order();
                    ord = ctx.Orders.Find(Ids[i]);
                    ord.orderStatus = "Blocked";
                    obj.Edit(ord);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        // PUT: api/TraderBlocks/5
        [Route("api/TraderBlocks/EditBlock")]
        public HttpResponseMessage Put(Block b)
        {
            try
            {
                Trader<Block> t = new Trader<Block>(ctx);
                t.Edit(b);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpDelete]
        // DELETE: api/TraderBlocks/5
        [Route("api/TraderBlocks/DeleteBlock/{id:int?}")]
        public HttpResponseMessage Delete(int id)
        {
            Trader<Block> b = new Trader<Block>(ctx);
            Trader<OrderDetail> od = new Trader<OrderDetail>(ctx);
            Trader<Order> o = new Trader<Order>(ctx);

            Block obj = ctx.Blocks.Find(id);
            try
            {
                if (obj == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Block with this" + id.ToString() + "Not found");
                }
                else
                {
                    List<OrderDetail> orders = new List<OrderDetail>();

                    orders = (from n in ctx.OrderDetails
                              where (n.blockId == id)
                              select n).ToList();

                    foreach (var item in orders)
                    {
                        Order obj1 = new Order();
                        obj1 = ctx.Orders.Find(item.orderId);
                        obj1.orderStatus = "Open";
                        od.Remove(item);
                    }
                    b.Remove(obj);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [HttpPost]
        [Route("api/TraderBlocks/ReceiveFills")]
        public HttpResponseMessage ReceiveFills([FromBody]List<Block> block)
        {
            Trader<Block> tb = new Trader<Block>(ctx);
            Trader<Order> to = new Trader<Order>(ctx);
            Trader<OrderDetail> tod = new Trader<OrderDetail>(ctx);
            List<OrderDetail> orders = new List<OrderDetail>();

            try
            {

                foreach (var item in block)
                {
                    //Block obj = new Block();
                    //obj.blockId = item.blockId;
                    //obj.blockStatus = item.blockStatus;
                    //obj.openQuantity = item.openQuantity;
                    //obj.price = item.price;
                    //obj.totalQuantity = item.totalQuantity;
                    //obj.executedQuantity = item.executedQuantity;
                    //obj.userName = item.userName;
                    //obj.symbol = item.symbol;
                    //obj.side = item.side;
                    //obj.createTStamp = item.createTStamp;
                    //obj.updatedTStamp = item.updatedTStamp;
                    //obj.stopPrice = item.stopPrice;
                    //obj.orderType = item.orderType;
                    
                  
                    var blk = ctx.Blocks.Find(item.blockId);
                     if(blk != null && blk.blockStatus == "Sent")
                    {
                        //tb.Edit(obj);
                        if (item.totalQuantity == item.executedQuantity)
                            item.blockStatus = "Allocated";
                        else 
                            item.blockStatus = "Partial";

                        ctx.Entry(blk).CurrentValues.SetValues(item);

                        var exQty = item.executedQuantity;

                        orders = (from n in ctx.OrderDetails
                                  where (n.blockId == item.blockId)
                                  select n).ToList();

                        foreach (var item1 in orders)
                        {
                            var x = ctx.Orders.Find(item1.orderId);

                            if (exQty > 0)
                            {
                                if (x.totalQuantity <= exQty)
                                {
                                    x.executedQuantity = x.totalQuantity;
                                    x.openQuantity = x.totalQuantity - x.executedQuantity;
                                    exQty -= x.executedQuantity;
                                    x.orderStatus = "Allocated";
                                    to.Edit(x);
                                }
                                else
                                {
                                    x.executedQuantity = exQty;
                                    x.openQuantity = x.totalQuantity - x.executedQuantity;
                                    exQty -= x.executedQuantity;
                                    x.orderStatus = "Allocated";
                                    to.Edit(x);
                                }
                            }
                        }
                        ctx.Entry(blk).CurrentValues.SetValues(item);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception e) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        [Route("api/TraderBlocks/ChangeBlockStatus")]
        public HttpResponseMessage ChangeBlockStatus(List<Block> b)
        {
            try
            {
                Trader<Block> t = new Trader<Block>(ctx);
                for (int i = 0; i < b.Count; i++)
                {
                    b[i].blockStatus = "Sent";
                    t.Edit(b[i]);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
