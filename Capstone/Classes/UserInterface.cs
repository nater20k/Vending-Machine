using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        VendingMachine vendingMachine = new VendingMachine();
        VendingMachineItem vendingMachineItem = new VendingMachineItem();

        public void RunInterface()
        {
            vendingMachine.AddItems();
            Prompt();

            bool done = false;
            while (!done)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        DisplayItems();
                        break;

                    case "2":
                        Console.Clear();
                        bool purchasing = true;
                        while (purchasing)
                        {
                            Console.WriteLine("(1) Feed Money\n(2) Select Product\n(3) Finish Transaction\nCurrent Money Provided: $" + vendingMachine.Balance);
                            Console.WriteLine();
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "1":
                                    Console.Clear();
                                    Console.WriteLine("Please enter your money (whole dollars only please)");
                                    input = Console.ReadLine();
                                    vendingMachine.FeedMoney(input);
                                    Console.WriteLine();
                                    Console.WriteLine(vendingMachine.SelectionResult);
                                    Console.Clear();

                                    break;

                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Please enter your product selection");
                                    input = Console.ReadLine();
                                    Console.WriteLine();
                                    vendingMachine.SelectItem(input.ToUpper());
                                    Console.WriteLine();
                                    Console.WriteLine(vendingMachine.SelectionResult);
                                    Console.WriteLine();
                                    break;

                                case "3":
                                    Console.Clear();
                                    Console.WriteLine(vendingMachine.FinishTransaction());
                                    Console.WriteLine();
                                    purchasing = false;
                                    break;
                            }

                        }

                        break;

                    case "3":
                        done = true;
                        break;

                }
                Prompt();
            }

            void Prompt()
            {

                Console.WriteLine("Welcome to the Vendo-Matic 7000!");
                Console.WriteLine();
                Console.WriteLine("Please make a selection");
                Console.WriteLine();
                Console.WriteLine("(1) Display Vending Machine Items \n(2) Purchase\n(3) End");
                Console.WriteLine();
            }

            void DisplayItems()
            {
                Console.WriteLine();
                Console.WriteLine("SKU   Name            Price  #");
                Console.WriteLine("-------------------------------");
                foreach (VendingMachineItem item in vendingMachine.items)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine();
            }
        }
    }
}
