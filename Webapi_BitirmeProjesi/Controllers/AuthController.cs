using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Webapi_BitirmeProjesi.Operations.AuthOperations.UserLogin;
using Webapi_BitirmeProjesi.Operations.AuthOperations.UserRegister;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.JWT;
using FluentValidation;
using AutoMapper;
using Webapi_BitirmeProjesi.Operations.AuthOperations.CompanyLogin;
using Webapi_BitirmeProjesi.Operations.AuthOperations.CompanyRegister;
using Webapi_BitirmeProjesi.DTOs;

namespace Webapi_BitirmeProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EventSystemDbContext _dbContext;
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;

        public AuthController(EventSystemDbContext dbContext,ITokenHelper tokenHelper,IMapper mapper)
        {
            _dbContext = dbContext;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        [HttpPost("user/register")]
        public IActionResult UserRegister(UserRegisterModel registerModel)
        {
            UserRegisterCommand command = new UserRegisterCommand(_dbContext,_mapper);
            command.Model= registerModel;
            UserRegisterCommandValidator validator= new UserRegisterCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPost("user/login")]
        public IActionResult UserLogin(UserLoginModel loginModel)
        {
            UserLoginCommand command = new UserLoginCommand(_dbContext,_tokenHelper);
            command.Model= loginModel;
            UserLoginCommandValidator validator = new UserLoginCommandValidator();
            validator.ValidateAndThrow(command);
            AccessToken token =command.Handle();
            return Ok(token);
        }

        [HttpPost("company/login")]
        public IActionResult CompanyLogin(CompanyLoginModel loginModel)
        {
            CompanyLoginCommand command = new CompanyLoginCommand(_dbContext, _tokenHelper);
            command.Model = loginModel;
            CompanyLoginCommandValidator validator= new CompanyLoginCommandValidator();
            validator.ValidateAndThrow(command);
            AccessToken token = command.Handle();
            return Ok(token);
        }

        [HttpPost("company/register")]
        public IActionResult CompanyRegister(CompanyRegisterModel registerModel)
        {
            CompanyRegisterCommand command = new CompanyRegisterCommand(_dbContext,_mapper);
            command.Model = registerModel;
            CompanyRegisterCommandValidator validator = new CompanyRegisterCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
