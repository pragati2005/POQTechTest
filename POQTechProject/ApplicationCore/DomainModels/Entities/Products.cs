using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class Root
    {
        public List<Products> products { get; set; }
        public ApiKeys apiKeys { get; set; }
    }
    public class Products
    {
        public string title { get; set; }
        public int price { get; set; }
        public List<string> sizes { get; set; }
        public string description { get; set; }
    }
}
