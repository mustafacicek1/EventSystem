using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.CreateCategory;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.DeleteCategory;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.GetCategories;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.GetCategoryDetail;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CategoryOperations.UpdateCategory;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.CreateCity;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.DeleteCity;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.GetCities;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.GetCityDetail;
using Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.UpdateCity;

namespace Webapi_BitirmeProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly EventSystemDbContext _dbContext;

        public AdminController(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            GetCategoriesQuery query = new GetCategoriesQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("categories/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            GetCategoryDetailQuery query = new GetCategoryDetailQuery(_dbContext);
            query.CategoryId=id;
            GetCategoryDetailQueryValidator validator = new GetCategoryDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost("categories")]
        public IActionResult CreateCategory(CreateCategoryModel categoryModel)
        {
            CreateCategoryCommand command = new CreateCategoryCommand(_dbContext);
            command.Model = categoryModel;
            CreateCategoryCommandValidator validator = new CreateCategoryCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Created("", categoryModel);
        }

        [HttpPut("categories/{id}")]
        public IActionResult UpdateCategory(int id, UpdateCategoryModel categoryModel)
        {
            UpdateCategoryCommand command = new UpdateCategoryCommand(_dbContext);
            command.CategoryId = id;
            command.Model = categoryModel;
            UpdateCategoryCommandValidator validator = new UpdateCategoryCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("categories/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            DeleteCategoryCommand command = new DeleteCategoryCommand(_dbContext);
            command.CategoryId = id;
            DeleteCategoryCommandValidator validator = new DeleteCategoryCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return NoContent();
        }

        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            GetCitiesQuery query = new GetCitiesQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        }
        
        [HttpGet("cities/{id}")]
        public IActionResult GetCityById(int id)
        {
            GetCityDetailQuery query = new GetCityDetailQuery(_dbContext);
            query.CityId = id;
            GetCityDetailQueryValidator validator = new GetCityDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost("cities")]
        public IActionResult CreateCity(CreateCityModel cityModel)
        {
            CreateCityCommand command = new CreateCityCommand(_dbContext);
            command.Model = cityModel;
            CreateCityCommandValidator validator = new CreateCityCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Created("", cityModel);
        }

        [HttpPut("cities/{id}")]
        public IActionResult UpdateCity(int id, UpdateCityModel cityModel)
        {
            UpdateCityCommand command = new UpdateCityCommand(_dbContext);
            command.CityId = id;
            command.Model = cityModel;
            UpdateCityCommandValidator validator = new UpdateCityCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete("cities/{id}")]
        public IActionResult DeleteCity(int id)
        {
            DeleteCityCommand command = new DeleteCityCommand(_dbContext);
            command.CityId = id;
            DeleteCityCommandValidator validator = new DeleteCityCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return NoContent();
        }
    }
}
