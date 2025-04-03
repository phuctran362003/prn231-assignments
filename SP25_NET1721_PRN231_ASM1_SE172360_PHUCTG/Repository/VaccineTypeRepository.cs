using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Models;

namespace Repository;

public class VaccineTypeRepository : GenericRepository<VaccineType>
{
    public VaccineTypeRepository()
    {
        
    }
    public async Task<List<VaccineType>> GetAll()
    {
        var entities = await _context.VaccineTypes.ToListAsync();
        return entities;
    }

    public async Task<List<VaccineType>> Search(string name)
    {
        var items = await _context.VaccineTypes
            .Where(s => s.Name.Contains(name) || string.IsNullOrEmpty(name))
            .ToListAsync();

        return items;
    }
}