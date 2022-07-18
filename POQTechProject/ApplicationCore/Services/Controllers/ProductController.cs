using DomainModels.Entities;
using Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Services.Controllers
{
    
    [Route("{controller}/{action}")]
    public class ProductController : Controller
    {

        public IProductRepository repository;
        public ProductController(IProductRepository _repository)
        {
            repository = _repository;
        }
        public async Task<IEnumerable<Products>> GetProducts()
        {
           return await repository.GetAllProducts();
            
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}