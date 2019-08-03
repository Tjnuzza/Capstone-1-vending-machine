using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            VendingMachine vendingMachine = new VendingMachine();
            Assert.AreEqual(vendingMachine.Balance, 0.0M);
        }

        [TestMethod]
        public void TestFeedMoney()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.FeedMoney(5.0M);
            Assert.AreEqual(5.0m, vendingMachine.Balance);

            vendingMachine.FeedMoney(3.0M);
            Assert.AreEqual(8.0m, vendingMachine.Balance);

            vendingMachine.FeedMoney(3.0M);
            Assert.AreEqual(11.0m, vendingMachine.Balance);
        }

        [TestMethod]
        public void TestPurchase()
        {
            Stocker stocker = new Stocker();
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.Load(stocker.Restock());
            vendingMachine.FeedMoney(100.0m);

            //  Price of A1 = 3.05
            vendingMachine.Purchase("A1");
            Assert.AreEqual(96.95M, vendingMachine.Balance);
            Assert.AreEqual(4, vendingMachine.Stock["A1"].Count);

            vendingMachine.Purchase("A1");
            Assert.AreEqual(93.9M, vendingMachine.Balance);
            Assert.AreEqual(3, vendingMachine.Stock["A1"].Count);
        }
    }
}