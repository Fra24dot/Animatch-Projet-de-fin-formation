using System;
using System.Collections.Generic;
using System.Text;
using Animatch.Domain.Entities;

namespace Animatch.Domain.ConnectingTables
{
    public class DogMedicalHistory
    {
        public Guid DogId { get; set; }
        public int MedicalHistoryId { get; set; }


        public Dog Dog { get; set; }
        public MedicalHistory MedicalHistory { get; set; }

    }
}
