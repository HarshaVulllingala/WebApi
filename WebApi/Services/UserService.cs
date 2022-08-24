using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserrepository _userrepo;
        public UserService(IUserrepository userrepo)
        {
            _userrepo = userrepo;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _userrepo.GetAllUsers();
        }
        public User GetUserById(int id)
        {
            return _userrepo.GetUserByID(id);   
        }
        public void AddUser(User user)
        {

            _userrepo.AddUser(user);     
            
        }
        public void UpdateUser(int id,User user)
        {
            _userrepo.UpdateUser(id,user);
        }
        public void Deleteuser(User user)
        {
            _userrepo.Deleteuser(user);
        }

        public void DeleteUser(IEnumerable<int> usersToDelete)
        {
            _userrepo.DeleteUser(usersToDelete);
        }
        public IEnumerable<User> GetUsersByPageNumber(int pageNumber, int pageSize)
        {
           return _userrepo.GetUsersByPageNumber(pageNumber, pageSize);
        }
    }
}