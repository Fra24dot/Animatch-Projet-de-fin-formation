using System;
using System.Collections.Generic;
using System.Text;

namespace Animatch.Domain.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
    }
}
