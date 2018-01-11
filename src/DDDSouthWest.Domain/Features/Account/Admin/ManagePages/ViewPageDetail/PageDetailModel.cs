using System;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManagePages.ViewPageDetail
{
    public class PageDetailModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Filename { get; set; }

        public string BodyMarkdown { get; set; }
            
        public string BodyHtml { get; set; }

        public bool IsLive { get; set; }

        public int PageOrder { get; set; }
        
        public DateTime LastModified { get; set; }
    }
}