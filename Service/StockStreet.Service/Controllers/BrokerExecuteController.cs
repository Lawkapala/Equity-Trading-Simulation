using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockStreet.DLL.RepositoryClass;
using System.Web.Http.Cors;using System.Threading;
namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BrokerExecuteController : ApiController
    {
        // GET: api/BrokerExecute

        public string Get()
        {
            BrokerSystem obj = new BrokerSystem();
            int status = obj.ThreadExecute();

            // BrokerSystem.Execute();

            if (status == 1)
            {
                return "Executed";
            }
            else
            {
                return "Error";
            }
                  
        }

        // GET: api/BrokerExecute/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BrokerExecute
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BrokerExecute/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BrokerExecute/5
        public void Delete(int id)
        {
        }
    }
}
