using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockStreet.DLL;
using StockStreet.DLL.RepositoryClass;
using System.Web.Http.Cors;

namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BrokerBlockViewController : ApiController
    {
        StockStExternalEntities1 context = new StockStExternalEntities1();
        // GET: api/BrokerBlockView
        public IEnumerable<ExternalBlock> Get()        {
            Broker<ExternalBlock> obj = new Broker<ExternalBlock>(context);
            return obj.OpenBlocks();
        }

        // GET: api/BrokerBlockView/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BrokerBlockView
        public void Post(List<Block> obj)
        {
            Broker<Block> br = new Broker<Block>(context);
            for (int i = 0; i < obj.Count; i++)
            {
                br.Add(obj[i]);
            }
        }

        // PUT: api/BrokerBlockView/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BrokerBlockView/5
        public void Delete(int id)
        {
        }
    }
}
