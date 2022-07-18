using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DomainModels;
using DomainModels.Entities;

namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> AllProducts()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:44367/api/Values/GetAllProducts");
            var products = await response.Content.ReadAsAsync<IEnumerable<Products>>();
            return View(products);
            
        }
    }
}