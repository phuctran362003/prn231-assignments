using VaccinaCare.Domain.Entities;

namespace VaccinaCare.Application.Interface;

public interface IVaccineService
{
    Task<List<Vaccine>> GetAllVaccinesAsync();
    Task<Vaccine?> GetVaccineByIdAsync(Guid vaccineId);
    Task<Vaccine> CreateVaccineAsync(string vaccineName, string description, decimal price);
    Task<Vaccine?> UpdateVaccineAsync(Guid vaccineId, string vaccineName, string description, decimal price);
    Task<bool> DeleteVaccineAsync(Guid vaccineId);
}