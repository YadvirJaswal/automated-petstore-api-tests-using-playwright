using SwaggerPetstoreAutomation.Models;
using Microsoft.Playwright;

namespace SwaggerPetstoreAutomation.Tests
{
    public class StoreTests : BaseTest
    {

        [Fact]
        public async Task Test_PlaceOrder()
        {
            // Arrange : Define request payload
            var newOrder = new OrderModel
            {
                Id = 0,
                PetId = 10,
                Quantity = 1,
                ShipDate = DateTime.UtcNow.ToString("o"),
                Status = "placed",
                Complete = true
            };

            // Act: Send Post Request to place an order
            var postResponse = await apiRequestContext.PostAsync("/v2/store/order", new APIRequestContextOptions
            {
                DataObject = newOrder
            });

            // Assert : Validate response
            Assert.True(postResponse.Ok, "Failed to place an order");
            
            var orderResponse = await postResponse.JsonAsync<OrderModel>();
            Assert.NotNull(orderResponse);
            Assert.Equal(newOrder.Status, orderResponse.Status);
        }

        [Fact]
        public async Task Test_GetOrderById()
        {
            // Step 1 : Place an Order
            // Arrange : Define request payload
            var newOrder = new OrderModel
            {
                Id = 0,
                PetId = 20,
                Quantity = 2,
                ShipDate = DateTime.UtcNow.ToString("o"),
                Status = "placed",
                Complete = true
            };
            // Act: Send Post Request to place an order
            var postResponse = await apiRequestContext.PostAsync("/v2/store/order", new APIRequestContextOptions
            {
                DataObject = newOrder
            });
            // Assert : Validate response
            Assert.True(postResponse.Ok, "Failed to place an order");
            var orderResponse = await postResponse.JsonAsync<OrderModel>();
            Assert.NotNull(orderResponse);
            var orderId = orderResponse.Id;
            Assert.NotEqual(0, orderId);

            // Step 2: Get an Order By Id
            var getResponse = await apiRequestContext.GetAsync($"/v2/store/order/{orderId}");
            Assert.True(getResponse.Ok);
            var fetchedUser = await getResponse.JsonAsync<OrderModel>();
            Assert.NotNull(fetchedUser);
            Assert.Equal(orderId, fetchedUser.Id);
            Assert.Equal(orderResponse.PetId, fetchedUser.PetId);
        }

        [Fact]
        public async Task Test_DeleteOrderById()
        {
            // Step 1 : Place an Order
            // Arrange : Define request payload
            var newOrder = new OrderModel
            {
                Id = 0,
                PetId = 3,
                Quantity = 2,
                ShipDate = DateTime.UtcNow.ToString("o"),
                Status = "placed",
                Complete = true
            };
            // Act: Send Post Request to place an order
            var postResponse = await apiRequestContext.PostAsync("/v2/store/order", new APIRequestContextOptions
            {
                DataObject = newOrder
            });
            // Assert : Validate response
            Assert.True(postResponse.Ok, "Failed to place an order");
            var orderResponse = await postResponse.JsonAsync<OrderModel>();
            Assert.NotNull(orderResponse);
            var orderId = orderResponse.Id;
            Assert.NotEqual(0, orderId);

            // Step 2: Delete Order By Order Id
            var deleteResponse = await apiRequestContext.DeleteAsync($"/v2/store/order/{orderId}");
            Assert.Equal(200,deleteResponse.Status);

            // Step 3 :Try to fetch the deleted order (should return 404)
            var getResponse = await apiRequestContext.GetAsync($"/v2/store/order/{orderId}");
            Assert.Equal(404,getResponse.Status);
        }

        [Fact]
        public async Task Test_GetStoreInventory()
        {
            var response = await apiRequestContext.GetAsync("/v2/store/inventory");
            Assert.Equal(200, response.Status);
            var inventoryData = await response.JsonAsync<Dictionary<string, int>>();
            Assert.NotNull(inventoryData);
            Assert.True(inventoryData.Count > 0, "Inventory Data is Empty");
        }
    }
}
