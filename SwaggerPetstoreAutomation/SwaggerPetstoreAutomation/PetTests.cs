using Microsoft.Playwright;
using System.Text.Json;
using SwaggerPetstoreAutomation.Models;

namespace SwaggerPetstoreAutomation
{
    public class PetTests : BaseTest
    {
        // Create Pet (Post Request)

        [Fact]
        public async Task AddNewPet_ShouldReturnSuccessAndCorrectData()
        {
            // Arrange : Define Payload for the Request
            var petData = new PetModel
            {
                Id = 0,
                Category = new Category { Id = 0, Name = "Dog" },
                Name = "Fluffy",
                PhotoUrls = new List<string> { "String" },
                Tags = new List<Tag> { new Tag { Id = 0, Name = "string" } },
                Status = "available"
            };
            var payload = JsonSerializer.Serialize(petData);

            // Act : Send Post Request
            var response = await APIRequestContext.PostAsync("/v2/pet", new APIRequestContextOptions
            {
                DataObject = petData
            });

            // Assert : Validate response body
            Assert.Equal(200,response.Status);

            // Deserialize response body
            var responseBody = await response.JsonAsync<PetModel>();
            Assert.NotNull(responseBody);
            Assert.Equal(petData.Name,responseBody.Name);
            Assert.Equal(petData.Category.Name,responseBody.Category.Name);
            Assert.Equal(petData.Status,responseBody.Status);
        }

        [Fact]
        public async Task GetPetById_ShouldReturnSuccessAndCorrectData()
        {
            // Assert: Specify the Id of the pet
            var id = "9222968140497181060";

            // Act : Send get request
            var response = await APIRequestContext.GetAsync($"/v2/pet/{id}");

            // Assert : Validate response
            Assert.Equal(200,response.Status);

            // Deserialize response body
            var responseBody = await response.JsonAsync<PetModel>();
            Assert.NotNull(responseBody);
            Assert.Equal(id.ToString(), responseBody.Id.ToString());
            Assert.Equal("Fluffy", responseBody.Name);
            Assert.Equal("available", responseBody.Status);
        }
    }
}
