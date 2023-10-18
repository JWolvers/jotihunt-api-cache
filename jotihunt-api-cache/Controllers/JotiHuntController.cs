using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jotihunt_api_cache.Controllers
{
    [Route("api/2.0")]
    [ApiController]
    public class JotiHuntController : ControllerBase
    {
        [HttpGet(nameof(Subscriptions))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ContentResult> Subscriptions()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://jotihunt.nl/api/2.0/subscriptions");
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
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://jotihunt.nl/api/2.0/areas");
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
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://jotihunt.nl/api/2.0/articles");
            return new ContentResult()
            {
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync(),
                ContentType = response?.Content?.Headers?.ContentType?.ToString()
            };
        }
    }
}
