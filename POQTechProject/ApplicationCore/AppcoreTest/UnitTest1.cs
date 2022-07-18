using AppRepoServices.Controllers;
using DomainModels.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Abstraction;
using System;
using System.Collections.Generic;

namespace AppcoreTest
{
    [TestClass]
    public class UnitTest1
    {
        private readonly ValuesController _controller;
        private readonly IProductRepository _service;
        public UnitTest1()
        {
            _service = new Fakerepo();
            _controller = new ValuesController(_service);

        }
        [TestMethod]
        public void GetByNoQueryParams_ReturnAllRecords()
        {
            InputParameters inputParameters = new InputParameters();
            int counter = 0;
            var okResult = _controller.GetProducts(inputParameters);
            foreach (var val in okResult)
            {
                counter++;
            }

            // Assert

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult, typeof(IEnumerable<Products>));
            

        }
        [TestMethod]
        public void GetBypriceParams_returns_betweenminandmax()
        {
            InputParameters inputParameters = new InputParameters() { minprice = 1 };
            InputParameters inputParameters2 = new InputParameters() { maxprice = 30 };
            InputParameters inputParameters3 = new InputParameters() { minprice = 10, maxprice = 30 };
            // Act
            var FoundResultbymin = _controller.GetProducts(inputParameters);
            var foundresultbymax = _controller.GetProducts(inputParameters);
            var foundbetweenminmax = _controller.GetProducts(inputParameters3);

            // Assert
            Assert.IsInstanceOfType(FoundResultbymin, typeof(IEnumerable<Products>));
            Assert.IsNotNull(FoundResultbymin);
            Assert.IsNotNull(foundresultbymax);
            Assert.IsNotNull(foundbetweenminmax);
        }
        [TestMethod]
        public void GetBySizes_returns_notfoundresult()
        {
            String sizes = "large";
            String sizes2 = "small,medium";
            String size3 = "XXL";


            InputParameters inputParameters = new InputParameters() { size = sizes };
            InputParameters inputParameters2 = new InputParameters() { size = sizes2 };
            InputParameters inputParameters3 = new InputParameters() { size = size3 };

            // Act
            var FoundResult = _controller.GetProducts(inputParameters);
            var FoundResult2 = _controller.GetProducts(inputParameters2);
            var FoundResult3 = _controller.GetProducts(inputParameters3);
            // Assert
            Assert.AreNotEqual(null,FoundResult.ToString());
            Assert.IsNotNull(FoundResult2);
            Assert.AreNotEqual(null, FoundResult3.ToString());

        }

    }
}

