
using Microsoft.Playwright;

namespace SwaggerPetstoreAutomation
{
    public class BaseTest : IAsyncLifetime
    {
        protected IPlaywright Playwright;
        public IAPIRequestContext apiRequestContext;
        public async Task InitializeAsync()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            apiRequestContext = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = "https://petstore.swagger.io"
            });
        }
        public async Task DisposeAsync()
        {
            await apiRequestContext.DisposeAsync();
        }
    }
}