using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InventoryAPI.Models
{
    public class Item
    {
        public int id { get; set; }
        public string itemName { get; set; }
        public float cost { get; set; }
    }
}
