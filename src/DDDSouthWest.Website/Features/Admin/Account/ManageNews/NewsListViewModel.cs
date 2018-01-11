using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Account.Admin.ManageNews.ListNews;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageNews
{
    public class NewsListViewModel
    {
        public IList<NewsListModel> News { get; set; }
    }
}