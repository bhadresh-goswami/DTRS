//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DTRS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserLoginMaster
    {
        public int LoginId { get; set; }
        public string RocketUserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsLogin { get; set; }
        public int UserRole { get; set; }
        public string Location { get; set; }
        public string FullName { get; set; }
        public string ImageName { get; set; }
    
        public virtual RoleMaster RoleMaster { get; set; }
    }
}
