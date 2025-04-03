using Repository;
using Repository.Models;

namespace Service;

public interface IVaccineTypeService
{
    Task<List<VaccineType>> GetAll();
    Task<VaccineType> GetById(int id);
    Task<int> Create(VaccineType vaccineType);
    Task<int> Update(VaccineType vaccineType);
    Task<bool> Delete(int id);
    Task<List<VaccineType>> Search(string name);
}

public class VaccineTypeService : IVaccineTypeService
{
    private readonly VaccineTypeRepository _repository;

    public VaccineTypeService()
    {
        _repository ??= new VaccineTypeRepository();
    }

    public async Task<int> Create(VaccineType vaccineType)
    {
        return await _repository.CreateAsync(vaccineType);
    }

    public async Task<bool> Delete(int id)
    {
        var schedule = await _repository.GetByIdAsync(id);
        if (schedule != null) return await _repository.RemoveAsync(schedule);
        return false;
    }

    public async Task<List<VaccineType>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<VaccineType> GetById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<VaccineType>> Search(string name)
    {
        return await _repository.Search(name);
    }

    public async Task<int> Update(VaccineType healthGuideCategory)
    {
        return await _repository.UpdateAsync(healthGuideCategory);
    }
}