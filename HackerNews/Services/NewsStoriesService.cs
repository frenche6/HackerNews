using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNews.Models;
using HackerNews.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HackerNews.Services
{
    public class NewsStoriesService : INewsStoriesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly ICacheService _cacheService;
        private readonly IConfiguration _configuration;

        public NewsStoriesService(IHttpClientFactory httpClientFactory, ICacheService cacheService, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
            _cacheService = cacheService;
            _configuration = configuration;
        }

        public async Task<List<StoryItem>> GetTopNewStories()
        {
            var topStoryIds = await GetStoryIds(LiveDataType.newstories);
            var topStories = await GetAllStoriesFromIdsAsync(topStoryIds);
            return topStories;

        }

        /// <summary>
        /// Comment
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public async Task<Stories> GetStoryIds(LiveDataType dataType)
        {

            var response = await _httpClient.GetStringAsync(_configuration["HackerNewsBaseUrl"] + dataType.GetLiveDataTypeDescription() + ".json");
            var stories = JsonConvert.DeserializeObject<List<int>>(response);
            return new Stories {StoryIds = stories};
        }

        public async Task<StoryItem> GetStoryItemFromId(int id)
        {
            var response = await _httpClient.GetStringAsync(_configuration["HackerNewsBaseUrl"] + "item/" + id + ".json");
            var story = JsonConvert.DeserializeObject<StoryItem>(response);
            return story;
        }

        public async Task<List<StoryItem>> GetAllStoriesFromIdsAsync(Stories stories)
        {
            if (_cacheService.GetNewStories() != null)
            {
                return _cacheService.GetNewStories();
            }

            var storyItems = new List<StoryItem>();
            var tasks = new List<Task<StoryItem>>();

            foreach (var storiesStoryId in stories.StoryIds)
            {
                tasks.Add(GetStoryItemFromId(storiesStoryId));
            }

            foreach (var task in tasks)
            {
                var storyItem = await task;
                storyItems.Add(storyItem);
            }

            _cacheService.SetNewStories(storyItems);
            return storyItems;
        }
    }
}