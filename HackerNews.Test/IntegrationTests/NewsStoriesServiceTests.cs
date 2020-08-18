using HackerNews.Services;
using HackerNews.Services.Interfaces;
using Xunit;

namespace HackerNews.Test
{
    public class NewsStoriesServiceTests
    {
        private readonly INewsStoriesService _newsStoriesService;

        public NewsStoriesServiceTests(INewsStoriesService newsStoriesService)
        {
            _newsStoriesService = newsStoriesService;
        }

        [Fact]
        public void CanGetTopStoryIds()
        {

        }
    }
}