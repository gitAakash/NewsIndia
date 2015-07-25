using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsIndia.Models
{
    public class CategoryMasterModel
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public bool IsVisible { get; set; }
        public bool IsActive { get; set; }
    }
}