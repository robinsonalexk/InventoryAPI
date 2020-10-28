using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryAPI.Services;
using InventoryAPI.Models;

namespace InventoryAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            return Ok(await _itemService.GetAllItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            return Ok(await _itemService.GetItemById(id));
        }

        [HttpGet("MaxPriced")]
        public async Task<IActionResult> GetAllMaxPricedItems()
        {
            return Ok(await _itemService.GetAllMaxPricedItems());
        }

        [HttpGet("MaxPriced/{name}")]
        public async Task<IActionResult> GetMaxPricedByItemName(string name)
        {
            return Ok(await _itemService.GetMaxPricedByItemName(name));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemCreateRequest request)
        {
            var response = new ApiCrudResponse();
            if (request == null)
            {
                response.success = false;
                response.message = "Recieved empty payload";
            }
            else
            response = await _itemService.Create(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Item request)
        {
            var response = new ApiCrudResponse();
            if (request == null)
            {
                response.success = false;
                response.message = "Recieved empty payload";
            }
            else
            {
                response = await _itemService.Update(request);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Item request)
        {
            var response = new ApiCrudResponse();
            if (request == null)
            {
                response.success = false;
                response.message = "Recieved empty payload";
            }
            else
            {
                response = await _itemService.Delete(request);
            }
            return Ok(response);
        }
    }
}
