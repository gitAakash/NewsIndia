using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Common;
using EntityProject;


namespace BusinessClasses
{
    public class UserModel
    {
        public UserModel()
        {
            UserId = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            MiddleName = null;
            DateOfBirth = DateTime.Now.Date;
            Gender = true;
            MaritalStatus = 0;
            EmailId = string.Empty;
            Password = string.Empty;
            MobileNumber = string.Empty;
           // AlternateContactNo = string.Empty;
           // UserImage = string.Empty;
            Address = string.Empty;
            City = "";
            State = 0;
            Country = 0;
            PinCode = string.Empty;
            IsActive = false;
            CreatedBy = 0;
            CreatedOn = DateTime.Now;
            ModifiedBy = 0;
            ModifiedOn = DateTime.Now;
           // UserRole = new UserRole();
           // ImageModel = new ProfileImageDimensionViewModel();
        }

        public IEnumerable<SerializableSelectListItem> DropDownForCountry { get; set; }
        public IEnumerable<SerializableSelectListItem> DropDownForState { get; set; } 
       
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        [StringLength(50, ErrorMessage = "The {0} should be only {1} characters long.")]
        [RegularExpression("^[a-zA-Z .]*$", ErrorMessage = "Please enter valid First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        [StringLength(50, ErrorMessage = "The {0} should be only {1} characters long.")]
        [RegularExpression("^[a-zA-Z .]*$", ErrorMessage = "Please enter valid Last Name")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Please enter Middle Name")]
        [StringLength(50, ErrorMessage = "The {0} should be only {1} characters long.")]
        [RegularExpression("^[a-zA-Z .]*$", ErrorMessage = "Please enter valid Middle Name")]
        public string MiddleName { get; set; }
       

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Please enter DOB")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select Gender")]
        public bool Gender { get; set; }


        [Required(ErrorMessage = "Please Select Marital Status")]
        public int MaritalStatus { get; set; }

        [Required(ErrorMessage = "Please enter Email Id")]
       /* [Remote("IsEmailExist", "User", ErrorMessage = "Email already exist")]*/
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password",
            ErrorMessage = "The Password and Confirm Password do not match.")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter Mobile Number")]
        [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Please enter a valid Mobile Number")]
        public string MobileNumber { get; set; }

        public string AlternateContactNo { get; set; }

        public string UserImage { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select State")]
        public int State { get; set; }

        [Required(ErrorMessage = "Please select Country")]
        public int Country { get; set; }

        public int OfficeState { get; set; }

        //public ProfileImageDimensionViewModel ImageModel { get; set; }

        [Required(ErrorMessage = "Please enter Pin Code")]
        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Invalid Pin Code")]
        public string PinCode { get; set; }

        [Required(ErrorMessage = "Please select Role")]
        public int SelectedRoleId { get; set; }

        //public UserRole UserRole { get; set; }

        public bool Status { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
