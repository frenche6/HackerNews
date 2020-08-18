using System.Collections.Generic;
using HackerNews.Models;
using HackerNews.Services.Interfaces;

namespace HackerNews.Services
{
    public class CacheService : ICacheService
    {
        private List<StoryItem> _newStoryItems;

        public CacheService()
        {
            _newStoryItems = new List<StoryItem>();
        }

        public List<StoryItem> GetNewStories()
        {
            return _newStoryItems.Count > 0 ? _newStoryItems : null;
        }
        
        public void SetNewStories(List<StoryItem> storyItems)
        {
            _newStoryItems = storyItems;
        }
    }
}