using DomainModels.Entities;
//using Microsoft.Extensions.Configuration;
using Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
       
        private readonly IMockyDBContext mockyDBContext;
        public ProductRepository(IMockyDBContext _mockyDBContext)
        {
            mockyDBContext = _mockyDBContext;
        }
       public IEnumerable<Products> GetAllProductsByInputValues(InputParameters inputParameters)
        {

            InputParameters obj = new InputParameters();
            obj = inputParameters;
            
            

            var products = mockyDBContext.GetAllProductsByInputValues(obj);
            return products;
        }
    




    }
}
