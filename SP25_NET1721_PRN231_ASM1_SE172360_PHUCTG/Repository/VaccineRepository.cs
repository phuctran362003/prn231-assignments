using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Models;

namespace Repository;

public class VaccineRepository : GenericRepository<Vaccine>
{
    public async Task<List<Vaccine>> GetAll()
    {
        var entities = await _context.Vaccines.Include(x => x.VaccineType).ToListAsync();
        return entities;
    }

    public async Task<Vaccine> GetByIdIncludeAsync(int id)
    {
        var entity = await _context.Vaccines.Include(x => x.VaccineType).FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null) _context.Entry(entity).State = EntityState.Detached;

        return entity;
    }

    public async Task<List<Vaccine>> Search(string name, string description, string type)
    {
        var schedules = await _context.Vaccines
            .Include(st => st.VaccineType)
            .Where(s =>
                s.VaccineName.Contains(name) || (string.IsNullOrEmpty(name)
                                                 && (s.Description.Contains(description) ||
                                                     string.IsNullOrEmpty(description))
                                                 && (s.VaccineType.Name.Contains(type) || string.IsNullOrEmpty(type)
                                                 ))).ToListAsync();

        return schedules;
    }
}