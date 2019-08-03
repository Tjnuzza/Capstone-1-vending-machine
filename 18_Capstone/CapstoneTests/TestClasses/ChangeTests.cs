using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class ChangeTests
    {
        [TestMethod]
        public void TestChangeReturn()
        {
            //  This has to be done as a TestMethod instead of a DataTestMethod
            //  DataRow doesn't like decimals, apparently because it's not a primitive type

            Change change = new Change();

            string message = change.TotalValue(0.0M);
            Assert.AreEqual("Vend-O-Matic 500 returned nothing.", message);

            message = change.TotalValue(0.5M);
            Assert.AreEqual("Vend-O-Matic 500 returned 2 quarters.", message);

            message = change.TotalValue(0.15M);
            Assert.AreEqual("Vend-O-Matic 500 returned 1 dimes 1 nickels.", message);

            message = change.TotalValue(0.07M);
            Assert.AreEqual("Vend-O-Matic 500 returned 1 nickels 2 pennies.", message);

            message = change.TotalValue(1.25M);
            Assert.AreEqual("Vend-O-Matic 500 returned 5 quarters.", message);
        }
    }
}