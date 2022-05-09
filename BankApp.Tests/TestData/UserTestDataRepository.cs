using BankApp.Entities;
using BankApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Tests.TestData
{
    public class UserTestDataRepository : IBankRepository
    {
        private List<User> _users;

        public UserTestDataRepository()
        {
            _users = new List<User>
            {
                new User()
                {
                    Id = 1,
                    Login = "a",
                    Password = "a",
                    Role = Roles.ADMIN,
                    USDBalance = 100
                },
                new User()
                {
                    Id=2,
                    Login = "b",
                    Password = "b",
                    Role = Roles.USER,
                    USDBalance=100
                }
            };
        }

        public Task Create(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string login, string password, string userToDelete)
        {
            var delUser = _users.Find(u => u.Login == userToDelete);
            return Task.FromResult(_users.Remove(delUser));
            
        }

        public User Login(string login, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(string login, string password, decimal newBalance)
        {
            throw new NotImplementedException();
        }
    }
}
