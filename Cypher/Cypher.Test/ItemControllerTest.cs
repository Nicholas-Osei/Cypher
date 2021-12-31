using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Moq;
using Xunit;

namespace Cypher.Test
{
    public class ItemControllerTest
    {
        public readonly IItemRepository MockItemRepository;
        public ItemControllerTest()
        {

            List<Item> _Item = new()
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
                },
                new Item
                {
                    Id = 3,
                    Name = "Item3",
                    ItemType = "testItem3"
                },
                new Item
                {
                    Id = 4,
                    Name = "Item4",
                    ItemType = "testItem4"
                },

            };

            var repository = new Mock<IItemRepository>();

            repository.Setup(x => x.GetListAsync().Result).Returns(_Item);
            repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()).Result).Returns((int i) => _Item.Where(x => x.Id == i).Single());
            repository.Setup(x => x.InsertAsync(It.IsAny<Item>()).Result).Returns(
               (Item objItem) =>
               {
                   if (objItem.Id.Equals(default))
                   {
                       objItem.Name = "Test Item Insert";
                       objItem.ItemType = "TestITes" ;
                       _Item.Add(objItem);
                   }
                   else
                   {
                       var original = _Item.Where(
                           q => q.Id == objItem.Id).Single();

                       if (original == null)
                       {
                           return 0;
                       }

                       original.Name = objItem.Name;
                       objItem.ItemType = objItem.ItemType;
                   }

                   return 1;
               });

            repository.Setup(x => x.DeleteAsync(It.IsAny<Item>())).Returns(
              (Item objItem) =>
              {
                  _Item.Remove(objItem);
                  return Task.CompletedTask;
              });

            repository.Setup(x => x.DeleteAsync(It.IsAny<Item>())).Returns(
              (Item objItem) =>
              {
                  _Item.Remove(objItem);
                  return Task.CompletedTask;
              });

            MockItemRepository = repository.Object;
        }
        [Fact]
        public async Task GetAllItemsTest()
        {
            var _items = await MockItemRepository.GetListAsync();
            Assert.NotNull(_items); // Test if null
            Assert.True(_items.Count > 0);
            Assert.True(_items.Count == 4);
        }
        [Fact]
        public async Task GetItemById()
        {

            var _item = await MockItemRepository.GetByIdAsync(2);
            Assert.NotNull(_item); // Check if null
            Assert.IsType<Item>(_item); // Check type of the object
            Assert.Equal("Item2", _item.Name); // check name is correct
            Assert.Equal("testItem2", _item.ItemType);
            Assert.Equal(2, _item.Id);
        }
        [Fact]
        public void InsertItemTest()
        {
            Item _item = new Item
            { Name = "TestItemPost", ItemType = "testItemfromPost" };

            int totalPlayerCount = MockItemRepository.GetListAsync().Result.Count;
            Assert.Equal(4, totalPlayerCount); // Check initial list count

            MockItemRepository.InsertAsync(_item);

            totalPlayerCount = MockItemRepository.GetListAsync().Result.Count;
            Assert.Equal(5, totalPlayerCount);  //Confirming the insertion
        }
        [Fact]
        public async Task DeleteItemTest()
        {

            int totalPlayerCount = MockItemRepository.GetListAsync().Result.Count;
            Assert.Equal(4, totalPlayerCount); // Check initial list count
            Item _item = MockItemRepository.GetByIdAsync(1).Result;


            await MockItemRepository.DeleteAsync(_item);
            // Verify the change
            totalPlayerCount = MockItemRepository.GetListAsync().Result.Count;
            Assert.Equal(3, totalPlayerCount); // Check initial list count
        }
    }
}
