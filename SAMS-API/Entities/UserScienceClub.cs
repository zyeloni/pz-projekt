using System;

namespace API.Entities
{
    public class UserScienceClub
    {
        public Guid ID { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
        public ScienceClub ScienceClub { get; set; }
        public Guid ScienceClubID { get; set; }
        public bool Active { get; set; }
    }
}