using System.Collections.Generic;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;

namespace Webapi_BitirmeProjesi.Operations.OrganizerOperations.GetCityList
{
    public class GetCityListQuery
    {
        private readonly EventSystemDbContext _dbContext;

        public GetCityListQuery(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CitiesViewModel> Handle()
        {
            var cities = _dbContext.Cities.Select(c => new CitiesViewModel
            {
                CityId = c.Id,
                CityName = c.Name,
            }).ToList();

            return cities;
        }
    }
}
