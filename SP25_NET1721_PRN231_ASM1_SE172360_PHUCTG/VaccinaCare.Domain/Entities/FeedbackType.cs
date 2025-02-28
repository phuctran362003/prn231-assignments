using System.ComponentModel.DataAnnotations;

namespace VaccinaCare.Domain.Entities
{
    public class FeedbackType : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
