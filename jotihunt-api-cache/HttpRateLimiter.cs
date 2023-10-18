namespace jotihunt_api_cache
{
    public class HttpRateLimiter
    {
        private string EndPoint { get; }
        private TimeSpan Interval { get; }
        private DateTime? LastFetch { get; set; }
        private HttpResponseMessage CachedResponse { get; set; }
        private HttpClient HttpClient { get; } = new HttpClient();
        private SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);

        public HttpRateLimiter(string endPoint, TimeSpan interval)
        {
            EndPoint = endPoint;
            Interval = interval;
            CachedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.GatewayTimeout);
        }

        public async Task<HttpResponseMessage> GetAsync()
        {
            if (await Semaphore.WaitAsync(TimeSpan.Zero))
            {
                try
                {
                    if (LastFetch == null || DateTime.UtcNow - LastFetch > Interval)
                    {
                        var response = await HttpClient.GetAsync(EndPoint);

                        LastFetch = DateTime.UtcNow;

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            CachedResponse = response;
                    }
                }
                finally
                {
                    Semaphore.Release();
                }
            }

            return CachedResponse;
        }
    }
}
