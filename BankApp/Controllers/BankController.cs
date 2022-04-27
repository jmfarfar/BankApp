using BankApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using BankApp.Repository;
using BankApp.Core.DTO;
using BankApp.Core.Register;
using MediatR;
using BankApp.Core.Login;
using BankApp.Core.LoggedUser;

namespace BankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _bankRepository;
        private readonly IMediator _mediator;

        public BankController(IBankRepository bankRepository, IMediator mediator)
        {
            _bankRepository = bankRepository;
            _mediator = mediator;
        }

        // Create User API (for internal purposes to populate the database)
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> CreateUser(UserRegisterCommand param)
        {
            return await _mediator.Send(param);
        }

        // Login API 
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserLoginCommand param)
        {            
            return await _mediator.Send(param);
        }        

        [HttpGet]
        public async Task<ActionResult<UserDTO>> Get()
        {
            return await _mediator.Send(new LoggedUserCommand());
        }

        // Update Balance API        
        [HttpPut]
        public async Task<ActionResult> UpdateBalance(string login, string password, decimal newBalance)
        {
            var result = await _bankRepository.Update(login, password, newBalance);
            if (result)
                return Ok();
            return BadRequest("ERROR: The balance cannot be updated");
        }

        // Delete User API
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string login, string password, string userToDelete)
        {
            var result = await _bankRepository.Delete(login, password, userToDelete);
            if (result)
                return Ok();
            return BadRequest("ERROR: The user cannot be removed");
        }
    }
}
