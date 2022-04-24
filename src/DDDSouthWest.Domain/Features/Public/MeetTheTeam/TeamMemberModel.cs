namespace DDDSouthWest.Domain.Features.Public.MeetTheTeam
{
    public class TeamMemberModel
    {
        public int MemberId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PicturePath { get; set; }
        public int YearJoined { get; set; }
        public string Twitter { get; set; }
    }
}