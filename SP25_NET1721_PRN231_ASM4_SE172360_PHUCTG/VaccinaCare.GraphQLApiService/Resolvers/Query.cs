using VaccinaCare.Application.Interface;
using VaccinaCare.Domain.Entities;

namespace VaccinaCare.API.Resolvers
{
    public class Query
    {
        private readonly IVaccineService _vaccineService;

        public Query(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        [GraphQLName("getAllVaccines")] // Định danh tên chính xác
        public async Task<List<Vaccine>> GetAllVaccines()
        {
            return await _vaccineService.GetAllVaccinesAsync();
        }
    }
}
