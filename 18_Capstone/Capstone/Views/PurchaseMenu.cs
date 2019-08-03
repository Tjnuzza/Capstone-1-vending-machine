using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Classes;
using Capstone.Views;

namespace Capstone.Classes
{
    public class PurchaseMenu : CLIMenu
    {
        /// <summary>
        /// The lump of change that our customer will receive upon finishing transaction
        /// </summary>
        private readonly Change TotalChange;

        public PurchaseMenu() : base()
        {
            this.menuOptions = new Dictionary<string, string>();
            this.Title = "*** Purchase Menu ***";
            this.menuOptions.Add("1", "Feed Money");
            this.menuOptions.Add("2", "Select Product");
            this.menuOptions.Add("3", "Finish Transaction");
            //this.menuOptions.Add("Q", "Back to Main Menu");
            this.TotalChange = new Change();
        }

        #region Methods
        protected override bool ExecuteSelection(string choice)
        {
            string selection = "";
            switch(choice)
            {
                case "1":// Feed money option
                    Console.Clear();
                    Console.WriteLine("*** Feed money ***");
                    Console.WriteLine($"*** Current balance: {VendOMatic.Balance:C} ***");
                    Console.WriteLine("1. Add 1¢");
                    Console.WriteLine("2. Add 5¢");
                    Console.WriteLine("3. Add 10¢");
                    Console.WriteLine("4. Add 25¢");
                    Console.WriteLine("5. Add $1");
                    Console.WriteLine("6. Add $2");
                    Console.WriteLine("7. Add $5");
                    Console.WriteLine("8. Add $10");
                    Console.WriteLine("Q. Quit");

                    selection = Console.ReadLine().Trim();
                    selection = selection.Substring(0, 1).ToUpper();
                    if (selection != "Q")
                    {
                        this.AddMoney(selection);
                    }
                    Console.WriteLine();
                    return true;

                case "2":// Purchase
                    VendOMatic.ShowContents();
                    Console.Write("Please enter a slot ID: ");
                    selection = Console.ReadLine().Trim();
                    this.MakePurchase(selection);
                    return true;

                case "3":// Finish transaction
                    //  Print out the change we recieve using our change object
                    Console.WriteLine(this.TotalChange.TotalValue(VendOMatic.Balance));

                    //  Restock the vending machine
                    Stocker stocker = new Stocker();
                    FileLog.Log("GIVE CHANGE", VendOMatic.Balance, 0.0M);
                    VendOMatic.Load(stocker.Restock());

                    //  Eat everything
                    this.Customer.Eat();
                    Console.ReadKey();
                    return false;

                case "Q":
                    return false;
            }// End switch
            return true;
        }// End ExecuteSelection

        private void AddMoney(string choice)
        {
            decimal startBalance = VendOMatic.Balance;
            switch (choice)
            {
                case "1":
                    VendOMatic.FeedMoney(0.01M);
                    break;
                case "2":
                    VendOMatic.FeedMoney(0.05M);
                    break;
                case "3":
                    VendOMatic.FeedMoney(0.1M);
                    break;
                case "4":
                    VendOMatic.FeedMoney(0.25M);
                    break;
                case "5":
                    VendOMatic.FeedMoney(1.0M);
                    break;
                case "6":
                    VendOMatic.FeedMoney(2.0M);
                    break;
                case "7":
                    VendOMatic.FeedMoney(5.0M);
                    break;
                case "8":
                    VendOMatic.FeedMoney(10.0M);
                    break;
            }
            FileLog.Log("FEED MONEY", startBalance, VendOMatic.Balance);
        }// End AddMoney

        private void MakePurchase(string selection)
        {
            decimal startBalance = VendOMatic.Balance;
            //  This is a placeholder Item
            //  If the slotID in selection is valid, this will be overwritten with that item's information
            Item purchaseItem = new Item("ID", "Item Name", 0.0M, "Item Category");
            string slotID = "";
            //  Using this variable to keep track of our transaction's validity
            //  Makes each 'if' statement decoupled from the others
            //  Otherwise we have a bunch of nested if/else statements that got super messy
            bool validTransaction = true;

            if (selection.Length < 2)
            {
                Console.WriteLine("Invalid slot ID, transaction denied.");
                validTransaction = false;
            }
            if (validTransaction)
            {
                slotID = selection.Substring(0, 2).ToUpper();
            }
            if (validTransaction && !VendOMatic.Stock.ContainsKey(slotID))
            {
                Console.WriteLine("Invalid slot ID, transaction denied.");
                validTransaction = false;
            }
            
            if (validTransaction && VendOMatic.Stock[slotID].Count < 1)
            {
                Console.WriteLine("This item is sold out.");
                validTransaction = false;
            }

            //  If validTransaction is still true, we know we have a valid key
            //  And that the item is not sold out
            if (validTransaction)
            {
                purchaseItem = VendOMatic.Stock[slotID][0];
            }
            if (validTransaction && VendOMatic.Balance < purchaseItem.Price)
            {
                Console.WriteLine("Insufficient funds, transaction denied.");
                validTransaction = false;
            }
            //  if validTransaction is still true here, then every condition above must also be true
            if (validTransaction)
            {
                Customer.Cart.Add(purchaseItem);
                VendOMatic.Purchase(slotID);

                FileLog.Log(purchaseItem.ItemName, startBalance, VendOMatic.Balance);
                FileLog.TransactionHistory(purchaseItem);

                Console.WriteLine($"You bought {purchaseItem.ItemName}. Enjoy!");
            }

            Console.ReadKey();

        }// End MakePurchase
        #endregion
    }
}