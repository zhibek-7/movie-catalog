using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace movie_catalog.Models
{
    public class IndexViewModel
    {
       
            public IEnumerable<movie> Movies { get; set; }
            public PageInfo PageInfo { get; set; }
       

    }
}