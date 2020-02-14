using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movie_catalog.Models
{
    public class MovieCatalogue
    {
        public int id { get; set; }
        public string nameMovie { get; set; }
        public string description { get; set; }
        public DateTime releaseDate { get; set; }
        public string producer { get; set; }
     
        public int userId { get; set; }
        public byte poster { get; set; }
    }
}