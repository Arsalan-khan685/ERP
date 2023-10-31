using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class Relation
    {
        public int RelationId { get; set; }
        [Required]
        public string RelationName { get; set; }
    }
}
