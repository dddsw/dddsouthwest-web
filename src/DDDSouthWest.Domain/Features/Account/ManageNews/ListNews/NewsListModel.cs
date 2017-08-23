using System;

namespace DDDSouthWest.Domain.Features.Account.ManageNews.ListNews
{
    public class NewsListModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Filename { get; set; }
        
        public DateTime DatePosted { get; set; }

        public bool IsLive { get; set; }
    }
}