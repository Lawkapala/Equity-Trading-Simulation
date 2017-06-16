using StockStreet.DLL;
using StockStreet.DLL.RepositoryClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BrokerHistoryController : ApiController
    {
        // GET: api/BrokerHistory
       
        StockStExternalEntities1 context = new StockStExternalEntities1();
        [HttpGet]
        public IEnumerable<ExternalBlock> Get()
        {
            Broker<ExternalBlock> obj = new Broker<ExternalBlock>(context);
            return obj.HistoryBlocks();
        }

        // GET: api/BrokerHistory/5
        [HttpGet]
        public IEnumerable<TradeExecution> Get(int id)
        {
            Broker<TradeExecution> obj = new Broker<TradeExecution>(context);
            return obj.HistoryBasedOnBlockId(id);
        }

        [HttpGet]
        public IEnumerable<ExternalBlock> GetPartial()
        {//Returns all pending (Open or new) orders for this PM. 

            Broker<ExternalBlock> obj = new Broker<ExternalBlock>(context);
            return obj.ViewPartial();
        }

        // POST: api/BrokerHistory
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BrokerHistory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BrokerHistory/5
        public void Delete(int id)
        {
        }
    }
}
