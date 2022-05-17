using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.DeleteCity
{
    public class DeleteCityCommand
    {
        public int CityId { get; set; }

        private readonly EventSystemDbContext _dbContext;

        public DeleteCityCommand(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            City city = _dbContext.Cities.SingleOrDefault(c => c.Id == CityId);
            if(city is null)
                throw new InvalidOperationException("City not found");

            _dbContext.Remove(city);
            _dbContext.SaveChanges();
        }
    }
}
