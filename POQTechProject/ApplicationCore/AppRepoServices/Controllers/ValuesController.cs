using DomainModels.Entities;
using Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AppRepoServices.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IProductRepository repository;

        public ValuesController(IProductRepository _repository) : base()
        {
            repository = _repository;
        }


        public IEnumerable<Products> GetProducts([FromUri] InputParameters inputParameters)
        {

            try
            {

                return repository.GetAllProductsByInputValues(inputParameters);

            }

            catch (Exception ex)
            {
                throw;
            }
            }
            

        
    }
}


