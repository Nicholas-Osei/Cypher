using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cypher.Api.Controllers.v1;
using Cypher.API.Controllers;
using Cypher.Application.Interfaces.Repositories;
using Cypher.Domain.Entities.Cypher;
using Moq;
using Xunit;

namespace Cypher.Test
{
    public class UserCredentialsControllerTest : BaseApiController<UserCredentialsControllerTest>
    {
        public readonly IUserCredentialsRepository MockUserCredentialRepository;
        public UserCredentialsControllerTest()
        {
            List<UserCredential> credentials = new()
            {
                new UserCredential
                {
                    Id =1,
                    Base64Credential = "ditiseentest="
                },
                new UserCredential
                {
                    Id = 2,
                    Base64Credential = "testofhetwerkt="
                },
            };

            var repository = new Mock<IUserCredentialsRepository>();

            repository.Setup(u=>u.GetListAsync().Result).Returns(credentials);

            //add UserCredentials
            repository.Setup(x => x.InsertAsync(It.IsAny<UserCredential>()).Result).Returns(
               (UserCredential objCredential) =>
               {
                   if (objCredential.Id.Equals(default(int)))
                   {
                       objCredential.Base64Credential = "Test userCredential Insert";

                       credentials.Add(objCredential);
                   }
                   else
                   {
                       var original = credentials.Where(
                           q => q.Id == objCredential.Id).Single();

                       if (original == null)
                       {
                           return 0;
                       }

                       original.Base64Credential = objCredential.Base64Credential;

                   }

                   return 1;
               });


            MockUserCredentialRepository = repository.Object;
        }
        [Fact]
        public async Task GetAllUserCredentialsTest()
        {
            // Getting list of all Gebruiker's credentials
            var _credentials = await MockUserCredentialRepository.GetListAsync();
            Assert.NotNull(_credentials); // Test if null
            Assert.True(_credentials.Count > 0);//Test if players list has player
            Assert.True(_credentials.Count == 2);
        }
        [Fact]
        public void InsertUserCredentialsTest()
        {
            // Create a new credential
            UserCredential _credential = new UserCredential
            { Base64Credential = "TestCaseAddCredential=" };

            int totalUserCredentials = MockUserCredentialRepository.GetListAsync().Result.Count;
            Assert.Equal(2, totalUserCredentials); // Check initial list count

            // save new credential
            MockUserCredentialRepository.InsertAsync(_credential);
            //Count again to check if the record was inserted 
            totalUserCredentials = MockUserCredentialRepository.GetListAsync().Result.Count;
            Assert.Equal(3, totalUserCredentials);  //Confirming the insertion
        }
    }
}
