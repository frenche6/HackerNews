using System;
using System.Collections.Generic;
using HackerNews.Models;
using HackerNews.Services.Interfaces;

namespace HackerNews.Services
{
    public class CacheService : ICacheService
    {
        private List<StoryItem> _newStoryItems;
        private DateTime _lastUpdated;

        public CacheService()
        {
            _newStoryItems = new List<StoryItem>();
        }

        public List<StoryItem> GetNewStories()
        {
            if (_lastUpdated < DateTime.UtcNow.AddMinutes(-5))
                ClearCache();

            return _newStoryItems.Count > 0 ? _newStoryItems : null;
        }
        
        public void SetNewStories(List<StoryItem> storyItems)
        {
            _newStoryItems = storyItems;
            _lastUpdated = DateTime.UtcNow;
        }

        private void ClearCache()
        {
            _newStoryItems.Clear();
        }
    }
}