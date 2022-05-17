using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;
using Webapi_BitirmeProjesi.Extensions;

namespace Webapi_BitirmeProjesi.Operations.OrganizerOperations.CreateEvent
{
    public class CreateEventCommand
    {
        public CreateEventModel Model { get; set; }
        private readonly EventSystemDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateEventCommand(EventSystemDbContext dbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public void Handle()
        {
            var authMail = _httpContextAccessor.HttpContext.User.GetEmail();
            var user = _dbContext.Users.SingleOrDefault(u=>u.Mail==authMail);
            Organizer organizer = _dbContext.Organizers.SingleOrDefault(o => o.UserId == user.Id);

            Category category = _dbContext.Categories.SingleOrDefault(c => c.Id == Model.CategoryId);
            if (category is null)
                throw new InvalidOperationException("Category not found");

            City city = _dbContext.Cities.SingleOrDefault(c => c.Id == Model.CityId);
            if (city is null)
                throw new InvalidOperationException("City not found");

            Event newEvent = _mapper.Map<Event>(Model);
            newEvent.EventStatus = true;
            newEvent.OrganizerId=organizer.Id;
            newEvent.TicketPrice = newEvent.TicketPrice == null ? newEvent.TicketPrice = 0:newEvent.TicketPrice=newEvent.TicketPrice;
            _dbContext.Events.Add(newEvent);
            _dbContext.SaveChanges();
        }
    }
}
