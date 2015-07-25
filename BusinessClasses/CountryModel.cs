using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessClasses
{
    public class CountryMaster 
    {
        public CountryMaster()
        {
            CountryId = 0;
            CountryName = string.Empty;
            IsActive = true;
            CreatedBy = 0;
            CreatedOn = Convert.ToDateTime("1/1/1900");
            ModifiedBy = 0;
            ModifiedOn = Convert.ToDateTime("1/1/1900");
        }
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Please enter Country Name")]
       // [Remote("IsCountryExist", "Country", AdditionalFields = "CountryId", ErrorMessage = "Country name already exist")]
        [Display(Name = "Country Name: ")]
        public string CountryName { get; set; }
        public int TotalResuts { get; set; }
        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
