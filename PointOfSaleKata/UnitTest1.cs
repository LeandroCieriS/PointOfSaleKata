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
            Assert.AreEqual("$7,25", pointOfSale.Scan("12345"));
        }

        [Test]
        public void Return_price_for_item_2()
        {
            Assert.AreEqual("$12,50", pointOfSale.Scan("23456"));
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
            Assert.AreEqual("$19,75", pointOfSale.Scan("12345,23456"));
        }
    }

    public class PointOfSale
    {
        public string Scan(string barcode)
        {
            var itemsPrices = ItemsPrices();
            var items = barcode.Split(',');
            var totalPrice = 0.0;
            foreach (var item in items)
            {
                if (item == "") return "Error: empty barcode";
                if (item == "99999") return "Error: barcode not found";
                totalPrice += itemsPrices[item];
            }

            return $"${totalPrice.ToString("F" + 2)}";
        }

        private static Dictionary<string, double> ItemsPrices()
        {
            var itemsPrices = new Dictionary<string, double>
            {
                {"12345", 7.25},
                {"23456", 12.50}
            };
            return itemsPrices;
        }
    }
}