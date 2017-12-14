using System;

namespace DDDSouthWest.Domain.Features.Public.News.NewsDetail
{
    public class NewsDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Filename { get; set; }
        public string BodyHtml { get; set; }
        public string BodyMarkdown { get; set; }
        public bool IsLive { get; set; }
        public DateTime DatePosted { get; set; }
    }
}