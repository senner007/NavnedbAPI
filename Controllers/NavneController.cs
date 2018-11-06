
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
        [HttpGet("{id:int}")]
        [Route("api/[controller]/{id}")]
        public ActionResult<Navne> GetId(int id)
        {
            NavnedbContext dbContext = new NavnedbContext();
            var navn = dbContext.Navne.Where(n => n.Id == id).FirstOrDefault();
            if (navn == null) return NotFound("Id not found");
            return Ok(navn);
        }


        [HttpGet("{navn}")]
        [Route("api/[controller]/{navn}")]
        public string GetName(string navn)

        {
            NavnedbContext dbContext = new NavnedbContext();
            var NameInDB = dbContext.Navne.Where(n => n.Navn == navn).FirstOrDefault();

            if (NameInDB == null) return "Ukendt navn";

            if (NameInDB.Køn == "mk") return "Unisex";

            if (NameInDB.Køn == "m") return "Dreng";

            else return "Pige";

        }
   
    }
    
}
