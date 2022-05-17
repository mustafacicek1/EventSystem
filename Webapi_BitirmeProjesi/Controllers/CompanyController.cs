using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.Extensions;
using Webapi_BitirmeProjesi.Operations.CompanyOperations.GetEventList;
using Webapi_BitirmeProjesi.Services;

namespace Webapi_BitirmeProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Company")]
    public class CompanyController : ControllerBase
    {
        private readonly EventSystemDbContext _dbContext;
        private readonly ILoggerService _loggerService;

        public CompanyController(EventSystemDbContext dbContext, ILoggerService loggerService)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
        }

        [HttpGet("events")]
        public IActionResult GetEvents()
        {
            string companyName = HttpContext.User.GetName();
            string logMessage = $"CompanyName : {companyName} --- Date : {DateTime.Now}";
            _loggerService.Log(logMessage);

            GetEventListQuery query = new GetEventListQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        }
    }
}
