using System;
using System.Collections.Generic;
using System.Net.Http;
using HackerNews.Models;
using HackerNews.Services;
using HackerNews.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace HackerNews.Test
{
    public class NewsStoriesServiceTests
    {
        private NewsStoriesService _newsStoriesService;
        private readonly Mock<IHttpClientFactory> _clientFactory;
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<IConfiguration> _configuration;

        public NewsStoriesServiceTests()
        {
            _clientFactory = new Mock<IHttpClientFactory>();
            _cacheService = new Mock<ICacheService>();
            _configuration = new Mock<IConfiguration>();
        }

        [Fact]
        public async void GetStoryIds_Success()
        {
            var returnData = "[24202115,24202112,24202104,24202095]";
            var client = new Mock<HttpClient>();
            client.Setup(x => x.GetStringAsync(It.IsAny<string>())).ReturnsAsync(returnData);
            var x = await _newsStoriesService.GetStoryIds(LiveDataType.newstories);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetAllStoriesFromIdAsync_Success(bool isCached)
        {
            List<StoryItem> returnCacheData = isCached ? new List<StoryItem>() : null;

            _cacheService.Setup(x => x.GetNewStories()).Returns(returnCacheData);
            _newsStoriesService = new NewsStoriesService(_clientFactory.Object, _cacheService.Object, _configuration.Object);
            //_newsStoriesService.GetStoryItemFromId();

        }
    }
}