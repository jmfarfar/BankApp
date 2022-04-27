using AutoMapper;
using BankApp.Core.DTO;
using BankApp.Core.JWT;
using BankApp.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace BankApp.Core.Login
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, UserDTO>
    {
        private readonly BankDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJWTGenerator _jwtGenerator;
        private readonly SignInManager<User> _signInManager;

        public UserLoginHandler(BankDbContext context, UserManager<User> userManager, IMapper mapper, IJWTGenerator jwtGenerator, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
            _signInManager = signInManager;
        }

        public async Task<UserDTO> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            
            if (user == null)
            {
                throw new Exception("The user doesn't exist");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                var userDTO = _mapper.Map<User, UserDTO>(user);
                userDTO.Token = _jwtGenerator.CreateToken(user);
                return userDTO;
            }

            throw new Exception("Login Failed");
        
        }
    }
}
