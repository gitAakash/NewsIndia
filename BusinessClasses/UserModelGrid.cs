using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessClasses
{
    /// <summary>
    /// Used to transport the data from various layer to display in the user manager Grid
    /// </summary>
    public class UserModelGrid
    {
        public int UserId { set; get; }
        public int RoleId { set; get; }
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public bool IsAdmin { set; get; }
        public string EmailId { set; get; }
        public string PhoneNumber { set; get; }
    }
}
