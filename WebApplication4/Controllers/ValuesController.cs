using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.Models;


namespace WebApplication4.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {

        private readonly ApplicationDbContext _context;

      
        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET api/values
        public IEnumerable<string> Get()
        {
            return _context.Notes.Select(x => x.Contenet);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
