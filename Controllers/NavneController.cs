
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NavnedbAPI.Models;

namespace NavnedbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavneController : ControllerBase
    {
        // GET api/values
        // TODO: rename controller
         [HttpGet]
        public ActionResult<IEnumerable<Navne>> Get(string startsWith = "", string sex = "")
        {
            NavnedbContext dbContext = new NavnedbContext();
            // TODO : improve me!
            var navne = dbContext.Navne.Where(n => (n.Navn.StartsWith(startsWith) || startsWith == "") && (n.Køn == sex || sex == "")).Select(s => new Navne() { Navn = s.Navn, Køn = s.Køn});
            return Ok(navne);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            NavnedbContext dbContext = new NavnedbContext();
            var navn = dbContext.Navne.Where(n => n.Id == id);
            return Ok(navn);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
