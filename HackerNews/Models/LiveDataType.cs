using System.ComponentModel;

namespace HackerNews.Models
{
    public enum LiveDataType
    {
        [Description("topstories")]
        Topstories,
        [Description("newstories")]
        newstories,
        [Description("beststories")]
        beststories
    }
}