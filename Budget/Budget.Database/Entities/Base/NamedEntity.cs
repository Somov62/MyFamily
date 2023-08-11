using System.ComponentModel.DataAnnotations;

namespace Budget.Database.Entities.Base
{
    public abstract class NamedEntity : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Name{ get; set; }
    }
}
