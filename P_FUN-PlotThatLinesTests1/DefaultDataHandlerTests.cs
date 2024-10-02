using Microsoft.VisualStudio.TestTools.UnitTesting;
using P_FUN_PlotThatLines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_FUN_PlotThatLines.Tests
{
    [TestClass()]
    public class DefaultDataHandlerTests
    {
        DefaultDataHandler handler = new();

        [TestMethod()]
        public void RefreshDateTest()
        {
            handler = handler.InitializeObjectTest(handler);

            List<Currency> c = handler.RefreshDate(handler.bitcoin, handler.bitcoin.First()._date, handler.bitcoin[5]._date);

            Assert.IsNotNull(c);
            Assert.AreNotEqual(0, c.Count);
        }

        [TestMethod()]
        public void ChangeDateTest()
        {
            handler = handler.InitializeObjectTest(handler);

            handler.ChangeDate(new object(), new EventArgs());
        }

        [TestMethod()]
        public void ReadPathTest()
        {
            handler = handler.InitializeObjectTest(handler);

            List<Currency> c = handler.ReadPath("../../bitcoin.csv", "bitcoin");

            Assert.IsNotNull(c);
            Assert.AreNotEqual(0, c.Count);
            Assert.IsInstanceOfType(c[0], typeof(Currency));
        }

        [TestMethod()]
        public void ReturnCorrectFormatDateTest()
        {
            handler = handler.InitializeObjectTest(handler);

            double date = handler.ReturnCorrectFormatDate(handler.bitcoin, 0);

            Assert.IsNotNull(date);
            Assert.IsInstanceOfType(date, typeof(double));
        }
    }
}