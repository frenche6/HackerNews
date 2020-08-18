using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Models;

namespace HackerNews.Services.Interfaces
{
    public interface INewsStoriesService
    {
        Task<List<StoryItem>> GetTopNewStories();
        Task<Stories> GetStoryIds(LiveDataType dataType);
        Task<StoryItem> GetStoryItemFromId(int id);
        Task<List<StoryItem>> GetAllStoriesFromIdsAsync(Stories stories);
    }
}