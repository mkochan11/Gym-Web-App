using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class Shift : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ReceptionistId { get; set; }

        [ForeignKey(nameof(ReceptionistId))]
        public Receptionist Receptionist { get; set; }
    }
}
