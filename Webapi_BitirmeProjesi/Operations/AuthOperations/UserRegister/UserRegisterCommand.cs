using AutoMapper;
using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;

namespace Webapi_BitirmeProjesi.Operations.AuthOperations.UserRegister
{
    public class UserRegisterCommand
    {
        public UserRegisterModel Model { get; set; }
        private readonly EventSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRegisterCommand(EventSystemDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(u=>u.Mail==Model.Mail);
            if (user is not null)
                throw new InvalidOperationException("This mail is already registered");

            User newUser = _mapper.Map<User>(Model);
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            if (Model.RoleId==1)
            {
                Organizer organizer = new Organizer();
                organizer.UserId = newUser.Id;
                _dbContext.Organizers.Add(organizer);
                _dbContext.SaveChanges();
            }
            if (Model.RoleId==2)
            {
                Participant participant = new Participant();
                participant.UserId= newUser.Id;
                _dbContext.Participants.Add(participant);
                _dbContext.SaveChanges();
            }
        }
    }
}
