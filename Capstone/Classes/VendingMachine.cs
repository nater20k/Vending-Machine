using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public decimal Balance { get; set; }
        public VendingMachineItem CustomerSelection { get; set; }
        public VendingMachineItem TempSelection { get; set; }
        public string SelectionResult { get; set; }
        public string AuditInput { get; set; }
        public int CashInt { get; set; }
        public decimal PreBalance { get; set; }

        public VendingMachine()
        {
            Balance = 0M;
        }

        public List<VendingMachineItem> items = new List<VendingMachineItem>();
        public string filePath = @"C:\VendingMachine\vendingmachine.csv";

        public List<VendingMachineItem> AddItems()
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        VendingMachineItem vendingMachineItem = new VendingMachineItem();
                        string line = sr.ReadLine();
                        string[] fields = line.Split('|');
                        vendingMachineItem.Sku = fields[0];
                        vendingMachineItem.Name = fields[1];
                        decimal price = decimal.Parse(fields[2]);
                        vendingMachineItem.Price = price;

                        items.Add(vendingMachineItem);
                    }
                }
            }
            catch
            {
                items = new List<VendingMachineItem>();
            }
            return items;
        }

        public void FeedMoney(string cash)
        {
            int[] acceptedBills = { 1, 2, 5, 10, 20, 100 };
            int cashInt = int.Parse(cash);

            if (acceptedBills.Contains(cashInt))
            {
                PreBalance = Balance;
                Balance += cashInt;
                WriteLog("Feed Money ");
            }
            else
            {
                SelectionResult = "Accepted Bills Only!";
            }
        }

        public void SelectItem(string selection)
        {
            bool isValid = false;

            foreach (VendingMachineItem item in items)
            {
                if (item.Sku.Equals(selection.ToUpper()))
                {
                    TempSelection = item;
                    isValid = true;
                    break;
                }
            }

            if (isValid == true)
            {
                if (TempSelection.Quantity > 0)
                {
                    if (Balance > TempSelection.Price)
                    {
                        foreach (VendingMachineItem item in items)
                        {
                            if (item.Sku.Equals(selection.ToUpper()))
                            {
                                item.Quantity--;

                                PreBalance = Balance;
                                Balance -= TempSelection.Price;
                                CustomerSelection = TempSelection;
                                WriteLog(CustomerSelection.Name + " " + CustomerSelection.Sku);

                                switch (CustomerSelection.Sku.Substring(0, 1).ToUpper())
                                {
                                    case "A":
                                        SelectionResult = "\"Crunch Crunch, Yum Yum!\"";
                                        break;
                                    case "B":
                                        SelectionResult = "\"Munch Munch, Yum!\"";
                                        break;
                                    case "C":
                                        SelectionResult = "\"Glug Glug, Yum!\"";
                                        break;
                                    case "D":
                                        SelectionResult = "\"Chew Chew, Yum!\"";
                                        break;
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        SelectionResult = "Insufficient Funds";
                    }
                }
                else
                {
                    SelectionResult = "SOLD OUT";
                }
            }
            else
            {
                SelectionResult = "Invalid Selection";
            }
        }

        public decimal ReturnBalance()
        {
            return Balance;
        }

        public string FinishTransaction()
        {
            int quarterCount = 0;
            int dimeCount = 0;
            int nickelCount = 0;
            decimal tempBalance = Balance * 100;

            while (tempBalance >= 25)
            {
                quarterCount++;
                tempBalance -= 25;
            }

            while (tempBalance >= 10)
            {
                dimeCount++;
                tempBalance -= 10;
            }
            while (tempBalance >= 5)
            {
                nickelCount++;
                tempBalance -= 5;
            }
            PreBalance = Balance;
            Balance = 0;
            WriteLog("Give Change");
            return $"Your change is {quarterCount} quarter(s), {dimeCount} dime(s), and {nickelCount} nickel(s).";
        }

        public void WriteLog(string auditInput)
        {
            string fullPath = @"c:\VendingMachine\Log.txt";

            var timeUtc = DateTime.UtcNow;
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);

            AuditInput = auditInput;

            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    sw.WriteLine($"{easternTime} {AuditInput.PadRight(12)} ${PreBalance.ToString().PadRight(4)}   ${Balance} ");
                }
            }
            catch
            {

            }
        }
    }
}
