namespace VaccinaCare.Domain.Entities;

public partial class Vaccine
{
    public string? VaccineName { get; set; }

    public string? Description { get; set; }

    public string? PicUrl { get; set; }

    public string? Type { get; set; }

    public int RequiredDoses { get; set; }

    public int DoseIntervalDays { get; set; } //khoảng cách giữa các mũi tiêm

    public decimal? Price { get; set; }
    public bool? AvoidChronic { get; set; } // Không khuyến khích cho bệnh mãn tính
    public bool? AvoidAllergy { get; set; } // Không khuyến nghị cho dị ứng
    public bool? HasDrugInteraction { get; set; } // Có cảnh báo về tương tác thuốc không?
    public bool? HasSpecialWarning { get; set; } // Có cảnh báo điều kiện sức khỏe đặc biệt không?

    public virtual ICollection<AppointmentsVaccine> AppointmentsVaccines { get; set; } = new List<AppointmentsVaccine>();
}

