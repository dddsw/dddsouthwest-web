using System;

namespace DDDSouthWest.Domain.Features.Public.News.ListNews
{
    public class NewsListModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Filename { get; set; }

        public string Body { get; set; }

        public DateTime DatePosted { get; set; }

        public bool IsLive { get; set; }
    }
}