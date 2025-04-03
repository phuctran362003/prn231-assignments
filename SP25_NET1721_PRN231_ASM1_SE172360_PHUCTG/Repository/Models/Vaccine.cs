using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models;

public class Vaccine
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(255)")] public string? VaccineName { get; set; }

    [Column(TypeName = "nvarchar(max)")] public string? Description { get; set; }

    [Column(TypeName = "nvarchar(500)")] public string? PicUrl { get; set; }

    public int RequiredDoses { get; set; }

    public int DoseIntervalDays { get; set; } //khoảng cách giữa các mũi tiêm

    public decimal? Price { get; set; }
    public bool? AvoidChronic { get; set; } // Không khuyến khích cho bệnh mãn tính
    public bool? AvoidAllergy { get; set; } // Không khuyến nghị cho dị ứng
    public bool? HasDrugInteraction { get; set; } // Có cảnh báo về tương tác thuốc không?
    public bool? HasSpecialWarning { get; set; } // Có cảnh báo điều kiện sức khỏe đặc biệt không?
    public int? VaccineTypeId { get; set; }

    [ForeignKey("VaccineTypeId")] public virtual VaccineType VaccineType { get; set; }
}