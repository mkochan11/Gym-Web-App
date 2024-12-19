using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public abstract class Training : BaseEntity
    {
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
