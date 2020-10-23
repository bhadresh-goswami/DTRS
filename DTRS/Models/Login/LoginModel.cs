using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTRS.Models.Login
{
    public class LoginModel
    {
        public int UserId { get; set; }
        public string EmailId { get; set; }     
        public string Password { get; set; }
        public string Role { get; set; }
    }
}