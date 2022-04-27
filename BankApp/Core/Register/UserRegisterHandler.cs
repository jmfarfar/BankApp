using AutoMapper;
using BankApp.Core.DTO;
using BankApp.Core.JWT;
using BankApp.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace BankApp.Core.Register
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserDTO>
    {
        private readonly BankDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJWTGenerator _jwtGenerator;
        public UserRegisterHandler(BankDbContext context, UserManager<User> userManager, IMapper mapper, IJWTGenerator jwtGenerator)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
        }
        public async Task<UserDTO> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var anyUser = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();

            if (anyUser)
            {
                throw new Exception("Username exist already");
            }

            var user = new User
            {
                UserName = request.Username,
                Name = request.Name,
                Role = request.Role,
                USDBalance = request.USDBalance
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var userDTO = _mapper.Map<User, UserDTO>(user);
                userDTO.Token = _jwtGenerator.CreateToken(user);
                return userDTO;
            }

            throw new Exception("Unable to register the user");
        }
    }
}
