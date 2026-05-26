using Animatch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Animatch.Domain.ConnectingTables
{
    public class DogMedia
    {
        public Guid DogId { get; set; }
        public int MediaId { get; set; }

        public Dog Dog { get; set; } = null!;
        public Media Media { get; set; } = null!;
    }
}
