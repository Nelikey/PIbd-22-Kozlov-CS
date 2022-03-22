using System.ComponentModel.DataAnnotations;

namespace AtelierDatabaseImplement.Models
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class DressComponent
    {
        public int Id { get; set; }
        public int DressId { get; set; }
        public int ComponentId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Component Component { get; set; }
        public virtual Dress Dress { get; set; }
    }
}
