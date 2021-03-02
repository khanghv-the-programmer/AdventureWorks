using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksBeta.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult SearchProductByCategoryAndSubCategory(string idSubString)
        {
            
            return View();
        }
    }
}
