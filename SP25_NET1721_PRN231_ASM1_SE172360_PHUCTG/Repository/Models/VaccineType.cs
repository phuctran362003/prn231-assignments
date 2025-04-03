using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models;

public class VaccineType
{
    [Key] public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; }

    [Column(TypeName = "nvarchar(500)")] public string Description { get; set; }

    public virtual ICollection<Vaccine>? Vaccines { get; set; } = new List<Vaccine>();
}