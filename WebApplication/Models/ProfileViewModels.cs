﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebApplication.Models.ViewModel;

namespace WebApplication.Models
{
    public class ProfileViewModel
    {
        public Common.UserTypeEnum? ModelUserType { get; set; }
    }

    public class AddressViewModel
    {
        public string Id { get; set; }

        public int OwnerType { get; set; }

        public string OwnerID { get; set; }

        public string RegionID { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Please enter your address line 1.", MinimumLength = 1)]
        [Display(Name = "Line 1")]
        public string Line1 { get; set; }

        [StringLength(150, ErrorMessage = "Please enter your address line 1.", MinimumLength = 1)]
        [Display(Name = "Line 2")]
        public string Line2 { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Please enter your address city.", MinimumLength = 1)]
        [Display(Name = "City")]
        public string City { get; set; }

        public IEnumerable<SelectListItem> RegionAliaes { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string RegionAlias { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Please enter your address postcode.", MinimumLength = 1)]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

    }

    public class UserViewModel
    {
        #region Non-editable 
        public string Id { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        #endregion

        #region Editable
        [Required]
        [StringLength(150, ErrorMessage = "Please enter your given or first name.", MinimumLength = 1)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Please enter your family name or surname.", MinimumLength = 1)]
        [Display(Name = "Last name")]
        public string Surname { get; set; }

        [Display(Name = "Contact number")]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }
        #endregion

        [Display(Name = "Registered address")]
        public virtual ICollection<AddressViewModel> Addresses { get; set; }
    }
    
    public class CraveatsDinerViewModel : UserViewModel
    {
    }

    public class PartnerRestaurantViewModel : UserViewModel
    {
    }
}