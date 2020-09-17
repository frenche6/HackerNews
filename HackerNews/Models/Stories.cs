using System.Collections.Generic;

namespace HackerNews.Models
{
    public class Stories
    {
        public Stories()
        {
            StoryIds = new List<int>();
        }

        public List<int> StoryIds { get; set; }
    }
}