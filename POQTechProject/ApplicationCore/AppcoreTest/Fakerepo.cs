using DomainModels.Entities;
using Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppcoreTest
{
    public class Fakerepo : IProductRepository
    {
        List<Products> productslist;
        public Fakerepo()
        {

            List<string> sizes1 = new List<string>();
            sizes1.Add("small");
            sizes1.Add("medium");

            productslist = new List<Products>();
            {
                new Products
                {
                    price = 10,
                    description = "This trouser perfectly pairs with a green shirt",
                    sizes = sizes1,
                    title = "A Red Trouser"


                };
                productslist.Add(new Products { price = 20, description = "This trouser perfectly pairs with a red shirt", sizes = sizes1, title = "A green rouser" });
                productslist.Add(new Products { price = 30, description = "This trouser perfectly pairs with a red shirt", sizes = sizes1, title = "A green rouser" });


            }
        }
        IEnumerable<Products> IProductRepository.GetAllProductsByInputValues(InputParameters inputParameters)
        {
            List<Products> productlisttoreturn = new List<Products>();
            if (inputParameters.maxprice == 0 && inputParameters.minprice == 0)
            {
                productlisttoreturn = productslist;
            }
            if (inputParameters.maxprice > 0 && inputParameters.minprice == 0)
            {
                productlisttoreturn = productslist.Where(x => x.price < inputParameters.maxprice).ToList();
            }
            if (inputParameters.minprice > 0 && inputParameters.maxprice == 0)
            {
                productlisttoreturn = productslist.Where(x => x.price > inputParameters.minprice).ToList();
            }
            if (inputParameters.maxprice > 0 && inputParameters.minprice == 0)
            {
                productlisttoreturn = productslist.Where(x => x.price < inputParameters.maxprice && x.price > inputParameters.maxprice).ToList();
            }
            if(!String.IsNullOrEmpty(inputParameters.size))
            {
                if (productlisttoreturn == null || productlisttoreturn.Count == 0)
                {
                    productlisttoreturn = productslist;
                }

                List<Products> productlistbysizes = new List<Products>();
                List<Products> productlisttoremove = new List<Products>();

                String[] sizearray = inputParameters.size.Split(',');


                foreach (var checksizes in sizearray)
                {
                    productlistbysizes.AddRange(productlisttoreturn.Where(x => x.sizes.Contains(checksizes)).ToList());

                }
                var uniqueValues = productlistbysizes.GroupBy(pair => new { pair.sizes, pair.price, pair.title, pair.description })
                         .Select(group => group.First())
                         .ToDictionary(pair => new { pair.sizes, pair.price, pair.title, pair.description });
                productlisttoreturn = uniqueValues.Values.ToList();
            }

            //else
            //{
            //    productlisttoreturn = productslist;
            //}
            return productlisttoreturn;
        }

       
    }
    }

      