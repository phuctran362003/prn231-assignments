using VaccinaCare.Application.Interface;
using VaccinaCare.Domain.Entities;

namespace VaccinaCare.GraphQLApiService.Resolvers
{
    public class Mutation
    {
        private readonly IVaccineService _vaccineService;

        public Mutation(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        public async Task<Vaccine> CreateVaccine(string vaccineName, string description, decimal price)
        {
            return await _vaccineService.CreateVaccineAsync(vaccineName, description, price);
        }
    }

}
