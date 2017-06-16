using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockStreet.DLL;
using StockStreet.DLL.RepositoryClass;
using System.Web.Http.Cors;
using StockStreet.DLL.EntityClass;

namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TraderOrdersController : ApiController
    {
        StockStInternalEntities ctx = new StockStInternalEntities();
        TokenBasedAuth t = new TokenBasedAuth();

        // GET: api/TraderOrders
        [Route("api/TraderOrders/GetOpenOrders/{token?}")]
        public IEnumerable<Order> GetOpenOrders(string token = null)
        {   //Get orders of all type  
            string username = t.Decode(token);
            IEnumerable<Order> s = null;
            if (username != null)
            {
                Trader<Order> t = new Trader<Order>(ctx);
                return t.ViewOrders(username);
            }
            else
                return s ?? Enumerable.Empty<Order>();
            
        }

        // GET: api/TraderOrders/5
        public IEnumerable<Order> Getod(int? id = null)
        {
            if (id != null)
            {
                Trader<Order> t = new Trader<Order>(ctx);
                return t.ViewOrders(id);
            }
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        //// POST: api/TraderOrders
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/TraderOrders/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/TraderOrders/5
        [Route("api/TraderOrders/RemoveOrder/{id}")]
        public HttpResponseMessage Delete(int id)
        {

            Trader<Order> obj = new Trader<Order>(ctx);
            Trader<OrderDetail> obj2 = new Trader<OrderDetail>(ctx);

            Order ob = ctx.Orders.Find(id);
            try
            {
                if (ob == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with this" + id.ToString() + "Not found");
                }
                else
                {
                    List<OrderDetail> data = (from n in ctx.OrderDetails
                                              where (n.orderId == id)
                                              select n).ToList();

                    ctx.OrderDetails.Remove(data.FirstOrDefault());

                    ob.orderStatus = "Open";
                    obj.Edit(ob);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            
            }

        

    }
}
