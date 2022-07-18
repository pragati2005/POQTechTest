using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
   public class InputParameters
    {
        public int minprice { get; set; }
        public int maxprice { get; set; }
        public String size { get; set; }
        public String commonwords { get; set; }
    }
}
