using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryAPI.Models
{
    public class ApiCrudResponse
    {
        public string message { get; set; }
        public bool success { get; set; }
        public Item item { get; set; }
    }
}
