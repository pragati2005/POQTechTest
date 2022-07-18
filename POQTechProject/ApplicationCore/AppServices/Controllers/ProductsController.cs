using DomainModels.Entities;
using Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace AppServices.Controllers
{
          
    public class ProductsController : ApiController
    {
        public IProductRepository repository;
   
        public ProductsController(IProductRepository _repository):base()
        {
            repository = _repository;
        }
        public  IEnumerable<Products> GetProducts()
        {
            return  repository.GetAllProducts();

        }
    }
}
