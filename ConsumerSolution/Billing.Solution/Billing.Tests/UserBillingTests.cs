using System;
using System.Collections.Generic;
using Billing.Tests.MockService;
using BillingAPI.Client.Implementation;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace Billing.Tests
{
    public class UserBillingTests : IClassFixture<BillingUserPact>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public UserBillingTests(BillingUserPact data)
        {
            _mockProviderService = data.MockProviderService;
            _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
            _mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        }

        [Fact]
        public void GetUser_WhenTheUserExists_ReturnsTheUser()
        {
            //Arrange
            _mockProviderService
                .Given($"There is a user with id '1' and first name 'Chandan'")
                .UponReceiving("A GET request to retrieve the user")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = $"/api/user/1",
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new //NOTE: Note the case sensitivity here, the body will be serialised as per the casing defined
                    {
                        id = 1,
                        userFirstName = "Chandan",
                        userLastName = "Vasishta",
                        userAddress = "Mohali"
                    }
                }); //NOTE: WillRespondWith call must come last as it will register the interaction

            var consumer = new UserClient(_mockProviderServiceBaseUri);

            //Act
            var result = consumer.GetUserDetails(1);

            //Assert
            Assert.Equal("Chandan", result.UserFirstName);

            _mockProviderService.VerifyInteractions(); //NOTE: Verifies that interactions registered on the mock provider are called at least once
        }
    }
}
