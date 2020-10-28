using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class ItemCreateRequest
    {
        public string itemName { get; set; }
        public float cost { get; set; }
    }
}
