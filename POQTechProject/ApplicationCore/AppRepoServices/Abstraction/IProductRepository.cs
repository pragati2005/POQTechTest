using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModels.Entities;
using Repository.Implementation;

namespace Repository.Abstraction
{
  public interface IProductRepository 
    {
    
        IEnumerable<Products> GetAllProductsByInputValues(InputParameters inputParameters);
    }
}
