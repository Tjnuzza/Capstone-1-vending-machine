using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class VendingMachine 
    {
        public Dictionary <string,List<Item>> Stock { get; private set; }
        public decimal Balance { get; private set; } = 0;
       
        public void Load(Dictionary<string, List<Item>> items)
        {
            this.Stock = items;
            this.Balance = 0;
        }

        #region Methods

        /// <summary>
        /// Print the contents of the vending machine
        /// </summary>
        public void ShowContents()
        {
            Console.Clear();
            foreach (KeyValuePair<string, List<Item>> product in this.Stock)
            {
                Console.Write($"{product.Key}\t");
                if(product.Value.Count == 0)
                {
                    Console.WriteLine("*** SOLD OUT ***");
                }
                else
                {
                    Console.Write($"{product.Value[0].ItemCategory}\t");
                    Console.Write($"{product.Value[0].ItemName}\t\t");
                    //  Tabs to make sure everything is lined up correctly
                    if(product.Value[0].ItemName.Length < 16)
                    {
                        Console.Write("\t");
                        if (product.Value[0].ItemName.Length < 8)
                        {
                            Console.Write("\t");
                        }
                    }
                    //  Available quantity and price
                    Console.WriteLine($"{product.Value.Count}\t{product.Value[0].Price:C}");
                }
            }
        }

        /// <summary>
        /// Add money to the machine
        /// </summary>
        /// <param name="money"></param>
        public void FeedMoney(decimal money)
        {
            this.Balance += money;
        }

        /// <summary>
        /// Remove money from balance and dispense the requested item
        /// </summary>
        /// <param name="slotID">Slot to dispense from, key of the item the customer wants</param>
        public void Purchase(string slotID)
        {
            this.Balance -= this.Stock[slotID][0].Price;
            this.Stock[slotID].RemoveAt(0);
        }
        #endregion
    }
}