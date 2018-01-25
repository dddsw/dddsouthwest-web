using System.Collections.Generic;
using DDDSouthWest.Domain.Features.Account.Admin.ManageUsers.ListUsers;

namespace DDDSouthWest.Website.Features.Admin.Account.ManageUsers
{
    public class ManageUsersViewModel
    {
        public ManageUsersViewModel()
        {
            Users = new List<UsersListModel>();
        }

        public IList<UsersListModel> Users { get; set; }
    }
}