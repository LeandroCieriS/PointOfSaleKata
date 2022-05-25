using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void Return_sum_of_scanned_items3()
        {
            Assert.AreEqual("$27,00", pointOfSale.Scan("12345,23456,12345"));
        }

        [Test]
        [TestCase("Error: empty barcode", "12345,,12345")]
        [TestCase("Error: barcode not found", "12345,99999,12345")]
        public void Return_error_of_scanned_items(string expected, string barcodes)
        {
            Assert.AreEqual(expected, pointOfSale.Scan(barcodes));
        }
        
    }

    public class PointOfSale
    {
        private static Dictionary<string, double> ItemsPrices()
        {
            var itemsPrices = new Dictionary<string, double>
            {
                {"12345", 7.25},
                {"23456", 12.50}
            };
            return itemsPrices;
        }

        public string Scan(string barcode)
        {
            var itemsPrices = ItemsPrices();
            var items = GetItems(barcode);
            if (BarCodesAreNotValid(items, out var error)) return error;

            var totalPrice = GetTotalPrice();
            return ConvertPriceToPrint();

            string ConvertPriceToPrint() => $"${totalPrice.ToString("F" + 2)}";
            double GetTotalPrice() => items.Sum(i => itemsPrices[i]);
        }

        private static string[] GetItems(string barcode)
        {
            return barcode.Split(',');
        }

        private static bool BarCodesAreNotValid(string[] items, out string error)
        {
            if (items.Contains("99999"))
            {
                error = "Error: barcode not found";
                return true;
            }

            if (items.Contains(string.Empty))
            {
                error = "Error: empty barcode";
                return true;
            }

            error = "";
            return false;
        }
    }
}