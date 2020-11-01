using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace InventoryTests
{
    public class ItemRepositoryTests
    {
        InventoryDbContext _context;
        ItemRepository _repository;
        List<Item> _items = new List<Item>();

        public ItemRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                    .UseInMemoryDatabase(databaseName: "inventory")
                    .Options;

            _context = new InventoryDbContext(options);


            _items.Add(new Item { id = 1, itemName = "TEST 1", cost = 20 });
            _items.Add(new Item { id = 2, itemName = "TEST 2", cost = 30 });
            _items.Add(new Item { id = 3, itemName = "TEST 1", cost = 10 });

            _context.Items.AddRange(_items);

            _context.SaveChanges();

            _repository = new ItemRepository(_context);
        }

        [Test]
        public async Task GetAllItemsTest()
        {
            var results = await _repository.GetAllItems();
            Assert.AreEqual(_items, results);
        }

        [Test]
        public async Task GetItemByIdTest()
        {
            var results = await _repository.GetItemById(1);
            Assert.AreEqual(_items.Find(x => x.id == 1), results);
        }

        [Test]
        public async Task GetAllMaxPricedItemsTest()
        {
            var maxPricedList = new List<Item>();
            maxPricedList.Add(await _context.Items.FirstOrDefaultAsync(x => x.id == 1));
            maxPricedList.Add(await _context.Items.FirstOrDefaultAsync(x => x.id == 2));
            var results = await _repository.GetAllMaxPricedItems();

            CollectionAssert.AreEquivalent(maxPricedList, results);
        }

        [Test]
        public async Task GetMaxPricedItemsByNameTest()
        {
            var results = await _repository.GetMaxPricedByItemName("TEST 1");
            var maxItem = await _context.Items.FirstOrDefaultAsync(x => x.id == 1);
            Assert.AreEqual(maxItem, results);
        }
        
        [Test]
        public async Task AddItemTest()
        {
            var newItem = new Item { itemName = "TEST 3", cost = 10 };
            var results = await _repository.Create(newItem);
            Assert.IsTrue(results.success);
            Assert.AreEqual(newItem, results.item);

            _context.Remove(newItem);
            _context.SaveChanges();
        }

        [Test]
        public async Task RemoveItemTest()
        {
            var newItem = new Item { itemName = "TEST 4", cost = 10 };
            _context.Add(newItem);
            await _context.SaveChangesAsync();

            var result = await _repository.Delete(newItem);
            Assert.IsTrue(result);
            CollectionAssert.DoesNotContain(_context.Items, newItem);
        }

        [Test]
        public async Task UpdateItemTest()
        {
            var newItem = new Item { itemName = "TEST 5", cost = 10 };
            _context.Add(newItem);
            await _context.SaveChangesAsync();

            var updateItem = await _context.Items.FirstOrDefaultAsync(x => x.itemName == "TEST 5");
            updateItem.cost = 50;
            var result = await _repository.Update(updateItem);
            Assert.IsTrue(result);
            CollectionAssert.Contains(_context.Items, updateItem);

            _context.Remove(updateItem);
            await _context.SaveChangesAsync();
        }
    }
}