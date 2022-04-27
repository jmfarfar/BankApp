using AutoMapper;
using BankApp.Core.DTO;
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
        public UserRegisterHandler(BankDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
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
                return userDTO;
            }

            throw new Exception("Unable to register the user");
        }
    }
}
