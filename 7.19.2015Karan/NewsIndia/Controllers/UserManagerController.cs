using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessClasses;
using Common;
using NewsIndia.AuthFilters;
using NewsIndiaBAL;


namespace NewsIndia.Controllers
{
    [UserAuth]

    public class UserManagerController : Controller
    {
        /// <summary>
        /// Used to activate the account of the user
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult UserActivate(string uid)
        {
            try
            {
                var result = NewsIndiaBAL.UserManager.ActivateUser(uid);
                return View(result);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AdminAuth]
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// Used to remove user 
        /// </summary>
        /// <returns></returns>
        [AdminAuth]
        public ActionResult RemoveUser(int userId)
        {
            return Json(NewsIndiaBAL.UserManager.RemoveUser(userId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to show the user grid for displaying user information
        /// </summary>
        /// <returns></returns>
        [AdminAuth]
        public ActionResult ShowUserTable()
        {
            var users = NewsIndiaBAL.UserManager.GetUsersForMasterGrid();
            return View(users);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult AddEditUser(int userId = 0)
        {
            //var datataaa = EmailDesigner.GetAccountActivationEmail("asd","IDSISSSSSSS");

            var userModel = new UserViewModel();


            var countryList = CountryList.GetCountryList();

            if (countryList.Any())
            {
                if (countryList.Count > 0)
                {
                    ViewBag.DropDownForCountry =
                        countryList.Select(c => new SerializableSelectListItem
                        {
                            Text = c.CountryName,
                            Value = c.CountryId.ToString(CultureInfo.InvariantCulture)
                        });
                }
            }



            if (SessionManager.GetSessionInfo() != null && (SessionManager.IsAdminLoggedIn() || SessionManager.GetSessionInfo().Id == userId))
            {

                if (userId > 0)
                {
                    userModel = UserManager.GetUserById(userId);

                }

                if (userModel.Country != 0)
                {
                    ViewBag.StateInfo = StateList.GetStateByCountry(userModel.Country);
                }



                if (SessionManager.IsAdminLoggedIn())
                    ViewBag.Roles = NewsIndiaBAL.RoleManager.GetRoles();


            }
            else if (SessionManager.GetSessionInfo() != null)
                return RedirectToAction("Index", "Home");


            ViewBag.IsAddRequest = userModel.UserId == 0;

            return View(userModel);
            //  return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Used to save the information of user 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult AddEditUser(UserViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var userId = userInfo.UserId;
                var userIdSaved = NewsIndiaBAL.UserManager.SaveUserInformation(userInfo, false, SessionManager.IsAdminLoggedIn(), SessionManager.GetSessionInfo() != null ? SessionManager.GetSessionInfo().Id : 0);

                if (userId == 0 && (userIdSaved > 0))
                {
                    string uniqueGuid = System.Guid.NewGuid().ToString();
                    NewsIndiaBAL.AuthenticationLinkManager.CreateAuthenticationLink(uniqueGuid, 1, userIdSaved);
                    var emailTemplate = EmailDesigner.GetAccountActivationEmail(userInfo.FirstName, uniqueGuid);
                    Common.EmailHelper.SendEmail("News India Admin", new List<string>() { userInfo.EmailId }, "Account Activation", emailTemplate);
                }

                if (SessionManager.IsAdminLoggedIn())
                    return RedirectToAction("Index", "UserManager");
                else if (SessionManager.GetSessionInfo() != null)
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("AccountCreated", "UserManager");
            }
            return View(userInfo);
        }

        public ActionResult AccountCreated()
        {
            return View();
        }

        /// <summary>
        /// Used to check if email address exist in database or not
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public JsonResult IsEmailUnique(string emailId)
        {
            return Json(!NewsIndiaBAL.UserManager.IsEmailExist(emailId), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used to get list of states based on countryid
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetStatesForCountry(int countryId)
        {
            try
            {
                return Json(StateList.GetStateByCountry(countryId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new StateInformation(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}