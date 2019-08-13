# Capstone 1: Vending Machine
First capstone at Tech Elevator. Simulates vending machine software in a command line program.

## Overview

This is the first capstone that I made at Tech Elevator. My partner shall remain nameless as I have yet to ask permission to give his name. It was the capstone to the first module, which focused on the C# language and the .Net framework. This module featured programming fundamentals, control structures, principles of object oriented programming, and file I/O.

## How it works
The entire program works within the command line. At this point in the program, we had yet to learn anything else. The first menu allows the user to begin purchasing, see the inventory, and check out. The purchase menu allows the user to purchase snacks and add money to the machine. Money must be added in standard denominations. Upon checkout, the user receives change and a message for eating each item they bought.

All persistent information is held in plain text files. These include the standard inventory and a history of all transactions. Transaction history updates after every purchase, and also records every time the customer adds money and receives change.

Additionally the project contains a file that represents all the items in the machine's inventory. It contains one line for each of the items that are available in the machine. By default, the Stocker class loads five of each item upon startup.

The final persistent file is a grand total of sales history. It contains a line for each item that the vending machine sells, and the total amount of times that item was sold, as well as the amount of money that machine has pulled in over its lifetime. This information is updated after every purchase, and persists between sessions.

## Known issues and future plans
As of 13 August 2019, this project is still in exactly the form that was turned in for a grade. It has some flaws including poor encapsulation of the vending machine from the CLI menus. It also has a few glitches that we did not catch in the initial testing phase.

I may someday go back to fix some of these issues. If I decide to do so, the original project will be preserved in a separate legacy branch.
