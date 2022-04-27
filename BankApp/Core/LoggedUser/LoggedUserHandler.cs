using AutoMapper;
using BankApp.Core.DTO;
using BankApp.Core.JWT;
using BankApp.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Core.LoggedUser
{
    public class LoggedUserHandler : IRequestHandler<LoggedUserCommand, UserDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserSession _userSession;
        private readonly IJWTGenerator _jwtGenerator;
        private readonly IMapper _mapper;

        public LoggedUserHandler(UserManager<User> userManager, IUserSession userSession, IJWTGenerator jwtGenerator, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userSession = userSession;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserDTO> Handle(LoggedUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(_userSession.GetUserSession());
            if (user != null)
            {
                var userDTO = _mapper.Map<User, UserDTO>(user);
                userDTO.Token = _jwtGenerator.CreateToken(user);
                return userDTO;
            }

            throw new Exception("User not found");
        }
    }
}
