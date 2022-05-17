using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.CreateCity
{
    public class CreateCityCommand
    {
        public CreateCityModel Model { get; set; }
        private readonly EventSystemDbContext _dbContext;

        public CreateCityCommand(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            City city = _dbContext.Cities.SingleOrDefault(c=>c.Name==Model.CityName);
            if (city is not null)
                throw new InvalidOperationException("City already exist");
            City newCity = new City();
            newCity.Name = Model.CityName;
            _dbContext.Cities.Add(newCity);
            _dbContext.SaveChanges();
        }
    }
}
