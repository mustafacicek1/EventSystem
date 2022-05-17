using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.JWT;

namespace Webapi_BitirmeProjesi.Operations.AuthOperations.CompanyLogin
{
    public class CompanyLoginCommand
    {
        public CompanyLoginModel Model { get; set; }
        private readonly EventSystemDbContext _dbContext;
        private readonly ITokenHelper _tokenHelper;

        public CompanyLoginCommand(EventSystemDbContext dbContext,ITokenHelper tokenHelper)
        {
            _dbContext = dbContext;
            _tokenHelper = tokenHelper;
        }

        public AccessToken Handle()
        {
            var company = _dbContext.Companies.SingleOrDefault(c=>c.Mail==Model.Mail && c.Password==Model.Password);
            if (company is null)
                throw new InvalidOperationException("Email or password is wrong");

            AccessToken token = _tokenHelper.CreateToken(company.CompanyName,company.Mail, company.Role);
            return token;
        }
    }
}
