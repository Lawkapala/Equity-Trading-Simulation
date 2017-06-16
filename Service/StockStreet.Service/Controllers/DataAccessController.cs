using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockStreet.DLL.EntityClass;
using StockStreet.DLL.RepositoryClass;
using StockStreet.DLL;
using System.Web.Http.Cors;

namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DataAccessController : ApiController
    {
        StockStExternalEntities1 context = new StockStExternalEntities1();

        // GET: api/DataAccess
        //portfolio dropdown service to display all market data
        [HttpGet]
        public List<SearchMarket> Get()
        {
            Broker<BrokerSecurity> b = new Broker<BrokerSecurity>(context);
            return b.GetAllMarket();
        }



        // GET: api/DataAccess/5
        //Portfolio when he searches for a stock

        //Resolved
        [HttpGet]
        public SearchMarket Get(string id)
        {
            Broker<BrokerSecurity> b = new Broker<BrokerSecurity>(context);
            return b.GetPrice(id.ToUpper());
        }


        // POST: api/DataAccess
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DataAccess/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DataAccess/5
        public void Delete(int id)
        {
            
        }
    }
}
