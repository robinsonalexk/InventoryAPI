using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryAPI.Models;

namespace InventoryAPI.Repositories
{
    public interface IItemRepository : IRepository
    {
        public Task<List<Item>> GetAllItems();
        public Task<Item> GetItemById(int id);
        public Task<List<Item>> GetAllMaxPricedItems();
        public Task<Item> GetMaxPricedByItemName(string name);
        public Task<ApiCrudResponse> Create(Item request);
        public Task<bool> Update(Item request);
        public Task<bool> Delete(Item request);
    }
}
