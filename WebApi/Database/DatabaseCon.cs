using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Database
{
    public class DatabaseCon : DbContext
    {
        public DatabaseCon(): base("DbConnection")
        {

        }
        public DbSet<User> Users { get; set; }
    }
}