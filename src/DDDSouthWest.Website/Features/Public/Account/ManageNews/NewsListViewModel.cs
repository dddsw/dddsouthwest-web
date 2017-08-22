using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Account.ManageEvents.ListNews;

namespace DDDSouthWest.Website.Features.Public.Account.ManageNews
{
    public class NewsListViewModel
    {
        public IList<NewsListModel> News { get; set; }
    }
}