using System;
using System.Collections.Generic;
using Cypher.API.Controllers;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Xunit;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Cypher.Test
{
    public class PlayerControllerTest : BaseApiController<PlayerControllerTest>
    {
        public readonly IPlayerRepository MockPlayerRepository;
        public PlayerControllerTest()
        {

           
            List<Player> _Player = new()
            {
                new Player {
                        Id = 1 ,
                        Name="John",
                        CreatedOn = DateTime.Now,
                        CreatedBy = "Developer"
                    },
                new Player {
                        Id = 2 ,
                        Name="Simon",
                        CreatedOn = DateTime.Now,
                        CreatedBy = "Developer"
                },
                new Player {
                        Id = 3 ,
                        Name="Travis",
                        CreatedOn = DateTime.Now,
                        CreatedBy = "Developer"
                }
            };

            //Mocking the repository for test
            var repository = new Mock<IPlayerRepository>();

            //Get All Players List
            repository.Setup(x => x.GetListAsync().Result).Returns(_Player);

            //Get player by Id
            repository.Setup(x => x.GetByIdAsync(It.IsAny<int>()).Result).Returns((int i) => _Player.Where(x => x.Id == i).Single());

            // Add Player
            repository.Setup(x => x.InsertAsync(It.IsAny<Player>()).Result).Returns(
                (Player objPlayer) =>
                {
        
                        _Player.Add(objPlayer);

                    return 1;
                });

            //Remove Player
            repository.Setup(x => x.DeleteAsync(It.IsAny<Player>())).Returns(
              (Player objPlayer) =>
              {
                  _Player.Remove(objPlayer);
                  return Task.CompletedTask;
              });


           MockPlayerRepository = repository.Object;
        }

        [Fact]
        public async Task GetAllPlayersTest()
        {
            // Getting list of all players
            var _players = await MockPlayerRepository.GetListAsync();
            Assert.NotNull(_players); // Test if null
            Assert.True(_players.Count > 0);//Test if players list has player
            Assert.True(_players.Count == 3);//Hoe groot is player list?
        }
        [Fact]
        public async Task GetPlayerById()
        {
            // Get player by id
            var _players = await MockPlayerRepository.GetByIdAsync(2);
            Assert.NotNull(_players); // Check if null
            Assert.IsType<Player>(_players); // Check type of the object
            Assert.Equal("Simon", _players.Name); // Check player name 
            Assert.Equal(2, _players.Id);
        }
        [Fact]
        public async Task InsertPlayerTestAsync()
        {
            // Create a new player
            Player _player = new Player
            { Name = "Test Case Player", CreatedOn = DateTime.Now, CreatedBy = "Test Case" };

            int totalPlayerCount = MockPlayerRepository.GetListAsync().Result.Count;
            Assert.Equal(3, totalPlayerCount); // Check initial list count

            // save new player
            await MockPlayerRepository.InsertAsync(_player);

            //Count again to check if the record was inserted 
            totalPlayerCount = MockPlayerRepository.GetListAsync().Result.Count;

            Assert.Equal(4, totalPlayerCount);  //Confirming the insertion

            var _playerID = await MockPlayerRepository.GetByIdAsync(4);
            Assert.Equal("Test Case Player1", _playerID.Name);
        }

        [Fact]
        public void UpdatePlayerTest()
        {
            // Find player by id
            Player _player = MockPlayerRepository.GetByIdAsync(1).Result;

            // Change one of its properties
            _player.Name = "UpdatingNamefromTestCase";

            // Save our changes.
            MockPlayerRepository.UpdateAsync(_player);

            // Verify the change
            Assert.Equal("UpdatingNamefromTestCase", MockPlayerRepository.GetByIdAsync(1).Result.Name);
        }

        [Fact]
        public async Task DeletePlayerTest()
        {
            // Get player by id to perform delete operation
            int totalPlayerCount = MockPlayerRepository.GetListAsync().Result.Count;
            Assert.Equal(3, totalPlayerCount); // Check initial list count
            Player _player = MockPlayerRepository.GetByIdAsync(1).Result;

            //Pass selected player to delete => verwijderen afhankelijk van de ID
            await MockPlayerRepository.DeleteAsync(_player);

            // Verify the change => List length
            totalPlayerCount = MockPlayerRepository.GetListAsync().Result.Count;
            Assert.Equal(2, totalPlayerCount); // Check initial list count
        }
    }
}


