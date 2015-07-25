using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsIndia.Models
{
    public class SubCategoryMasterModel
    {
        public int ID { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsVisible { get; set; }
        public bool IsActive { get; set; }
    }
}