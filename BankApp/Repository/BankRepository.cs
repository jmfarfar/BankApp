using BankApp.Entities;
using System.Linq;

namespace BankApp.Repository
{
    public class BankRepository : IBankRepository
    {
        // inject context
        private readonly BankDbContext _db;

        public BankRepository(BankDbContext db)
        {
            _db = db;
        }

        public void Delete(string login, string password, string userToDelete)
        {
            var userLogged = RetrieveUserInfo(login, password);
            if (userLogged is not null && userLogged.Role == Roles.ADMIN)
            {
                var userDel = _db.Users.First<User>(u => u.Login == userToDelete);
                _db.Users.Remove(userDel);
                _db.SaveChanges();
            }
        }

        public User Login(string login, string password)
        {
            var userRetrieved = _db.Users.Where(u => u.Login == login && u.Password == password);
            if (userRetrieved is not null)
            {
                var user = new User
                {
                    Id = userRetrieved.First().Id,
                    Login = userRetrieved.First().Login,
                    Role = userRetrieved.First().Role,
                    USDBalance = userRetrieved.First().USDBalance
                };
                return user;
            }
            return null;
        }

        public async void Update(string login, string password, decimal newBalance)
        {
            var userLogged = RetrieveUserInfo(login, password);
            if (userLogged is not null)
            {
                userLogged.USDBalance = newBalance;
                _db.ChangeTracker.Clear();
                _db.Users.Update(userLogged);
                await _db.SaveChangesAsync();
            }
        }        

        // method to retrieve all user info including password to help the Update method to update the correct registry.
        private User RetrieveUserInfo(string login, string password)
        {
            var userReq = _db.Users.Where(u => u.Login == login && u.Password == password);
            if (userReq is not null)
            {
                var user = new User
                {
                    Id = userReq.First().Id,
                    Login = userReq.First().Login,
                    Password = userReq.First().Password,
                    Role = userReq.First().Role,
                    USDBalance = userReq.First().USDBalance,
                };
                return user;
            }
            return null;
        }
    }
}
