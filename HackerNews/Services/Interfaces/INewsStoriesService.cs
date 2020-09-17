using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Models;

namespace HackerNews.Services.Interfaces
{
    public interface INewsStoriesService
    {
        Task<int> GetNewStoriesCount();
        Task<List<StoryItem>> GetNewStories(int page, int numberOfRecords);
        Task<Stories> GetStoryIds(LiveDataType dataType);
        Task<StoryItem> GetStoryItemFromId(int id);
        Task<List<StoryItem>> GetAllStoriesFromIdsAsync(List<int> stories);
    }
}