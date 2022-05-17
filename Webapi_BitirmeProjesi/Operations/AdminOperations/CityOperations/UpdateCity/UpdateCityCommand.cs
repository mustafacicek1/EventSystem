using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.UpdateCity
{
    public class UpdateCityCommand
    {
        public int CityId { get; set; }
        public UpdateCityModel Model { get; set; }
        private readonly EventSystemDbContext _dbContext;

        public UpdateCityCommand(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            City city = _dbContext.Cities.SingleOrDefault(c=>c.Id == CityId);
            if(city is null)
                throw new InvalidOperationException("City not found");

            city.Name = Model.CityName;
            _dbContext.SaveChanges();
        }
    }
}
