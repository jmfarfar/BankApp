using BankApp.Entities;
using System.Threading.Tasks;

namespace BankApp.Repository
{
    public interface IBankRepository
    {
        public User Login(string login, string password);
        public Task Create(User user);
        public Task<bool> Update(string login, string password, decimal newBalance);
        public Task<bool> Delete(string login, string password, string userToDelete);
    }
}
