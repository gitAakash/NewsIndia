using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsIndia.Models
{
    public class Login
    {
        [Required]
        public string UserName { set; get; }
        [Required]
        public string Password { set; get; }

        public bool RememberMe { set; get; }

    }
}