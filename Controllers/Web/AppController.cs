using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSNumberTumbler.Controllers.Web
{
    public class AppController : Controller
    {
        public IActionResult Index(int? id = null)
        {
            return View();
        }

        public IActionResult AddNumberSet()
        {
            return View();
        }
      
    }
}
