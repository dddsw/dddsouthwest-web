using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Public.News.ListNews;

namespace DDDSouthWest.Website.Features.Public.News
{
    public class NewsListViewModel
    {
        public IList<NewsListModel> News { get; set; }
    }
}   