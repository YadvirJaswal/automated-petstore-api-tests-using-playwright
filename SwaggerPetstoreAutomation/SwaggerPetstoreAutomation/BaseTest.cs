
using Microsoft.Playwright;

namespace SwaggerPetstoreAutomation
{
    public class BaseTest : IAsyncLifetime
    {
        protected IPlaywright Playwright;
        protected IAPIRequestContext APIRequestContext;
        public async Task InitializeAsync()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            APIRequestContext = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = "https://petstore.swagger.io/v2"
            });
        }
        public async Task DisposeAsync()
        {
            await APIRequestContext.DisposeAsync();
        }
    }
}