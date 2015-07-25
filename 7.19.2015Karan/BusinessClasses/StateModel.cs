using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common;

namespace BusinessClasses
{
        public class StateMaster 
        {
            public StateMaster()
            {
                StateId = 0;
                StateName = string.Empty;
                IsActive = true;
                CreatedBy = 0;
                CreatedOn = Convert.ToDateTime("1/1/1900");
                ModifiedBy = 0;
                ModifiedOn = Convert.ToDateTime("1/1/1900");
                CountryId = 0;
                CountryName = string.Empty;
            }
            public int StateId { get; set; }

            [Required(ErrorMessage = "Please enter State Name")]
            //[Remote("IsStateExist", "State", AdditionalFields = "StateId", ErrorMessage = "State name already exist")]
            [Display(Name = "State Name: ")]
            public string StateName { get; set; }
            public IEnumerable<SerializableSelectListItem> DropDownForCountry { get; set; }
            [Required(ErrorMessage = "Please select Country")]
            public int CountryId { get; set; }
            public int TotalResults { get; set; }
            public string CountryName { get; set; }
            public int ModifiedBy { get; set; }

            public DateTime ModifiedOn { get; set; }

            public int CreatedBy { get; set; }

            public DateTime CreatedOn { get; set; }

            public bool IsActive { get; set; }

        }
    }

