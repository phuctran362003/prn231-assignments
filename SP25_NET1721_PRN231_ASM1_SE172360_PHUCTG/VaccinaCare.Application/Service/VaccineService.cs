using VaccinaCare.Application.Interface;
using VaccinaCare.Application.Interface.Common;
using VaccinaCare.Domain.Entities;
using VaccinaCare.Repository.Interfaces;

namespace VaccinaCare.Application.Service;

public class VaccineService : IVaccineService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsService _claimsService;
    private readonly ILoggerService _logger;


    public VaccineService(IUnitOfWork unitOfWork, ILoggerService logger, IClaimsService claimsService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _claimsService = claimsService;
    }
    /// <summary>
    /// Lấy danh sách tất cả vaccine
    /// </summary>
    public async Task<List<Vaccine>> GetAllVaccinesAsync()
    {
        return await _unitOfWork.VaccineRepository.GetAllAsync();
    }

    /// <summary>
    /// Lấy vaccine theo ID
    /// </summary>
    public async Task<Vaccine?> GetVaccineByIdAsync(Guid vaccineId)
    {
        return await _unitOfWork.VaccineRepository.GetByIdAsync(vaccineId);
    }

    /// <summary>
    /// Tạo mới vaccine
    /// </summary>
    public async Task<Vaccine> CreateVaccineAsync(string vaccineName, string description, decimal price)
    {
        var vaccine = new Vaccine
        {
            Id = Guid.NewGuid(),
            VaccineName = vaccineName,
            Description = description,
            Price = price
        };

        await _unitOfWork.VaccineRepository.AddAsync(vaccine);
        await _unitOfWork.SaveChangesAsync();

        _logger.Info($"Vaccine {vaccineName} created successfully.");
        return vaccine;
    }

    /// <summary>
    /// Cập nhật thông tin vaccine
    /// </summary>
    public async Task<Vaccine?> UpdateVaccineAsync(Guid vaccineId, string vaccineName, string description, decimal price)
    {
        var vaccine = await _unitOfWork.VaccineRepository.GetByIdAsync(vaccineId);
        if (vaccine == null)
        {
            _logger.Error($"Vaccine with ID {vaccineId} not found.");
            return null;
        }

        vaccine.VaccineName = vaccineName;
        vaccine.Description = description;
        vaccine.Price = price;

        await _unitOfWork.VaccineRepository.UpdateAsync(vaccine);
        await _unitOfWork.SaveChangesAsync();

        _logger.Info($"Vaccine {vaccineName} updated successfully.");
        return vaccine;
    }

    /// <summary>
    /// Xóa vaccine (Soft Delete)
    /// </summary>
    public async Task<bool> DeleteVaccineAsync(Guid vaccineId)
    {
        var vaccine = await _unitOfWork.VaccineRepository.GetByIdAsync(vaccineId);
        if (vaccine == null)
        {
            _logger.Error($"Vaccine with ID {vaccineId} not found.");
            return false;
        }

        await _unitOfWork.VaccineRepository.SoftRemoveAsync(vaccine);
        await _unitOfWork.SaveChangesAsync();

        _logger.Error($"Vaccine {vaccine.VaccineName} deleted successfully.");
        return true;
    }
}