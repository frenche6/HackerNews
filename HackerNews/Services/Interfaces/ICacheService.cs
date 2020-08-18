using System.Collections.Generic;
using HackerNews.Models;

namespace HackerNews.Services.Interfaces
{
    public interface ICacheService
    {
        List<StoryItem> GetNewStories();
        void SetNewStories(List<StoryItem> storyItems);
    }
}