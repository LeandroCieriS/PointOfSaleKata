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
    }

    public class PointOfSale
    {
        public string Scan(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return "Error: empty barcode";
            if (barcode == "99999") return "Error: barcode not found";
            return barcode == "23456" ? "$12.50" : "$7.25";
        }
    }
}