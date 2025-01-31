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
    }
}
