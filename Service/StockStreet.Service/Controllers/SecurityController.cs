using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StockStreet.DLL;
using StockStreet.DLL.RepositoryClass;
using System.Data.Entity;
using StockStreet.DLL.EntityClass;
using System.Web.Http.Cors;
using StockStreet.DLL.RepositoryInterface;

namespace StockStreet.Service.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SecurityController : ApiController
    {
        StockStExternalEntities1 context = new StockStExternalEntities1();
        // GET: api/Security
        //When Broker logs in ,His first page
        public IEnumerable<BrokerSecurity> Get()
        {
            IBroker<BrokerSecurity> b = new Broker<BrokerSecurity>(context);
            return b.GetAll();
        }

        [HttpGet]
        // GET: api/Security/5
        //Portfolio manager searches and calls this function (verify)
       public BrokerSecurity Get(int id)
        {
            IBroker<BrokerSecurity> b = new Broker<BrokerSecurity>(context);
            return b.Get(id);
        }

        [HttpPost]

        // POST: api/Security
        public void Post(BrokerSecurity brData)
        {
            IBroker<BrokerSecurity> b = new Broker<BrokerSecurity>(context);
            b.Add(brData);
        }

        [HttpPut]
        // PUT: api/Security/5
        public void Put(BrokerSecurity brData)
        {
            IBroker<BrokerSecurity> b = new Broker<BrokerSecurity>(context);
            b.Edit(brData);
            context.SaveChanges();
        }

        [HttpDelete]
        // DELETE: api/Security/5
        public void Delete(int id)
        {
            IBroker<BrokerSecurity> b = new Broker<BrokerSecurity>(context);
            b.Remove(b.Get(id));
            context.SaveChanges();
        }
    }
}
