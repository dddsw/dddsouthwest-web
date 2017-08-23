using System;

namespace DDDSouthWest.Website.Features.Public.News
{
    public class NewsDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Filename { get; set; }
        public string Body { get; set; }
        public DateTime DatePosted { get; set; }
        public string CanonicalFilename { get; set; }
    }
}