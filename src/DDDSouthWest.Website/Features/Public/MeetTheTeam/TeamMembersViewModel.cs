using System.Collections.Generic;

namespace DDDSouthWest.Website.Features.Public.MeetTheTeam
{
    public class TeamMembersViewModel
    {
        public TeamMembersViewModel()
        {
            TeamMembers = new List<TeamMemberViewModel>();
        }

        public IList<TeamMemberViewModel> TeamMembers { get; set; }
    }

    public class TeamMemberViewModel
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PicturePath { get; set; }
        public int YearJoined { get; set; }
        public string Twitter { get; set; }
    }
}