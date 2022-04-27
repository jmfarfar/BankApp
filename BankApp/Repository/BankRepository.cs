using BankApp.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.Repository
{
    public class BankRepository : IBankRepository
    {        
        private readonly BankDbContext _db;

        public BankRepository(BankDbContext db)
        {
            _db = db;
        }

        public async Task Create(User user)
        {
            _db.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(string login, string password, string userToDelete)
        {
            var userLogged = RetrieveUserInfo(login, password);
            if (userLogged is not null && userLogged.Role == Roles.ADMIN)
            {
                //var userDel = _db.Users.First(u => u.Login == userToDelete);
                //_db.Users.Remove(userDel);
                //await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public User Login(string login, string password)
        {
            //var userRetrieved = _db.Users.Where(u => u.Login == login && u.Password == password).ToList();            
            //if (userRetrieved.Count > 0)
            //{
            //    var user = new User
            //    {                    
            //        Id = userRetrieved[0].Id,
            //        Login = userRetrieved[0].Login,
            //        Role = userRetrieved[0].Role,
            //        USDBalance = userRetrieved[0].USDBalance
            //    };
            //    return user;
            //}
            return null;
        }

        public async Task<bool> Update(string login, string password, decimal newBalance)
        {
            var userLogged = RetrieveUserInfo(login, password);
            if (userLogged is not null)
            {
                userLogged.USDBalance = newBalance;
                _db.ChangeTracker.Clear();
                _db.Users.Update(userLogged);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }        

        // method to retrieve all user info(including password) to help the Update method to update the correct registry.
        private User RetrieveUserInfo(string login, string password)
        {
            //var userReq = _db.Users.Where(u => u.Login == login && u.Password == password).ToList();
            //if (userReq.Count > 0)
            //{
            //    var user = new User
            //    {
            //        Id = userReq[0].Id,
            //        Login = userReq[0].Login,
            //        Password = userReq[0].Password,
            //        Role = userReq[0].Role,
            //        USDBalance = userReq[0].USDBalance,
            //    };
            //    return user;
            //}
            return null;
        }
    }
}
