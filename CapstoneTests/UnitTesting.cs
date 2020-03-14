using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using Capstone.Classes;

namespace CapstoneTest
{
    [TestClass]
    public class UnitTesting
    {
        VendingMachine test;
        VendingMachineItem testItem;

        [TestInitialize]
        public void Initialize()
        {
            test = new VendingMachine();
            testItem = new VendingMachineItem();
        }

        [TestMethod]
        public void FeedMoneyTest()
        {
            test.FeedMoney("5");
            Assert.AreEqual(5, test.Balance);
            test.FeedMoney("5");
            Assert.AreEqual(10, test.Balance);
            test.FeedMoney("3");
            Assert.AreEqual(10, test.Balance);
        }

        [TestMethod]
        public void ReturnBalanceTest()
        {
            test.Balance = 10;
            Assert.AreEqual(10, test.ReturnBalance());
        }

        [TestMethod]
        public void FinishTransactionTest()
        {
            test.Balance = 1;
            Assert.AreEqual("Your change is 4 quarter(s), 0 dime(s), and 0 nickel(s).", test.FinishTransaction());
            test.Balance = 5.70M;
            Assert.AreEqual("Your change is 22 quarter(s), 2 dime(s), and 0 nickel(s).", test.FinishTransaction());
            test.Balance = 2.40M;
            Assert.AreEqual("Your change is 9 quarter(s), 1 dime(s), and 1 nickel(s).", test.FinishTransaction());
        }

        [TestMethod]
        public void SelectItemTest()
        {
            test.AddItems();

            test.SelectItem("C3");
            Assert.AreEqual("Mountain Melter", test.TempSelection.Name);

            test.SelectItem("d2");
            Assert.AreEqual("Little League Chew", test.TempSelection.Name);
        }

        [TestMethod]
        public void SelectItemBalanceTest()
        {
            test.AddItems();

            test.Balance = 5;
            test.SelectItem("b4");
            Assert.AreEqual(3.25M, test.Balance);
        }

        [TestMethod]
        public void SelectItemErrorTest()
        {            
            test.AddItems();

            test.SelectItem("b5");
            Assert.AreEqual("Invalid Selection", test.SelectionResult);
            
            test.Balance = 1;
            test.SelectItem("a1");
            Assert.AreEqual("Insufficient Funds", test.SelectionResult);
        }
    }
}
