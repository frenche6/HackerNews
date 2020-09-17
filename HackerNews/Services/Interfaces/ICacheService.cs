using System.Collections.Generic;
using HackerNews.Models;

namespace HackerNews.Services.Interfaces
{
    public interface ICacheService
    {
        Stories GetStoryIds();
        void SetStoryIds(Stories storyIds);
        List<StoryItem> GetNewStories();
        void SetNewStories(List<StoryItem> storyItems);
    }
}