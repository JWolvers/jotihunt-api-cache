using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jotihunt_api_cache.Controllers
{
    [Route("api/2.0")]
    [ApiController]
    public class JotiHuntController : ControllerBase
    {
        private HttpRateLimiter SubscriptionsRateLimiter { get; } = new HttpRateLimiter("https://jotihunt.nl/api/2.0/subscriptions", TimeSpan.FromSeconds(15));
        private HttpRateLimiter AreasRateLimiter { get; } = new HttpRateLimiter("https://jotihunt.nl/api/2.0/areas", TimeSpan.FromSeconds(15));
        private HttpRateLimiter ArticlesRateLimiter { get; } = new HttpRateLimiter("https://jotihunt.nl/api/2.0/articles", TimeSpan.FromSeconds(15));

        [HttpGet(nameof(Subscriptions))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ContentResult> Subscriptions()
        {
            var response = await SubscriptionsRateLimiter.GetAsync();
            return new ContentResult()
            {
                 StatusCode = (int)response.StatusCode,
                 Content = await response.Content.ReadAsStringAsync(),
                 ContentType = response?.Content?.Headers?.ContentType?.ToString()
            };
        }

        [HttpGet(nameof(Areas))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ContentResult> Areas()
        {
            var response = await AreasRateLimiter.GetAsync();
            return new ContentResult()
            {
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync(),
                ContentType = response?.Content?.Headers?.ContentType?.ToString()
            };
        }

        [HttpGet(nameof(Articles))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ContentResult> Articles()
        {
            var response = await ArticlesRateLimiter.GetAsync();
            return new ContentResult()
            {
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync(),
                ContentType = response?.Content?.Headers?.ContentType?.ToString()
            };
        }
    }
}
