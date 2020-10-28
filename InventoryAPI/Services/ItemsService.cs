using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Models;
using InventoryAPI.Repositories;

namespace InventoryAPI.Services
{
    public class ItemsService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemsService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _itemRepository.GetAllItems();
        }

        public async Task<Item> GetItemById(int id)
        {
            return await _itemRepository.GetItemById(id);
        }

        public async Task<List<Item>> GetAllMaxPricedItems()
        {
            return await _itemRepository.GetAllMaxPricedItems();
        }

        public async Task<Item> GetMaxPricedByItemName(string name)
        {
            return await _itemRepository.GetMaxPricedByItemName(name);
        }

        public async Task<ApiCrudResponse> Create(ItemCreateRequest request)
        {
            ApiCrudResponse response = new ApiCrudResponse();
            Item item = new Item() { itemName = request.itemName, cost = request.cost};
            
            response = await _itemRepository.Create(item);
            if (response.success)
            {
                response.message = "Item created";
            }
            return response;
        }

        public async Task<ApiCrudResponse> Update(Item request)
        {
            ApiCrudResponse response = new ApiCrudResponse();
            
            response.success = await _itemRepository.Update(request);
            if (response.success)
            {
                response.message = "Update successful";
            }
            return response;
        }

        public async Task<ApiCrudResponse> Delete(Item request)
        {
            ApiCrudResponse response = new ApiCrudResponse();
            
            response.success = await _itemRepository.Delete(request);
            if (response.success)
            {
                response.message = "Delete successful";
            }
            return response;
        }
    }

    public interface IItemService
    {
        public Task<List<Item>> GetAllItems();
        public Task<Item> GetItemById(int id);
        public Task<List<Item>> GetAllMaxPricedItems();
        public Task<Item> GetMaxPricedByItemName(string name);
        public Task<ApiCrudResponse> Create(ItemCreateRequest request);
        public Task<ApiCrudResponse> Update(Item request);
        public Task<ApiCrudResponse> Delete(Item request);
    }
}



