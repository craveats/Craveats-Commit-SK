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
    
    [Serializable]
    public partial class AuthenticateUser_Result
    {
        public int Id { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public Nullable<int> UserStatus { get; set; }
        public Nullable<int> UserTypeFlag { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string ResetCode { get; set; }
        public Nullable<System.DateTime> ResetCodeSentAt { get; set; }
        public Nullable<System.DateTime> ResetCodeExpiry { get; set; }
        public Nullable<int> AddressId { get; set; }
        public string ProfileAssetUrl { get; set; }
    }
}
