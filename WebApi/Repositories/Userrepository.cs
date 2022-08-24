using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Database;
using WebApi.Models;


namespace WebApi.Repositories
{
    public class Userrepository: IUserrepository
    {
        private readonly DatabaseCon db;
        public Userrepository(DatabaseCon dbcontext)
        {
            db = dbcontext;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return db.Users.ToList();
        }
        public User GetUserByID(int id)
        {
            return db.Users.Find(id);
        }
        public void AddUser(User user)
        {
                db.Users.Add(user);
                db.SaveChanges();
            
            
        }
        public void UpdateUser(int id,User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }
        public void Deleteuser(User user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }
        public void DeleteUser(IEnumerable<int> usersToDelete)
        {
            IEnumerable<User> users = db.Users.Where(x => usersToDelete.Contains(x.id)).ToList();
            db.Users.RemoveRange(users);
            db.SaveChanges();
        }
        public IEnumerable<User> GetUsersByPageNumber(int pageNumber, int pageSize)
        {
            int startRow = ((pageNumber - 1) * pageSize) + 1;
            int endRow = pageNumber * pageSize;
            var users= db.Users.OrderBy(x => x.id).Where(x=> x.id>=startRow && x.id<=endRow).ToList<User>();
            return users;


        }
    }
}