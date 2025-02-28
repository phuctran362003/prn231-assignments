using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaccinaCare.Domain.Entities;

public partial class Feedback : BaseEntity
{

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    [Required]
    public Guid FeedbackTypeId { get; set; }

    [ForeignKey("FeedbackTypeId")]
    public virtual FeedbackType FeedbackType { get; set; } // New relationship
}
