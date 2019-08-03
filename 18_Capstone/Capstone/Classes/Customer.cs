using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Customer
    {
        /// <summary>
        /// This represents every item that the customer has purchased
        /// </summary>
        public List<Item> Cart;

        public Customer()
        {
            this.Cart = new List<Item>();
      
        }

        /// <summary>
        /// The message when the customer eats the item. Customer eats everything at end of transaction
        /// </summary>
        public void Eat()
        {
            foreach (Item item in Cart)
            {
                Console.WriteLine($"You eat the {item.ItemName}. {item.Eat()}");
            }
        }
    }
}