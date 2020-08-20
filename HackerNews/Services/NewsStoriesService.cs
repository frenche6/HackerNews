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
        private readonly ICacheService _cacheService;
        private readonly IConfiguration _configuration;
        private readonly IHttpService _httpService;

        public NewsStoriesService(ICacheService cacheService, IConfiguration configuration, IHttpService httpService)
        {
            _cacheService = cacheService;
            _configuration = configuration;
            _httpService = httpService;
        }

        public async Task<List<StoryItem>> GetTopNewStories()
        {
            var topStoryIds = await GetStoryIds(LiveDataType.newstories);
            var topStories = await GetAllStoriesFromIdsAsync(topStoryIds);
            return topStories;

        }

        /// <summary>
        /// Get list of story items for data type provided
        /// </summary>
        /// <param name="dataType">Live data type of story to get</param>
        /// <returns>Stories, which is a list of ids</returns>
        public async Task<Stories> GetStoryIds(LiveDataType dataType)
        {
            var response = await _httpService.GetStringAsync(_configuration["HackerNewsBaseUrl"] + dataType.GetLiveDataTypeDescription() + ".json");
            var stories = JsonConvert.DeserializeObject<List<int>>(response);
            return new Stories {StoryIds = stories};
        }

        /// <summary>
        /// Get individual story item
        /// </summary>
        /// <param name="id">Id used to get data</param>
        /// <returns>Individual story item</returns>
        public async Task<StoryItem> GetStoryItemFromId(int id)
        {
            var response = await _httpService.GetStringAsync(_configuration["HackerNewsBaseUrl"] + "item/" + id + ".json");
            var story = JsonConvert.DeserializeObject<StoryItem>(response);
            return story;
        }

        /// <summary>
        /// For each story id, go get story.
        /// Caches list of story fetched.
        /// </summary>
        /// <param name="stories"></param>
        /// <returns></returns>
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