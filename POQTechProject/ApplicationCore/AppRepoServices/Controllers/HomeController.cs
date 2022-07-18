using DomainModels.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AppRepoServices.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();

        }
        public ActionResult GetAllProducts([FromUri] InputParameters inputParameters,String Apikey= "0c4bbda1-bf7b-479d-b619-83a1df21f4e7")
        {
            
            try
            {
                if (Apikey == ConfigurationManager.AppSettings["ApiKey"] || Apikey == ConfigurationManager.AppSettings["ApiKey2"])
                {
                    var client = new HttpClient();

                    var inputuri = ConfigurationManager.AppSettings["localhostURL"] + "?minprice=" + inputParameters.minprice + "&maxprice=" + inputParameters.maxprice + "&size=" + inputParameters.size + "&commonwords=" + inputParameters.commonwords + "&ApiKey=" + Apikey;
                    var response = client.GetAsync(inputuri).Result;
                    var products = response.Content.ReadAsAsync<IEnumerable<Products>>().Result;
                    ViewBag.count = products.Count();
                    return View(products);
                }
                else
                {
                    throw new Exception("Unathorised : " + "Please provide a valid key");
                }
            }
            catch(Exception ex)
            {
                
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                ViewBag.ErrorMessage = ex.Message;
                return View("~/Views/Shared/Error.cshtml"); 
                    
                    
                    }


        }
    }
}