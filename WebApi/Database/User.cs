using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string contact { get; set; }
    }
}