using SwaggerPetstoreAutomation.Models;
using Microsoft.Playwright;
namespace SwaggerPetstoreAutomation.Tests
{
    public class UserTests: BaseTest
    {
        [Fact]
        public async Task Tests_UserModel_ApiChaining()
        {
            // Step 1: Create new User

            // Arrange : Define payload for the request
            var newUser = new UserModel
            {
                Id = 0, // The id is generated dynamically by the server
                UserName = "testuser",
                FirstName = "Scott",
                LastName = "Grady",
                Email = "scottgrady@gmail.com",
                Password = "Scott123",
                Phone = "1234567890",
                UserStatus = 1
            };

            // Act : Send post request
            var postResponse = await apiRequestContext.PostAsync("/v2/user", new APIRequestContextOptions
            {
                DataObject = newUser
            });

            // Assert : Validate response
            Assert.Equal(200,postResponse.Status);

            // Step 2: Get User by User Name

            // Act : Send get request
            var getResponse = await apiRequestContext.GetAsync($"/v2/user/{newUser.UserName}");

            // Assert : Validate response
            Assert.Equal(200,getResponse.Status);

            // Desearilize post response
            var fetchedUser = await getResponse.JsonAsync<UserModel>();
            Assert.NotNull(fetchedUser);

            Assert.Equal(newUser.UserName, fetchedUser.UserName);

            // Step 3: Update user by Username

            // Arrange : Define request payload
            var updatedUser = new UserModel
            {
                Id = fetchedUser.Id,
                UserName = "testuser",
                FirstName = "Martin", // updated first name 
                LastName = "Grady",
                Email = "martingrady@gmail.com", // updated email address
                Password = "Martin123", // updated password
                Phone = "1234567890",
                UserStatus = 1
            };

            // Act : Send put request
            var putResponse = await apiRequestContext.PutAsync($"/v2/user/{fetchedUser.UserName}", new APIRequestContextOptions
            {
                DataObject = updatedUser
            });

            Assert.True(putResponse.Ok, "Failed to update user");

            // Step 4: Verify the user is updated by fetching the user
            var getResponseForUpdatedUser = await apiRequestContext.GetAsync($"/v2/user/{newUser.UserName}");
            Assert.Equal(200, getResponseForUpdatedUser.Status);
            var fechtedUpdatedUser = await getResponseForUpdatedUser.JsonAsync<UserModel>();
            Assert.NotNull(fechtedUpdatedUser);

            //  Validate the updated details
            Assert.Equal("Martin", fechtedUpdatedUser.FirstName);
            Assert.Equal("martingrady@gmail.com", fechtedUpdatedUser.Email);
            Assert.Equal("Martin123", fechtedUpdatedUser.Password);

            // Step 5 : Delete user by username

            // Act : Send Delete Request
            var deleteResponse = await apiRequestContext.DeleteAsync($"/v2/user/{fetchedUser.UserName}");
            // Assert : Validate response
            Assert.Equal(200, deleteResponse.Status);

            // Step 4: Verify pet is deleted
            var verifyResponse = await apiRequestContext.GetAsync($"/v2/user/ {fetchedUser.UserName}");
            Assert.Equal(404, verifyResponse.Status);
        }
    }
}
