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
        [Route("GetNewStories")]
        public async Task<List<StoryItem>> GetNewStories()
        {
            var stories = await _newsStoriesService.GetNewStories();
            return stories;
        }
    }
}