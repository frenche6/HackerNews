using HackerNews.Services.Interfaces;
using Xunit;
using Moq;

namespace HackerNews.Test
{
    public class CacheServiceTests
    {
        private Mock ICacheService;

        public CacheServiceTests()
        {
            ICacheService = new Mock<ICacheService>();
        }

        public void GetStories()
        {
            var service = new Mock<ICacheService>();
        }
    }
}