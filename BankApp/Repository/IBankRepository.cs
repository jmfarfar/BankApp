using BankApp.Entities;

namespace BankApp.Repository
{
    public interface IBankRepository
    {
        public User Login(string login, string password);
        public void Update(string login, string password, decimal newBalance);
        public void Delete(string login, string password, string userToDelete);
    }
}
