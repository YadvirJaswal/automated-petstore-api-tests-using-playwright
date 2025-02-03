using Microsoft.Playwright;
using System.Text.Json;
using SwaggerPetstoreAutomation.Models;
using Xunit.Sdk;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace SwaggerPetstoreAutomation
{ 
    public class PetTests : BaseTest
    {
        [Fact]
        public async Task Test_PetStore_ApiChaining()
        {
            // Step 1: Add a new pet

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

            // Act : Send Post Request
            var postResponse = await apiRequestContext.PostAsync("/v2/pet", new APIRequestContextOptions
            {
                DataObject = petData
            });

            // Assert : Validate response body
            Assert.Equal(200, postResponse.Status);

            // Deserialize response body
            var createdPet = await postResponse.JsonAsync<PetModel>();
            Assert.NotNull(createdPet);
            var petId = createdPet.Id; // Extract dynamically generated id

            Assert.Equal(petData.Name, createdPet.Name);
            Assert.Equal(petData.Category.Name, createdPet.Category.Name);
            Assert.Equal(petData.Status, createdPet.Status);

            // Step 2: Get Pet By Id

            // Act : Send get request
            var getResponse = await apiRequestContext.GetAsync($"/v2/pet/{petId}");

            // Assert : Validate response
            Assert.Equal(200, getResponse.Status);

            // Deserialize response body
            var fechtedPet = await getResponse.JsonAsync<PetModel>();
            Assert.NotNull(fechtedPet);
            Assert.Equal(petId.ToString(), fechtedPet.Id.ToString());
            Assert.Equal("Fluffy", fechtedPet.Name);
            Assert.Equal("available", fechtedPet.Status);

            // Step 3: Delete Pet by Id

            // Act : Send Delete Request
            var deleteResponse = await apiRequestContext.DeleteAsync($"/v2/pet/{petId}");
            // Assert : Validate response
            Assert.Equal(200, deleteResponse.Status);

            // Step 4: Verify pet is deleted
            var verifyResponse = await apiRequestContext.GetAsync($"/v2/pet/{petId}");
            Assert.Equal(404, verifyResponse.Status);
        }
    }
}
