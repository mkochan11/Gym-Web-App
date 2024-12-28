using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
