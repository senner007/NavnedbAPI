
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NavnedbAPI.Models;

namespace NavnedbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavneController : ControllerBase
    {
        // GET api/navne
        [HttpGet]
        public ActionResult<IEnumerable<Navne>> Get(string startsWith = "", string sex = "")
        {
            NavnedbContext dbContext = new NavnedbContext();
            // TODO : improve me!
            if (startsWith == "" && sex == "") {
                // Add total count to header
                Request.HttpContext.Response.Headers.Add("Navne-Total-Count", dbContext.Navne.Count().ToString());
                return Ok(dbContext.Navne);
            }
            var navne = dbContext.Navne.Where(n => (n.Navn.StartsWith(startsWith) || startsWith == "") && (n.Køn == sex || sex == "")).Select(s => new { Navn = s.Navn, Køn = s.Køn});
            return Ok(navne);
        }

        // GET api/navne/1
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            NavnedbContext dbContext = new NavnedbContext();
            var navn = dbContext.Navne.Where(n => n.Id == id);
            return Ok(navn);
        }
   
    }
}
