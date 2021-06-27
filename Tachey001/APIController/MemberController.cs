using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tachey001.APIController
{
    public class MemberController : ApiController
    {
        // GET: api/Member
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Member/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Member
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Member/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Member/5
        public void Delete(int id)
        {
        }
    }
}
