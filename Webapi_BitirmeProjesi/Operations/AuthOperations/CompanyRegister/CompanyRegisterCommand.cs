using AutoMapper;
using System;
using System.Linq;
using Webapi_BitirmeProjesi.Common;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.Operations.AuthOperations.CompanyRegister
{
    public class CompanyRegisterCommand
    {
        public CompanyRegisterModel Model { get; set; }
        private readonly EventSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public CompanyRegisterCommand(EventSystemDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var company=_dbContext.Companies.SingleOrDefault(c=>c.Mail==Model.Mail);
            if (company is not null)
                throw new InvalidOperationException("Company already registered");

            Company newCompany = _mapper.Map<Company>(Model);
            newCompany.Role = RoleEnum.Company.ToString();
            _dbContext.Companies.Add(newCompany);
            _dbContext.SaveChanges();
        }
    }
}
