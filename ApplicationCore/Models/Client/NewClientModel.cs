using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Client
{
    public class NewClientModel
    {
        public required string AccountId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}
