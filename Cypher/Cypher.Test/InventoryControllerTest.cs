using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.API.Controllers;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Moq;
using Xunit;

namespace Cypher.Test
{
    public class InventoryControllerTest : BaseApiController<InventoryControllerTest>
    {
        public readonly IInventoryRepository MockInventoryRepository;
        List<Item> _Items = new()
        {
            new Item
            {
                Id = 1,
                Name = "Item1",
                ItemType = "testItem1"

            },
            new Item
            {
                Id = 2,
                Name = "Item2",
                ItemType = "testItem2"
            }
        };
        public InventoryControllerTest()
        {
            List<Inventory> _Inventory = new()
            {
                new Inventory
                {
                    Id = 1,
                    Items = _Items
                },
                new Inventory
                {
                    Id = 2,
                    Items = _Items
                }

            };
            var repository = new Mock<IInventoryRepository>();
            repository.Setup(x => x.GetListAsync().Result).Returns(_Inventory);

            repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()).Result).Returns((int i) => _Inventory.Where(x => x.Id == i).Single());

            repository.Setup(x => x.InsertAsync(It.IsAny<Inventory>()).Result).Returns(
                (Inventory objInventory) =>
                {
                    
                   _Inventory.Add(objInventory);
                    

                    return 1;
                });

            repository.Setup(x => x.DeleteAsync(It.IsAny<Inventory>())).Returns(
             (Inventory objInventory) =>
             {
                 _Inventory.Remove(objInventory);
                 return Task.CompletedTask;
             });

            MockInventoryRepository = repository.Object;
        }
        [Fact]
        public async Task GetAllInventoryTest()
        {
            var _inventories = await MockInventoryRepository.GetListAsync();
            Assert.NotNull(_inventories); // Test if null
            Assert.True(_inventories.Count > 0);
            Assert.True(_inventories.Count == 2);
        }
        [Fact]
        public async Task GetInventoryById()
        {

            var _inventory = await MockInventoryRepository.GetByIdAsync(2);
            Assert.NotNull(_inventory); // Check if null
            Assert.IsType<Inventory>(_inventory); // Check type of the object
            Assert.Equal(2, _inventory.Items.Count); // Check items length
            Assert.Equal(2, _inventory.Id);
        }
        [Fact]
        public void InsertInventoryTest()
        {
            _Items.Add(new Item
            {
                Id = 1,
                Name = "ItemfromInsert",
                ItemType = "testItemfromInsert"

            });
            Inventory _inventory = new Inventory
            { Items = _Items};
        

            int totalPlayerCount = MockInventoryRepository.GetListAsync().Result.Count;
            Assert.Equal(2, totalPlayerCount); // Check initial list count

            MockInventoryRepository.InsertAsync(_inventory);

            //Count again to check if the record was inserted 
            totalPlayerCount = MockInventoryRepository.GetListAsync().Result.Count;
            Assert.Equal(3, totalPlayerCount);  //Confirming the insertion
        }
        [Fact]
        public async Task DeleteInventoryTest()
        {

            int totalInventory = MockInventoryRepository.GetListAsync().Result.Count;
            Assert.Equal(2, totalInventory); // Check initial list count

            Inventory _inventory = MockInventoryRepository.GetByIdAsync(1).Result;
            await MockInventoryRepository.DeleteAsync(_inventory);

            // Verify the change
            totalInventory = MockInventoryRepository.GetListAsync().Result.Count;
            Assert.Equal(1, totalInventory); // Check initial list count
        }
    }
}
