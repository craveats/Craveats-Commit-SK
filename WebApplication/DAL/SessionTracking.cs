//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SessionTracking
    {
        public int Id { get; set; }
        public string SessionID { get; set; }
        public string IPAddress { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<System.DateTime> LoggedInAt { get; set; }
        public Nullable<System.DateTime> LoggedOutAt { get; set; }
    }
}