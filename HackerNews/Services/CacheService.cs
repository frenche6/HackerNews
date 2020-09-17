using System;
using System.Collections.Generic;
using HackerNews.Models;
using HackerNews.Services.Interfaces;

namespace HackerNews.Services
{
    public class CacheService : ICacheService
    {
        private Stories _storyIds;
        private List<StoryItem> _newStoryItems;
        private DateTime _lastUpdated;

        public CacheService()
        {
            _storyIds = new Stories();
            _newStoryItems = new List<StoryItem>();
        }

        private bool IsCacheValid()
        {
            if (_lastUpdated > DateTime.UtcNow.AddMinutes(-5))
                return true;
            else
                return false;
        }

        public Stories GetStoryIds()
        {
            if (!IsCacheValid())
                ClearCache();

            return _storyIds.StoryIds.Count > 0 ? _storyIds : null;
        }

        public void SetStoryIds(Stories storyIds)
        {
            _storyIds = storyIds;
            _lastUpdated = DateTime.UtcNow;
        }

        public List<StoryItem> GetNewStories()
        {
            if (!IsCacheValid())
                ClearCache();

            return _newStoryItems.Count > 0 ? _newStoryItems : null;
        }
        
        public void SetNewStories(List<StoryItem> storyItems)
        {
            _newStoryItems = storyItems;
        }

        private void ClearCache()
        {
            _storyIds.StoryIds.Clear();
            _newStoryItems.Clear();
        }
    }
}