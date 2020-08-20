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
        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IHttpService> _httpService;

        public NewsStoriesServiceTests()
        {
            _cacheService = new Mock<ICacheService>();
            _configuration = new Mock<IConfiguration>();
            _httpService = new Mock<IHttpService>();
        }

        [Fact]
        public async void GetStoryIds_Success()
        {
            var returnData = "[24202115,24202112,24202104,24202095]";
            _httpService.Setup(x => x.GetStringAsync(It.IsAny<string>())).ReturnsAsync(returnData);

            _newsStoriesService = new NewsStoriesService(_cacheService.Object, _configuration.Object, _httpService.Object);
            var stories = await _newsStoriesService.GetStoryIds(LiveDataType.newstories);
            Assert.NotNull(stories.StoryIds);
            Assert.True(stories.StoryIds.Count > 0);
        }

        [Fact]
        public async void GetStoryIds_NullData_Failure()
        {
            var returnData = "";
            _httpService.Setup(x => x.GetStringAsync(It.IsAny<string>())).ReturnsAsync(returnData);

            _newsStoriesService = new NewsStoriesService(_cacheService.Object, _configuration.Object, _httpService.Object);
            var stories = await _newsStoriesService.GetStoryIds(LiveDataType.newstories);
            Assert.Null(stories.StoryIds);
        }

        [Fact]
        public async void GetStoryItemFromId_Success()
        {
            var returnData =
                "{\r\n  \"by\" : \"dhouston\",\r\n  \"descendants\" : 71,\r\n  \"id\" : 8863,\r\n  \"kids\" : [ 9224, 8917, 8952, 8958, 8884, 8887, 8869, 8940, 8908, 9005, 8873, 9671, 9067, 9055, 8865, 8881, 8872, 8955, 10403, 8903, 8928, 9125, 8998, 8901, 8902, 8907, 8894, 8870, 8878, 8980, 8934, 8943, 8876 ],\r\n  \"score\" : 104,\r\n  \"time\" : 1175714200,\r\n  \"title\" : \"My YC app: Dropbox - Throw away your USB drive\",\r\n  \"type\" : \"story\",\r\n  \"url\" : \"http://www.getdropbox.com/u/2/screencast.html\"\r\n}";

            _httpService.Setup(x => x.GetStringAsync(It.IsAny<string>())).ReturnsAsync(returnData);
            _newsStoriesService = new NewsStoriesService(_cacheService.Object, _configuration.Object, _httpService.Object);

            var story = await _newsStoriesService.GetStoryItemFromId(8863);
            Assert.NotNull(story);
        }

        [Fact]
        public async void GetStoryItemFromId_BadData_Failure()
        {
            var returnData = "THIS IS BAD DATA";
            _httpService.Setup(x => x.GetStringAsync(It.IsAny<string>())).ReturnsAsync(returnData);
            _newsStoriesService = new NewsStoriesService(_cacheService.Object, _configuration.Object, _httpService.Object);

            await Assert.ThrowsAsync<Newtonsoft.Json.JsonReaderException>(() =>
                 _newsStoriesService.GetStoryItemFromId(8863));
        }

        [Fact]
        public async void GetAllStoriesFromIdsAsync()
        {
            var returnData =
                "{\r\n  \"by\" : \"dhouston\",\r\n  \"descendants\" : 71,\r\n  \"id\" : 8863,\r\n  \"kids\" : [ 9224, 8917, 8952, 8958, 8884, 8887, 8869, 8940, 8908, 9005, 8873, 9671, 9067, 9055, 8865, 8881, 8872, 8955, 10403, 8903, 8928, 9125, 8998, 8901, 8902, 8907, 8894, 8870, 8878, 8980, 8934, 8943, 8876 ],\r\n  \"score\" : 104,\r\n  \"time\" : 1175714200,\r\n  \"title\" : \"My YC app: Dropbox - Throw away your USB drive\",\r\n  \"type\" : \"story\",\r\n  \"url\" : \"http://www.getdropbox.com/u/2/screencast.html\"\r\n}";
            var storyIds = new Stories {StoryIds = new List<int> {1, 2, 3, 4}};

            _httpService.Setup(x => x.GetStringAsync(It.IsAny<string>())).ReturnsAsync(returnData);
            _newsStoriesService = new NewsStoriesService(_cacheService.Object, _configuration.Object, _httpService.Object);
            var stories = await _newsStoriesService.GetAllStoriesFromIdsAsync(storyIds);

            Assert.NotNull(stories);
            Assert.True(stories.Count == storyIds.StoryIds.Count);
        }
    }
}