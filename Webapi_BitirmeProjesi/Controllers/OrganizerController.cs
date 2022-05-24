using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Operations.OrganizerOperations.CancelEvent;
using Webapi_BitirmeProjesi.Operations.OrganizerOperations.CreateEvent;
using Webapi_BitirmeProjesi.Operations.OrganizerOperations.GetCategoryList;
using Webapi_BitirmeProjesi.Operations.OrganizerOperations.GetCityList;
using Webapi_BitirmeProjesi.Operations.OrganizerOperations.GetEventDetail;
using Webapi_BitirmeProjesi.Operations.OrganizerOperations.GetMyEvents;
using Webapi_BitirmeProjesi.Operations.OrganizerOperations.UpdateEvent;

namespace Webapi_BitirmeProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Organizer")]
    public class OrganizerController : ControllerBase
    {
        private readonly EventSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public OrganizerController(EventSystemDbContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            GetCategoryListQuery query = new GetCategoryListQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            GetCityListQuery query = new GetCityListQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("myevents")]
        public IActionResult GetMyEvents()
        {
            GetMyEventsQuery query = new GetMyEventsQuery(_dbContext,_contextAccessor);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("events/{id}")]
        public IActionResult GetEventDetail(int id)
        {
            GetEventDetailQuery query = new GetEventDetailQuery(_dbContext);
            query.EventId = id;
            GetEventDetailQueryValidator validator = new GetEventDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost("events")]
        public IActionResult CreateEvent(CreateEventModel eventModel)
        {
            CreateEventCommand command = new CreateEventCommand(_dbContext,_contextAccessor,_mapper);
            command.Model= eventModel;
            CreateEventCommandValidator validator = new CreateEventCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Created("", eventModel);
        }

        [HttpPost("cancelevent/{id}")]
        public IActionResult CancelEvent(int id)
        {
            CancelEventCommand command = new CancelEventCommand(_dbContext,_contextAccessor);
            command.EventId= id;
            CancelEventCommandValidator validator = new CancelEventCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPatch("events/{id}")]
        public IActionResult UpdateEvent(int id,UpdateEventModel eventModel)
        {
            UpdateEventCommand command = new UpdateEventCommand(_dbContext,_contextAccessor);
            command.EventId = id;
            command.Model= eventModel;
            UpdateEventCommandValidator validator = new UpdateEventCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
