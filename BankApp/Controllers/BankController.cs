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
        private readonly BankDbContext _db;
        private readonly IBankRepository _bankRepository;

        public BankController(BankDbContext db, IBankRepository bankRepository)
        {
            _db = db;
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
            return BadRequest();
            
        }


        // Create User API (for internal purposes to populate the database)
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            _db.Add(user);
            await _db.SaveChangesAsync();
            return Ok();
        }

        // Update Balance API        
        [HttpPut]
        public async Task<ActionResult> UpdateBalance(string login, string password, decimal newBalance)
        {
            _bankRepository.Update(login,password,newBalance);
            return Ok();            
        }

        // Delete User API
    }
}
