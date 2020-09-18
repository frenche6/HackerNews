using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> GetNewStoriesCount()
        {
            Stories newStoryIds = _cacheService.GetStoryIds();

            if (newStoryIds == null)
            {
                newStoryIds = await GetStoryIds(LiveDataType.newstories);
                _cacheService.SetStoryIds(newStoryIds);
            }

            return newStoryIds.StoryIds.Count;
        }

        public async Task<List<StoryItem>> GetNewStories(int page, int numberOfRecords)
        {
            Stories newStoryIds = _cacheService.GetStoryIds();

            if (newStoryIds == null)
            {
                newStoryIds = await GetStoryIds(LiveDataType.newstories);
                _cacheService.SetStoryIds(newStoryIds);
            }

            var pagedStoryIds = GetPagedStoryIds(newStoryIds, page, numberOfRecords);
            var results = await GetAllStoriesFromIdsAsync(pagedStoryIds);
            return results;

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

        private List<int> GetPagedStoryIds(Stories storyIds, int page, int numberOfRecords)
        {
            if (page == 1)
                return storyIds.StoryIds.Take(numberOfRecords).ToList();

            return storyIds.StoryIds.Skip(page * numberOfRecords).Take(numberOfRecords).ToList();
        }

        public async Task<List<StoryItem>> GetNumberOfStories(int numberOfStories)
        {
            Stories newStoryIds = _cacheService.GetStoryIds();

            if (newStoryIds == null)
            {
                newStoryIds = await GetStoryIds(LiveDataType.newstories);
                _cacheService.SetStoryIds(newStoryIds);
            }

            return await GetAllStoriesFromIdsAsync(newStoryIds.StoryIds.Take(numberOfStories).ToList());
        }

        /// <summary>
        /// For each story id, go get story.
        /// Caches list of story fetched.
        /// </summary>
        /// <param name="stories"></param>
        /// <returns></returns>
        public async Task<List<StoryItem>> GetAllStoriesFromIdsAsync(List<int> storyIds)
        {
            var results = new List<StoryItem>();
            var cachedStories = _cacheService.GetNewStories();
            var tasks = new List<Task<StoryItem>>();

            foreach(var storyId in storyIds)
            {
                if (cachedStories != null && cachedStories.Exists(x => x.Id == storyId))
                    results.Add(cachedStories.Single(x => x.Id == storyId));
                else
                    tasks.Add(GetStoryItemFromId(storyId));
            }

            var fetchedStoryItems = new List<StoryItem>();

            foreach (var task in tasks)
            {
                var storyItem = await task;
                fetchedStoryItems.Add(storyItem);
            }

            _cacheService.SetNewStories(fetchedStoryItems);

            results.AddRange(fetchedStoryItems);
            return results;
        }

        
    }
}