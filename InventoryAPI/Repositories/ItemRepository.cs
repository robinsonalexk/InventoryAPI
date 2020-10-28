using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;

namespace InventoryAPI.Repositories
{
    public class ItemRepository : IItemRepository
    {
        protected readonly InventoryDbContext _inventoryDbContext;

        public ItemRepository(InventoryDbContext inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _inventoryDbContext.Items.ToListAsync();
        }

        public async Task<Item> GetItemById(int id)
        {
            return await _inventoryDbContext.Items.Where(x => x.id == id).FirstOrDefaultAsync();
        }


        public async Task<List<Item>> GetAllMaxPricedItems()
        {
            List<Item> result = new List<Item>();
            var names = await _inventoryDbContext.Items.Select(x => x.itemName).Distinct().ToListAsync();
            foreach(string name in names)
            {
                result.Add(await GetMaxPricedByItemName(name));
            }
            return result;
        }

        public async Task<Item> GetMaxPricedByItemName(string name)
        {
            var query = _inventoryDbContext.Items.Where(x => x.itemName.ToLower() == name.ToLower());
            return await query.Where(x => x.cost == query.Max(x => x.cost)).FirstOrDefaultAsync();
        }

        public async Task<ApiCrudResponse> Create(Item request)
        {
            ApiCrudResponse results = new ApiCrudResponse();
            _inventoryDbContext.Add(request);
            results.success = await _inventoryDbContext.SaveChangesAsync() != 0;
            results.item = request;
            return results;
        }

        public async Task<bool> Update(Item request)
        {
            _inventoryDbContext.Update(request);
            return await _inventoryDbContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> Delete(Item request)
        {
            _inventoryDbContext.Remove(request);
            return await _inventoryDbContext.SaveChangesAsync() != 0;
        }
    }
}
