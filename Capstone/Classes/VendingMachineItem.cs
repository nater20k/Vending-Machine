using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachineItem
    {
        public string Sku { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public VendingMachineItem()
        {
            Quantity = 5;            
        }

        public override string ToString()
        {           
            return $"{Sku} {Name.PadRight(25)} ${Price}  {Quantity}";
        }
    }
}
