using System;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageNews.ViewNewsDetail
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Filename { get; set; }
        public string BodyMarkdown { get; set; }
        public bool IsLive { get; set; }
        public DateTime DatePosted { get; set; }
    }
}