using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IUserrepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByID(int id);
        void AddUser(User user);
        void UpdateUser(int id, User user);
        void Deleteuser(User user);
        void DeleteUser(IEnumerable<int> usersToDelete);
        IEnumerable<User> GetUsersByPageNumber(int pageNumber, int pageSize);
    }
}
