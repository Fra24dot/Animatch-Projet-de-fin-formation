using Animatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class UserLifestyle
    {
        public int Id { get; set; }
        
        public JobType JobType { get; set; }
        public bool RemoteWork { get; set; }

        public int DogAloneHours { get; set; }
        public bool ActiveLifestyle { get; set; }
        public bool FinanciallyStable { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; } = null!;



    }
}
