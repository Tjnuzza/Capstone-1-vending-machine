using System;
using System.Collections.Generic;
using System.Text;


namespace Capstone.Classes
{
    public class Item
    {
        #region Properties
        /// <summary>
        /// The slot ID of this item in our vending machine
        /// </summary>
        public string SlotID { get; }
        /// <summary>
        /// The name of the vending machine product
        /// </summary>
        public string ItemName { get; }
        /// <summary>
        /// The price of this item
        /// </summary>
        public decimal Price { get; }
        /// <summary>
        /// The category of this item (chip, candy, drink, gum)
        /// </summary>
        public string ItemCategory { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for Item, takes 4 arguments
        /// </summary>
        /// <param name="slotID">This item's slot ID</param>
        /// <param name="itemName">This Item's name</param>
        /// <param name="price">This item's price</param>
        /// <param name="itemCategory">This item's category (chip, candy, drink, gum)</param>
        public Item(string slotID, string itemName, decimal price, string itemCategory)
        {
            //  Format: A1|Potato Crisps|3.05|Chip
            this.SlotID = slotID;
            this.ItemCategory = itemCategory;
            this.ItemName = itemName;
            this.Price = price;
        }
        #endregion

        /// <summary>
        /// The end of the "eat" message when the customer eats the item.
        /// </summary>
        /// <returns>The string that the customer's item makes.</returns>
        public string Eat()
        {
            switch(this.ItemCategory)
            {
                case "Chip":
                    return "Crunch Crunch, Yum!";
                case "Candy":
                    return "Munch Munch, Yum!";
                case "Drink":
                    return "Glug Glug, Yum!";
                case "Gum":
                    return "Chew Chew, Yum!";
            }
            return "Yum!";
        }
    }
}