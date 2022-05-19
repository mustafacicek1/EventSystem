using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.GetCityDetail
{
    public class GetCityDetailQuery
    {
        public int CityId { get; set; }
        private readonly EventSystemDbContext _dbContext;

        public GetCityDetailQuery(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CitiesViewModel Handle()
        {
            var city = _dbContext.Cities.SingleOrDefault(c => c.Id == CityId);
            if (city is null)
                throw new InvalidOperationException("City not found");

            CitiesViewModel vm = new CitiesViewModel();
            vm.CityId = city.Id;
            vm.CityName = city.Name;
            return vm;
        }
    }
}
