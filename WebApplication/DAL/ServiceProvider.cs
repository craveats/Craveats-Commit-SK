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
    
    public partial class ServiceProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brief { get; set; }
        public string Detail { get; set; }
        public Nullable<int> AddressId { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<int> ServiceProviderStatus { get; set; }
        public string AssetUrl { get; set; }
        public Nullable<int> PartnerUserId { get; set; }
        public Nullable<int> ReviewedBy { get; set; }
        public Nullable<System.DateTime> ReviewDate { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}