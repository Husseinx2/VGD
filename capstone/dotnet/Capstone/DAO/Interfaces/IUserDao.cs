using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO.Interfaces
{
    public interface IUserDao
    {
        public IList<ReturnUser> GetUsers();
        public IList<ReturnUser> GetActiveUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User CreateUser(string username, string password, string role);
        bool DeleteUser(int userId);

    }
}
