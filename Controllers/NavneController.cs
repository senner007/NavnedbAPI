using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NavnedbAPI.Models;

namespace NavnedbAPI.Controllers
{
    // https://stackoverflow.com/questions/43895485/required-query-string-parameter-in-asp-net-core
    public class QueryParameters
    {
        [StringLength(50)]
        public string startsWith { get; set; } = "";
        [StringLength(2)]
        public string gender { get; set; } = "";
        public uint take { get; set; } = int.MaxValue;
        public uint skip { get; set; } = 0;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class NavneController : ControllerBase
    {
        
        // GET api/navne
        [HttpGet]
        public ActionResult<IEnumerable<Navne>> Get([FromQuery] QueryParameters parameters)
        {
            NavnedbContext dbContext = new NavnedbContext();
            // TODO : improve me!
            if (String.IsNullOrEmpty(parameters.startsWith) && String.IsNullOrEmpty(parameters.gender)) {
                // Add total count to header
                Request.HttpContext.Response.Headers.Add("Navne-Total-Count", dbContext.Navne.Count().ToString());
                return Ok(dbContext.Navne.Skip((int)parameters.skip).Take((int)parameters.take));
            }
            var navne = dbContext.Navne.Where(n => (n.Navn.StartsWith(parameters.startsWith) && (n.Køn == parameters.gender || parameters.gender == ""))).Select(s => new { Navn = s.Navn, Køn = s.Køn});
            return Ok(navne.Skip((int)parameters.skip).Take((int)parameters.take));
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
