using DomainModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Abstraction
{
   public  interface IMockyDBContext
    {
      
        IEnumerable<Products> GetAllProductsByInputValues(InputParameters obj);


    }
}
