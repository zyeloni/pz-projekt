using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class ScienceClub
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public List<UserScienceClub> Users { get; set; }
        public List<StudentAid> StudentAids { get; set; }
    }
}
