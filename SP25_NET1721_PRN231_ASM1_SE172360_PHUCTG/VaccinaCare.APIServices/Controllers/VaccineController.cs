using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.Models;
using Service;

namespace VaccinaCare.APIServices.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VaccineController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IVaccineService _vaccineService;

    public VaccineController(IConfiguration configuration, IVaccineService vaccineService)
    {
        _configuration = configuration;
        _vaccineService = vaccineService;
    }

    // [Authorize(Roles = "1, 2")]
    [HttpGet]
    [EnableQuery]
    public async Task<IEnumerable<Vaccine>> Get()
    {
        return await _vaccineService.GetAll();
    }

    // [Authorize(Roles = "1, 2")]
    [HttpGet("{id}")]
    public async Task<Vaccine> Get(int id)
    {
        return await _vaccineService.GetById(id);
    }

    // [Authorize(Roles = "1, 2")]
    [HttpGet("{expertResponse}/{location}/{name}")]
    public async Task<IEnumerable<Vaccine>> Get(string expertResponse, string location, string name)
    {
        return await _vaccineService.Search(expertResponse, location, name);
    }

    // [Authorize(Roles = "1, 2")]
    [HttpPost]
    public async Task<int> Post(Vaccine healthGuide)
    {
        return await _vaccineService.Create(healthGuide);
    }

    // [Authorize(Roles = "1, 2")]
    [HttpPut("{id}")]
    public async Task<int> Put(Vaccine healthGuide)
    {
        return await _vaccineService.Update(healthGuide);
    }

    // [Authorize(Roles = "1, 2")]
    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _vaccineService.Delete(id);
    }
}