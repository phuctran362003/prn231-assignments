namespace VaccinaCare.Domain.Entities;

public class Appointment
{
    public Guid? ParentId { get; set; }
    public Guid? ChildId { get; set; }
    public Guid? PolicyId { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public Guid? VaccineSuggestionId { get; set; }
    public string? Notes { get; set; }
    public bool NotificationSent { get; set; } = false;
    public string? CancellationReason { get; set; }
    public virtual ICollection<AppointmentsVaccine> AppointmentsVaccines { get; set; } = new List<AppointmentsVaccine>();

}


