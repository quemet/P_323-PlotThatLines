using P_FUN_PlotThatLines;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace P_FUN_PlotThatLines.Tests
{
    [TestClass()]
    public class ApiTests
    {
        [TestMethod()]
        public void RefreshDateTest()
        {
            Api api = new Api();
            List<Currency> data = new List<Currency>() { new Currency(DateTime.Now.ToString("yyyy-MM-dd"), 1500f, 1600f, 1450f, 1400f, 1000, "CHF", "bitcoin") };
            DateTime d1 = DateTime.Now;
            DateTime d2 = DateTime.Now;

            var c = api.RefreshDate(data, d1, d2);

            Assert.IsNotNull(c);
            Assert.IsInstanceOfType(c, typeof(List<Currency>));
        }

        [TestMethod()]
        public void ReadPathTest()
        {
            Api api = new Api();
            string path = "../../../P_323-PlotThatLines/bitcoin.csv";
            string currency = "bitcoin";

            var c = api.ReadPath(path, currency);

            Assert.IsNotNull(c);
            Assert.IsInstanceOfType(c, typeof(List<Currency>));
            Assert.IsTrue(c.Count != 0);
        }

        [TestMethod()]
        public void ReturnCorrectFormatDateTest()
        {
            Api api = new Api();
            List<Currency> currencies = new List<Currency>() { new Currency(DateTime.Now.ToString("yyyy-MM-dd"), 1500f, 1600f, 1450f, 1400f, 1000, "CHF", "bitcoin") };
            string trueDate = DateTime.Now.ToOADate().ToString("yyyy-MM-dd");

            var d = api.ReturnCorrectFormatDate(currencies, 0);

            Assert.IsNotNull(d);
            Assert.IsInstanceOfType(d, typeof(double));
            Assert.IsTrue(d.ToString("yyyy-MM-dd") == trueDate);
        }
    }
}