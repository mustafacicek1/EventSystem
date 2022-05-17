using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.JWT;

namespace Webapi_BitirmeProjesi.Operations.AuthOperations.UserLogin
{
    public class UserLoginCommand
    {
        public UserLoginModel Model { get; set; }
        private readonly EventSystemDbContext _dbContext;
        private readonly ITokenHelper _tokenHelper;

        public UserLoginCommand(EventSystemDbContext dbContext,ITokenHelper tokenHelper)
        {
            _dbContext = dbContext;
            _tokenHelper = tokenHelper;
        }

        public AccessToken Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(u=>u.Mail==Model.Mail && u.Password==Model.Password);
            if (user is null)
                throw new InvalidOperationException("Email or password is wrong!");


            string fullName=user.FirstName+" "+user.LastName;
            AccessToken token= _tokenHelper.CreateToken(fullName,user.Mail, user.Role);
            return token;
        }
    }
}
