using System;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManagePages.ListPages
{
    public class PageListModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PageOrder { get; set; }

        public bool IsLive { get; set; }

        public DateTime LastModified { get; set; }
    }
}