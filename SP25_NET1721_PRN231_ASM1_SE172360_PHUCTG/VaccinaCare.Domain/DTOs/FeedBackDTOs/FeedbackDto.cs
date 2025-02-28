namespace VaccinaCare.Domain.DTOs.FeedBackDTOs
{
    public class FeedbackDto
    {
        public int Rating { get; set; }
        public string Comments { get; set; }
        public string? FeedbackTypeName { get; set; } // Include Feedback Type Name

    }
}
