using System.Threading.Tasks;

namespace HackerNews.Services.Interfaces
{
    public interface IHttpService
    {
        Task<string> GetStringAsync(string url);
    }
}