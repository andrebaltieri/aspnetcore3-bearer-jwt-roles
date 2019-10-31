using System.Collections.Generic;
using System.Linq;
using Shop.Models;

namespace Shop.Repositories
{
    public class UserRepository
    {
        private IList<User> _users;
        public UserRepository()
        {
            _users = new List<User>();
            var manger = new User { Id = 1, Username = "batman", Password = "batman", Role = "manager" };
            var employee = new User { Id = 2, Username = "robin", Password = "robin", Role = "employee" };
            _users.Add(manger);
            _users.Add(employee);
        }
        public User Get(string username, string password)
        {
            return _users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}