using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public abstract class Trainer<T> : Employee
    {
        public List<T> Trainings { get; set; } = new List<T>();
    }
}
