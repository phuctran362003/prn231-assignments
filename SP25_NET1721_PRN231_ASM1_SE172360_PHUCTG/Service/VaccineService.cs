using Repository;
using Repository.Models;

namespace Service;

public interface IVaccineService
{
    Task<List<Vaccine>> GetAll();
    Task<Vaccine> GetById(int id);
    Task<int> Create(Vaccine vaccine);
    Task<int> Update(Vaccine vaccine);
    Task<bool> Delete(int id);
    Task<List<Vaccine>> Search(string name, string description, string type);
}

public class VaccineService : IVaccineService
{
    private readonly VaccineRepository _repository;

    public VaccineService(VaccineRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Create(Vaccine vaccine)
    {
        return await _repository.CreateAsync(vaccine);
    }

    public async Task<bool> Delete(int id)
    {
        var schedule = await _repository.GetByIdAsync(id);
        if (schedule != null) return await _repository.RemoveAsync(schedule);

        return false;
    }

    public async Task<List<Vaccine>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Vaccine> GetById(int id)
    {
        return await _repository.GetByIdIncludeAsync(id);
    }

    public async Task<List<Vaccine>> Search(string name, string description, string type)
    {
        return await _repository.Search(name, description, type);
    }

    public async Task<int> Update(Vaccine vaccine)
    {
        return await _repository.UpdateAsync(vaccine);
    }
}