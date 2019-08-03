using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace Capstone.Classes
{
    public class AuditLog
    {
        public string FilePath;

        /// <summary>
        /// This function appends one line to Log.txt containing the relevant information
        /// </summary>
        /// <param name="transaction">The name of the transaction, possibly the item that was purchased</param>
        /// <param name="startBalance">The vending machine balance before the transaction</param>
        /// <param name="endBalance">The vending machine balance after the transaction</param>
        public void Log(string transaction, decimal startBalance, decimal endBalance)
        {
            this.FilePath = Path.GetFullPath(".");
            this.FilePath = @"..\..\..\..\etc\Log.txt";
            using (StreamWriter sw = new StreamWriter(FilePath, true))
            {
                sw.WriteLine($"{DateTime.Now} {transaction} {startBalance:C} {endBalance:C}");
            }
        }

        /// <summary>
        /// Logs entry in Transaction History, called every time the customer buys something
        /// </summary>
        /// <param name="purchase">The item that was purchased, will be added to the file</param>
        public void TransactionHistory(Item purchase)
        {
            //  Path to TransactionHistory.txt
            this.FilePath = Path.GetFullPath(".");
            this.FilePath = @"..\..\..\..\etc\TransactionHistory.txt";

            //  This dictionary will hold everything in memory so we can rewrite the entire file
            //  Quantities of items will be held here as decimals and cast to ints later
            Dictionary<string, decimal> history = new Dictionary<string, decimal>();
            
            //  Read everything into our dictionary, increment happens during write phase
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while(!sr.EndOfStream)
                {
                    string input = sr.ReadLine();
                    //  Total sales line has different rules
                    if (input.StartsWith('*'))
                    {
                        string[] info = input.Split(' ');
                        decimal total = decimal.Parse(info[2]);
                        history.Add("**TOTAL SALES**$", total + purchase.Price);
                    }
                    else//  Item lines
                    {
                        string[] info = input.Split('|');
                        decimal total = decimal.Parse(info[1]);
                        history.Add(info[0], total);
                    }
                }
            }// End file read

            //  Write to TransactionHistory
            //  Most lines will be the same after the fact
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                foreach(KeyValuePair<string, decimal> entry in history)
                {
                    if(entry.Key.StartsWith("*"))// The sales line, which needs to be treated differently
                    {
                        sw.WriteLine($"{entry.Key} {entry.Value}");
                    }
                    else if (entry.Key == purchase.ItemName)//  The item that was purchased, count needs to be incremented by one
                    {
                        sw.WriteLine($"{entry.Key}|{(int)entry.Value + 1}");
                    }
                    else//  This line will be the same as before
                    {
                        sw.WriteLine($"{entry.Key}|{(int)entry.Value}");
                    }
                }
            }// End file write
        }// End TransactionHistory
    }
}