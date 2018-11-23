using Generic.Obfuscation.TripleDES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Models.ViewModel;

namespace WebApplication.Controllers
{
    public class ProfileController : Controller
    {
        public ProfileController()
        {
            //SessionManager.RegisterSessionActivity();
        }

        // GET: Profile
        public ActionResult Index()
        {
            return View("ProfileView");
        }

        public ActionResult ProfileView(ProfileViewModel model)
        {
            model = new ProfileViewModel();

            if (Session != null && Session.Contents != null)
            {
                AuthenticatedUserInfo authenticatedUserInfo = Session["loggeduser"] as AuthenticatedUserInfo;

                if (authenticatedUserInfo != null)
                {
                    UserDTO userDTO = EntityDTOHelper.GetEntityDTO<DAL.User, UserDTO>(new CEUserManager().FindById(
                        int.Parse(DataSecurityTripleDES.GetPlainText(authenticatedUserInfo.UserId))));

                    model.ModelUserType = (Common.UserTypeEnum) userDTO.UserTypeFlag;

                    return View(model);
                }
            }

            ModelState.AddModelError(string.Empty, "Session has expired");
            return View("ProfileView", null);
        }

        public ActionResult CraveatsDiner(CraveatsDinerViewModel model)
        {
            if (Session != null && Session.Contents != null)
            {
                AuthenticatedUserInfo authenticatedUserInfo = Session["loggeduser"] as AuthenticatedUserInfo;

                if (authenticatedUserInfo != null)
                {
                    UserDTO userDTO = EntityDTOHelper.GetEntityDTO<DAL.User, UserDTO>(new CEUserManager().FindById(
                        int.Parse(DataSecurityTripleDES.GetPlainText(authenticatedUserInfo.UserId))));

                    CraveatsDinerViewModel craveatsDinerViewModel = null;

                    if (((Common.UserTypeEnum)userDTO.UserTypeFlag).HasFlag(Common.UserTypeEnum.CraveatsDiner))
                    {
                        craveatsDinerViewModel = new CraveatsDinerViewModel()
                        {
                            Id = userDTO.Id,
                            ContactNumber = userDTO.ContactNumber,
                            Email = userDTO.EmailAddress,
                            FirstName = userDTO.FirstName,
                            Surname = userDTO.Surname,
                            Role = Common.UserTypeEnum.PartnerRestaurant.ToString()
                        };
                    }

                    DataProvider dataProvider = new DataProvider();

                    DAL.Address anAddress = dataProvider.FindAddressById(
                        int.Parse(DataSecurityTripleDES.GetPlainText(userDTO.AddressId)));

                    AddressViewModel addressViewModel = EntityDTOHelper.GetEntityDTO<DAL.Address, AddressViewModel>(anAddress);

                    if (anAddress != null)
                    {
                        DAL.Region region = dataProvider.FindRegionById(anAddress.RegionId ?? 0);

                        if (region != null)
                        {
                            addressViewModel.RegionAlias = region.RegionAlias;
                            addressViewModel.RegionID = DataSecurityTripleDES.GetEncryptedText(region.Id);
                        }

                        craveatsDinerViewModel.Addresses = new List<AddressViewModel>() { addressViewModel };
                    }

                    return View("CraveatsDiner", craveatsDinerViewModel);
                }

            }

            return View("Error");
        }

        public ActionResult PartnerRestaurant(PartnerRestaurantViewModel model)
        {
            if (Session != null && Session.Contents != null)
            {
                AuthenticatedUserInfo authenticatedUserInfo = Session["loggeduser"] as AuthenticatedUserInfo;

                if (authenticatedUserInfo != null)
                {
                    UserDTO userDTO = EntityDTOHelper.GetEntityDTO<DAL.User, UserDTO>(new CEUserManager().FindById(
                        int.Parse(DataSecurityTripleDES.GetPlainText(authenticatedUserInfo.UserId))));

                    PartnerRestaurantViewModel partnerRestaurantViewModel = null;

                    if (((Common.UserTypeEnum)userDTO.UserTypeFlag).HasFlag(Common.UserTypeEnum.PartnerRestaurant))
                    {
                        partnerRestaurantViewModel = new PartnerRestaurantViewModel()
                        {
                            Id = userDTO.Id,
                            ContactNumber = userDTO.ContactNumber,
                            Email = userDTO.EmailAddress,
                            FirstName = userDTO.FirstName,
                            Surname = userDTO.Surname,
                            Role = Common.UserTypeEnum.PartnerRestaurant.ToString()
                        };
                    }
                    

                    DataProvider dataProvider = new DataProvider();

                    DAL.Address anAddress = dataProvider.FindAddressById(
                        int.Parse(DataSecurityTripleDES.GetPlainText(userDTO.AddressId)));

                    AddressViewModel addressViewModel = EntityDTOHelper.GetEntityDTO<DAL.Address, AddressViewModel>(anAddress);

                    if (anAddress != null)
                    {
                        DAL.Region region = dataProvider.FindRegionById(anAddress.RegionId ?? 0);

                        if (region != null)
                        {
                            addressViewModel.RegionAlias = region.RegionAlias;
                            addressViewModel.RegionID = DataSecurityTripleDES.GetEncryptedText(region.Id);
                        }

                        partnerRestaurantViewModel.Addresses = new List<AddressViewModel>() { addressViewModel };
                    }

                    return View("PartnerRestaurant", partnerRestaurantViewModel);
                    
                }

            }

            return View("Error");
        }

        //public ActionResult Index()
        //{
        //    ProfileViewModel model = new ProfileViewModel();

        //    if (Session != null && Session.Contents != null)
        //    {
        //        AuthenticatedUserInfo authenticatedUserInfo = Session["loggeduser"] as AuthenticatedUserInfo;

        //        if (authenticatedUserInfo != null)
        //        {
        //            UserDTO userDTO = EntityDTOHelper.GetEntityDTO<DAL.User, UserDTO>(new CEUserManager().FindById(
        //                int.Parse(DataSecurityTripleDES.GetPlainText(authenticatedUserInfo.UserId))));

        //            model.ModelUserType = (Common.UserTypeEnum)userDTO.UserTypeFlag;

        //            return View(model);
        //        }
        //    }

        //    ModelState.AddModelError("Error", "Session has expired");
        //    return View(model); 
        //}

        //IsPartnerRestaurant = FindIfIsPartnerRestaurant();

        //private object FindIfIsPartnerRestaurant()
        //{
        //    throw new NotImplementedException();
        //}

        //public ActionResult DetailFromSessionDTO()
        //{
        //    if (Session != null && Session.Contents != null) {

        //        AuthenticatedUserInfo authenticatedUserInfo = Session["loggeduser"] as AuthenticatedUserInfo;

        //        if (authenticatedUserInfo != null) {
        //            UserDTO userDTO = EntityDTOHelper.GetEntityDTO<DAL.User, UserDTO>(new CEUserManager().FindById(
        //                int.Parse(DataSecurityTripleDES.GetPlainText(authenticatedUserInfo.UserId))));

        //            PartnerRestaurantViewModel partnerRestaurantViewModel = null;
        //            CraveatsDinerViewModel craveatsDinerViewModel = null;

        //            if (((Common.UserTypeEnum)userDTO.UserTypeFlag).HasFlag(Common.UserTypeEnum.PartnerRestaurant)) {
        //                partnerRestaurantViewModel = new PartnerRestaurantViewModel()
        //                {
        //                    Id = userDTO.Id,
        //                    ContactNumber = userDTO.ContactNumber,
        //                    Email = userDTO.EmailAddress,
        //                    FirstName = userDTO.FirstName,
        //                    Surname = userDTO.Surname,
        //                    Role = Common.UserTypeEnum.PartnerRestaurant.ToString()
        //                };
        //            }
        //            else {

        //                craveatsDinerViewModel = new CraveatsDinerViewModel()
        //                {
        //                    Id = userDTO.Id,
        //                    ContactNumber = userDTO.ContactNumber,
        //                    Email = userDTO.EmailAddress,
        //                    FirstName = userDTO.FirstName,
        //                    Surname = userDTO.Surname,
        //                    Role = Common.UserTypeEnum.PartnerRestaurant.ToString()
        //                };
        //            }

        //            DataProvider dataProvider = new DataProvider();

        //            DAL.Address anAddress = dataProvider.FindAddressById(
        //                int.Parse(DataSecurityTripleDES.GetPlainText(userDTO.AddressId)));

        //            AddressViewModel addressViewModel = EntityDTOHelper.GetEntityDTO<DAL.Address, AddressViewModel>(anAddress);

        //            if (anAddress != null)
        //            {
        //                DAL.Region region = dataProvider.FindRegionById(anAddress.RegionId ?? 0);

        //                if (region != null)
        //                {
        //                    addressViewModel.RegionAlias = region.RegionAlias;
        //                    addressViewModel.RegionID = DataSecurityTripleDES.GetEncryptedText(region.Id);
        //                }

        //                if (((Common.UserTypeEnum)userDTO.UserTypeFlag).HasFlag(Common.UserTypeEnum.PartnerRestaurant))
        //                {
        //                    partnerRestaurantViewModel.Addresses = new List<AddressViewModel>() { addressViewModel };
        //                }
        //                else {
        //                    craveatsDinerViewModel.Addresses = new List<AddressViewModel>() { addressViewModel };
        //                }
        //            }

        //            if (((Common.UserTypeEnum)userDTO.UserTypeFlag).HasFlag(Common.UserTypeEnum.PartnerRestaurant))
        //                return View(partnerRestaurantViewModel);
        //            else
        //                return View(craveatsDinerViewModel);
        //        }

        //    }

        //    return View("Error");  
        //}

        //public ActionResult DetailFromSessionDTO()

    }
}