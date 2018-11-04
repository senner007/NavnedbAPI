using System;
using Microsoft.AspNetCore.Mvc;

namespace NavnedbAPI.Controllers
{

    public class HomeController : Controller
    {
        // GET api/values
         [HttpGet]
        public IActionResult Index()
        {
            // NavnedbContext dbContext = new NavnedbContext();
            // var navne = dbContext.Navne.Where(n => (n.Navn.StartsWith(startsWith)) && n.Køn == sex).Select(s => new Navne() { Navn = s.Navn, Køn = s.Køn});
            // return Ok(navne);
            return View();
        }

    
    }
}
