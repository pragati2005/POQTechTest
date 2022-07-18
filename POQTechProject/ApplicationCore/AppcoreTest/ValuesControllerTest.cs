using AppRepoServices.Controllers;
using DomainModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.Abstraction;
using Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AppcoreTest
{
   public class ValuesControllerTest
    {
        private readonly ValuesController _controller;
        private readonly IProductRepository _service;
        public ValuesControllerTest()
        {
            _service = new Fakerepo();
            _controller = new ValuesController(_service);

        }
        [Fact]
        public void GetByNoQueryParams_ReturnAllRecords()
        {
            InputParameters inputParameters = new InputParameters();

            var okResult = _controller.GetProducts(inputParameters) ;
            // Assert

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(IEnumerable<Products>));
            Assert.Equals(3, okResult.ToList().Count);
           
        }
        [Fact]
        public void GetBypriceParams_returns_betweenminandmax()
        {
            InputParameters inputParameters = new InputParameters() { minprice = 1 };
            InputParameters inputParameters2 = new InputParameters() { maxprice = 30};
            InputParameters inputParameters3 = new InputParameters() { minprice=10,maxprice = 30 };
            // Act
            var FoundResultbymin = _controller.GetProducts(inputParameters);
            var foundresultbymax = _controller.GetProducts(inputParameters);
            var foundbetweenminmax= _controller.GetProducts(inputParameters3);

            // Assert
            Assert.IsInstanceOfType(FoundResultbymin, typeof(IEnumerable<Products>));
            Assert.IsNotNull(FoundResultbymin);
            Assert.IsNotNull(foundresultbymax);
            Assert.IsNotNull(foundbetweenminmax);
        }
        [Fact]
        public void GetBySizes_returns_notfoundresult()
        {
            InputParameters inputParameters = new InputParameters() { minprice = 1 };

            // Act
            var notFoundResult = _controller.GetProducts(inputParameters);
            // Assert
            Assert.IsInstanceOfType(notFoundResult, typeof(IEnumerable<Products>));
        }
    }
}
