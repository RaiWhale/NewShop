//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechnologyShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserLevelPermission
    {
        public int Id { get; set; }
        public int UserLevelId { get; set; }
        public string ControllerName { get; set; }
        public byte PermissionCode { get; set; }
    
        public virtual UserLevel UserLevel { get; set; }
    }
}
