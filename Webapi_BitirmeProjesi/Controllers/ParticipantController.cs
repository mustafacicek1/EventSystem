using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.CancelParticipating;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetAvaliableCategories;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetAvaliableCities;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetCanceledEvents;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetCouldntJoinedEvents;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetEvents;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetEventsByCategory;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetEventsByCity;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetJoinedEvents;
using Webapi_BitirmeProjesi.Operations.ParticipantOperations.JoinEvent;

namespace Webapi_BitirmeProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Participant")]
    public class ParticipantController : ControllerBase
    {
        private readonly EventSystemDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public ParticipantController(EventSystemDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("events")]
        public IActionResult GetEvents()
        {
            GetEventsQuery query = new GetEventsQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            GetAvaliableCitiesQuery query = new GetAvaliableCitiesQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            GetAvaliableCategoriesQuery query = new GetAvaliableCategoriesQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("events/category/{id}")]
        public IActionResult GetEventsByCategory(int id)
        {
            GetEventsByCategoryQuery query = new GetEventsByCategoryQuery(_dbContext);
            query.CategoryId = id;
            GetEventsByCategoryQueryValidator validator = new GetEventsByCategoryQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpGet("events/city/{id}")]
        public IActionResult GetEventsByCity(int id)
        {
            GetEventsByCityQuery query = new GetEventsByCityQuery(_dbContext);
            query.CityId= id;
            GetEventsByCityQueryValidator validator = new GetEventsByCityQueryValidator();
            validator.ValidateAndThrow(query);
            var result= query.Handle();
            return Ok(result);
        }

        [HttpGet("events/joined")]
        public IActionResult GetJoinedEvents()
        {
            GetJoinedEventsQuery query = new GetJoinedEventsQuery(_dbContext,_contextAccessor);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("events/couldntjoined")]
        public IActionResult GetCouldntJoinedEvents()
        {
            GetCouldntJoinedEventsQuery query = new GetCouldntJoinedEventsQuery(_dbContext,_contextAccessor);
            var reult = query.Handle();
            return Ok(reult);
        }

        [HttpGet("events/canceled")]
        public IActionResult GetCanceledEvents()
        {
            GetCanceledEventsQuery query = new GetCanceledEventsQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpPost("events/join/{id}")]
        public IActionResult JoinEvent(int id)
        {
            JoinEventCommand command = new JoinEventCommand(_dbContext,_contextAccessor);
            command.EventId = id;
            JoinEventCommandValidator validator = new JoinEventCommandValidator();
            validator.ValidateAndThrow(command);
            var result = command.Handle();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            return Ok();
        }

        [HttpPost("events/canceljoin/{id}")]
        public IActionResult CancelJoin(int id)
        {
            CancelParticipatingCommand command = new CancelParticipatingCommand(_dbContext,_contextAccessor);
            command.EventId= id;
            CancelParticipatingCommandValidator validator = new CancelParticipatingCommandValidator();
            validator.ValidateAndThrow(command);
            var result = command.Handle();
            if (result.Count > 0)
            {
                return Ok(result);
            }
            return Ok();
        }
    }
}
