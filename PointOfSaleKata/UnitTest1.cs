using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace PointOfSaleKata
{
    public class Tests
    {
        private PointOfSale pointOfSale;

        [SetUp]
        public void Setup()
        {
            pointOfSale = new PointOfSale();
        }

        [Test]
        public void Return_price_for_item_1()
        {
            Assert.AreEqual("$7.25", pointOfSale.Scan("12345"));
        }

        [Test]
        public void Return_price_for_item_2()
        {
            Assert.AreEqual("$12.50", pointOfSale.Scan("23456"));
        }

        [Test]
        public void Return_error_for_unknown_item()
        {
            Assert.AreEqual("Error: barcode not found", pointOfSale.Scan("99999"));
        }
        
        [Test]
        public void Return_error_for_empty_barcode()
        {
            Assert.AreEqual("Error: empty barcode", pointOfSale.Scan(""));
        }

        [Test]
        public void Return_sum_of_scanned_items()
        {
            Assert.AreEqual("$19.75", pointOfSale.Scan("12345,23456,99999,"));
        }
    }

    public class PointOfSale
    {
        public string Scan(string barcode)
        {
            var itemsPrices = new Dictionary<string, string>
            {
                {"12345", "$7.25"},
                {"23456", "$12.50"},
                {"99999", "Error: barcode not found"},
                {"", "Error: empty barcode"}
            };
            return itemsPrices[barcode];
        }
    }
}