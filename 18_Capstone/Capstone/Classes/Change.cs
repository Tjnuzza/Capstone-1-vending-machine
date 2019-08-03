using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Change 
    {
        /// <summary>
        /// Returns a string that shows how much change the customer received
        /// </summary>
        /// <param name="balance">The starting balance of the vending machine, which we use to make change</param>
        /// <returns>The customer's change, in quarters, dimes, nickels, and pennies</returns>
        public string TotalValue(decimal balance)
        {
            if (balance == 0.0M)
            {
                return "Vend-O-Matic 500 returned nothing.";
            }
            else//  Balance should never be < 0
            {
                string message = "Vend-O-Matic 500 returned";

                int coins = (int)(balance / 0.25M);
                if (coins > 0)
                {
                    message += $" {coins} quarters";
                    balance %= 0.25M;
                }
                coins = (int)(balance / 0.10M);
                if (coins > 0)
                {
                    message += $" {coins} dimes";
                    balance %= 0.10M;
                }
                coins = (int)(balance / 0.05M);
                if (coins > 0)
                {
                    message += $" {coins} nickels";
                    balance %= 0.05M;
                }
                coins = (int)(balance / 0.01M);
                if (coins > 0)
                {
                    message += $" {coins} pennies";
                    balance %= 0.01M;
                }
                message += ".";
                return message;
            }
        }
    }
}