using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movie_catalog.Models
{
    public class User
    {
        public class MovieCatalogue
        {
            public int id { get; set; }
            public string name { get; set; }
            public string login { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string confirmPassword { get; set; }
        }
    }
}