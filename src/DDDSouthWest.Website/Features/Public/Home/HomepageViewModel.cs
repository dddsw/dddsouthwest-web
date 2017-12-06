using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Public.News.ListNews;

namespace DDDSouthWest.Website.Features.Public.Home
{
    public class HomepageViewModel
    {
        public HomepageViewModel()
        {
            News = new List<NewsListModel>();
        }

        public IList<NewsListModel> News { get; set; }
    }
}