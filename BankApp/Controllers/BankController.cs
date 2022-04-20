using BankApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using BankApp.Repository;

namespace BankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankRepository _bankRepository;

        public BankController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        // Login API 
        [HttpGet]
        public ActionResult<User> Login(string login, string password)
        {
            var user = _bankRepository.Login(login, password);
            if (user is not null)
            {
                return Ok(user);
            }
            return BadRequest("ERROR: The login or password you entered do not exist!");

        }


        // Create User API (for internal purposes to populate the database)
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            await _bankRepository.Create(user);
            return Ok();
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
