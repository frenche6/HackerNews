using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Models;
using HackerNews.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsStoriesController : ControllerBase
    {
        private readonly INewsStoriesService _newsStoriesService;

        public NewsStoriesController(INewsStoriesService newsStoriesService)
        {
            _newsStoriesService = newsStoriesService;
        }

        [HttpGet]
        [Route("GetNewStoriesCount")]
        public async Task<int> GetNewStoriesCount()
        {
            return await _newsStoriesService.GetNewStoriesCount();
        }

        [HttpGet]
        [Route("GetNewStories/{page}/{numberOfRecords}")]
        public async Task<List<StoryItem>> GetNewStories(int page, int numberOfRecords)
        {
            var stories = await _newsStoriesService.GetNewStories(page, numberOfRecords);
            return stories;
        }
    }
}