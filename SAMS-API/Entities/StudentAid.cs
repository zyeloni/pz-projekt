using System;

namespace API.Entities
{
    public class StudentAid
    {
        public DateTime DateTime { get; set; }
        public Guid ID { get; set; }
        public string Subject { get; set; }
        public string Comment { get; set; }
        public int Count { get; set; }

        public DateTime StartDate { get; set; }
        public int Duration { get; set; }

        public Guid UserID { get; set; }
        public Guid ScienceClubID { get; set; }

        public User User { get; set; }
        public ScienceClub ScienceClub { get; set; }
    }
}
