using DomainModels.Entities;
using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
//using System.Text.Json;
using System.Threading.Tasks;

namespace Repository
{
    public class MockyDBContext : IMockyDBContext
    {
        private readonly HttpClient httpclient;
        //private readonly IConfiguration configuration;

        public MockyDBContext()
        {
            httpclient = new HttpClient();
          
        }
        public IEnumerable<Products> GetAllProductsByInputValues(InputParameters obj)
        {

            try
            {
                var requesturi = new Uri(ConfigurationManager.AppSettings["MockyAPIURL"]);

                httpclient.BaseAddress = new Uri(ConfigurationManager.AppSettings["MockyAPIURL"]);

                //var httpResponseMessage2 = await httpclient.GetStringAsync(requesturi);
                var httpResponseMessage = httpclient.GetAsync(requesturi);
                httpResponseMessage.Wait();

                List<Products> ProductListtoreturn = new List<Products>();
                var result = httpResponseMessage.Result;
                if (obj != null)
                {
                    var ProductList = (result.Content.ReadAsAsync<Root>()).Result;

                    if (obj.minprice == 0 && obj.maxprice == 0 && String.IsNullOrEmpty(obj.size) && String.IsNullOrEmpty(obj.commonwords))
                    {
                        if (ProductListtoreturn == null || ProductListtoreturn.Count == 0)
                        {
                            ProductListtoreturn = ProductList.products;
                        }

                        //return (IEnumerable<Products>)ProductListtoreturn;
                    }
                    if (obj.maxprice > 0 && obj.minprice == 0)
                    {

                        if (ProductListtoreturn == null || ProductListtoreturn.Count == 0)
                        {
                            ProductListtoreturn = ProductList.products;
                        }

                        ProductListtoreturn = ProductListtoreturn.Where(x => x.price <= obj.maxprice).ToList();
                        // return (IEnumerable<Products>)ProductListtoreturn.Where(x => x.price > obj.minprice && x.price < obj.maxprice).ToList();

                    }
                    if (obj.minprice > 0 && obj.maxprice == 0)
                    {

                        if (ProductListtoreturn == null || ProductListtoreturn.Count == 0)
                        {
                            ProductListtoreturn = ProductList.products;
                        }

                        ProductListtoreturn = ProductListtoreturn.Where(x => x.price > obj.minprice).ToList();
                        // return (IEnumerable<Products>)ProductListtoreturn.Where(x => x.price > obj.minprice && x.price < obj.maxprice).ToList();

                    }
                    if (obj.minprice > 0 && obj.maxprice > 0)
                    {

                        if (ProductListtoreturn == null || ProductListtoreturn.Count == 0)
                        {
                            ProductListtoreturn = ProductList.products;
                        }

                        ProductListtoreturn = ProductListtoreturn.Where(x => x.price > obj.minprice && x.price < obj.maxprice).ToList();
                        // return (IEnumerable<Products>)ProductListtoreturn.Where(x => x.price > obj.minprice && x.price < obj.maxprice).ToList();

                    }
                    if (!String.IsNullOrEmpty(obj.size))
                    {
                        if (ProductListtoreturn == null || ProductListtoreturn.Count == 0)
                        {
                            ProductListtoreturn = ProductList.products;
                        }

                        List<Products> productlistbysizes = new List<Products>();
                        List<Products> productlisttoremove = new List<Products>();

                        String[] sizearray = obj.size.Split(',');


                        foreach (var checksizes in sizearray)
                        {
                            productlistbysizes.AddRange(ProductListtoreturn.Where(x => x.sizes.Contains(checksizes)).ToList());

                        }
                        var uniqueValues = productlistbysizes.GroupBy(pair => new { pair.sizes, pair.price, pair.title, pair.description })
                                 .Select(group => group.First())
                                 .ToDictionary(pair => new { pair.sizes, pair.price, pair.title, pair.description });
                        ProductListtoreturn = uniqueValues.Values.ToList();

                        //  return uniqueValues.Values.ToList();
                    }

                    if (!String.IsNullOrEmpty(obj.commonwords))
                    {
                        if (ProductListtoreturn == null || ProductListtoreturn.Count == 0)
                        {
                            ProductListtoreturn = ProductList.products;

                        }

                        List<Products> productlwithrequiredcommonwords = new List<Products>();
                        List<Products> productlisttoremove = new List<Products>();


                        Dictionary<string, int> checkcount = new Dictionary<string, int>();
                        List<String> description = new List<String>();


                        foreach (var product in ProductListtoreturn)
                        {
                            description.AddRange(product.description.Split(' '));

                        }
                        foreach (var value in (description.GroupBy(x => x)
                                 .Select(group => group.First())
                                 .ToList()))
                        {
                            checkcount[value] = 0;
                        }

                        foreach (var product in ProductListtoreturn)
                        {
                            foreach (var val in checkcount.ToList())
                            {
                                if (product.description.Contains(val.Key))
                                {
                                    checkcount[val.Key] = checkcount[val.Key] + 1;
                                }
                            }
                        }
                        String str = "";
                        var requiredmostcommonwords = ((checkcount.OrderByDescending(x => x.Value).Take(10))).OrderBy(x => x.Value).Take(5);
                        //var requiredmostcommonwords2= (Dictionary<String, int>)requiredmostcommonwords.OrderBy(x=>x.Value).Take(5);
                        foreach (var product in ProductListtoreturn)
                        {
                            foreach (var val in requiredmostcommonwords)
                            {
                                if (product.description.Contains(val.Key))
                                {
                                    productlwithrequiredcommonwords.Add(product);
                                }
                            }

                        }

                        var uniqueValues = productlwithrequiredcommonwords.GroupBy(pair => new { pair.sizes, pair.price, pair.title, pair.description })
                               .Select(group => group.First())
                               .ToDictionary(pair => new { pair.sizes, pair.price, pair.title, pair.description });
                        ProductListtoreturn = uniqueValues.Values.ToList();


                    }


                }

                //else
                //{
                //    var ProductList = (result.Content.ReadAsAsync<Root>()).Result;
                //    ProductListtoreturn = ProductList.products;

                //}
                // String inputparamsarray = Inputparams.Split(',');
                return (IEnumerable<Products>)ProductListtoreturn;
            }catch(Exception ex)
            {
                 
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                throw;
            }
        }
        



    }
}