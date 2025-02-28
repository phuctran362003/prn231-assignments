namespace VaccinaCare.Domain.DTOs.FeedBackDTOs
{
    public class FeedbackDto
    {
        public Guid AppointmentId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
