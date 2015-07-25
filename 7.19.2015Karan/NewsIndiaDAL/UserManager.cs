using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;
using EntityProject;

namespace NewsIndiaDAL
{
    public class UserManager
    {
        /// <summary>
        /// Used to get the active categories
        /// </summary>
        /// <returns></returns>
        public static List<UserMaster> GetActiveUsers()
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return nie.UserMasters.Where(m => m.IsActive).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<UserMaster>();
            }
        }

        /// <summary>
        /// Used to get user by id
        /// </summary>
        /// <returns></returns>
        public static UserViewModel GetUserById(int userId = 0)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    var um = nie.UserMasters.FirstOrDefault(m => m.Id == userId && m.IsActive);
                    return new UserViewModel()
                    {
                        UserId = um.Id,
                        FirstName = um.FirstName,
                        LastName = um.LastName,
                        MiddleName = um.MiddleName,
                        Gender = um.Gender,
                        EmailId = um.Email,
                        //Password = um.Password,
                        MobileNumber = um.PhoneNumber,
                        Address = um.Address,
                        City = um.City,
                        // StateName = um.State,
                        //CountryName = um.Country,
                        State = um.StateID,
                        Country = um.CountryID,
                        PinCode = um.Pincode,
                        IsActive = um.IsActive,
                        CreatedBy = um.CreatedBy,
                        CreatedOn = um.CreatedOn,
                        ModifiedBy = um.ModifiedBy,
                        ModifiedOn = um.ModifiedOn,
                        DateOfBirth = Convert.ToDateTime(um.DateOfBirth),
                        IsEnabled = Convert.ToBoolean(um.IsEnabled),
                        SelectedRoleId = um.RoleId
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserViewModel();
            }
        }

        /// <summary>
        /// Used to get users information for grid 
        /// </summary>
        /// <returns></returns>
        public static List<UserModelGrid> GetUsersForMasterGrid()
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    var adminRoleId = nie.UserRoles.FirstOrDefault(m => m.Name == "admin") ?? new UserRole() { Id = 0 };

                    return nie.UserMasters.Where(m => m.IsActive).Select(m => new UserModelGrid()
                    {
                        EmailId = m.Email,
                        FirstName = m.FirstName,
                        LastName = m.LastName,
                        IsAdmin = m.RoleId == adminRoleId.Id,
                        MiddleName = m.MiddleName,
                        PhoneNumber = m.PhoneNumber,
                        RoleId = m.RoleId,
                        UserId = m.Id

                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<UserModelGrid>();
            }
        }

        /// <summary>
        /// Used to check if the email address exist in database
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public static bool IsEmailExist(string emailId)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    return nie.UserMasters.Any(m => m.Email == emailId & m.IsActive);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Used to save the User Information
        /// </summary>
        /// <returns></returns>
        public static int SaveUserInformation(UserViewModel um, bool requestForPasswordChange, bool isAdminSave,
            int? loggedInUser = null)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    if (um.UserId != 0)
                    {
                        var userInfo = nie.UserMasters.FirstOrDefault(m => m.Id == um.UserId & m.IsActive);
                        if (userInfo != null)
                        {

                            userInfo.FirstName = um.FirstName;
                            userInfo.LastName = um.LastName;
                            userInfo.MiddleName = um.MiddleName;
                            userInfo.Gender = um.Gender;
                           // userInfo.Email = um.EmailId;
                            userInfo.PhoneNumber = um.MobileNumber;
                            userInfo.DateOfBirth = um.DateOfBirth;

                            if (requestForPasswordChange)
                                userInfo.Password = um.Password;

                            userInfo.Address = um.Address;
                            userInfo.City = um.City;
                            userInfo.StateID = um.State;
                            userInfo.CountryID = um.Country;
                            userInfo.Pincode = um.PinCode;
                            userInfo.ModifiedBy = loggedInUser;
                            userInfo.ModifiedOn = DateTime.Now;
                            if (isAdminSave)
                            {
                                userInfo.RoleId = um.SelectedRoleId;
                                userInfo.IsEnabled = um.IsEnabled;
                            }
                            nie.SaveChanges();
                            return userInfo.Id;
                        }
                        return 0;

                    }
                    else
                    {
                        var userData = new UserMaster()
                         {

                             FirstName = um.FirstName,
                             LastName = um.LastName,
                             MiddleName = um.MiddleName,
                             Gender = um.Gender,
                             Email = um.EmailId,
                             Password = um.Password,
                             PhoneNumber = um.MobileNumber,
                             Address = um.Address,
                             City = um.City,
                             StateID = um.State,
                             CountryID = um.Country,
                             Pincode = um.PinCode,
                             IsActive = true,
                             CreatedBy = loggedInUser,
                             CreatedOn = DateTime.Now,
                             ModifiedBy = loggedInUser,
                             ModifiedOn = DateTime.Now,
                             DateOfBirth = um.DateOfBirth



                         };

                        if (isAdminSave)
                        {
                            userData.RoleId = um.SelectedRoleId;
                            userData.IsEnabled = um.IsEnabled;
                        }
                        else
                        {
                            userData.RoleId = 3;
                            userData.IsEnabled = false;
                        }
                        nie.UserMasters.Add(userData);
                        nie.SaveChanges();
                        return userData.Id;
                    }


                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Used to activate the user account
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static UserActivationModel ActivateUser(string uid)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    var guidInfo = nie.AuthenticationLinks.FirstOrDefault(m => m.UnqiueLink == uid && !m.IsUsed);
                    if (guidInfo != null && guidInfo.VerificationTypeId == 1)
                    {
                        var userInfo = nie.UserMasters.FirstOrDefault(m => m.Id == guidInfo.UserId);
                        if (userInfo != null)
                        {
                            userInfo.IsEnabled = true;
                            guidInfo.IsUsed = true;
                            nie.SaveChanges();
                            return new UserActivationModel()
                            {
                                UserId = userInfo.Id,
                                UserName = userInfo.FirstName + (userInfo.MiddleName == null ? ("") : " " + userInfo.MiddleName) + " " + userInfo.LastName
                            };
                        }

                    }
                    return new UserActivationModel();
                }
            }
            catch (Exception ex)
            {
                return new UserActivationModel();
            }
        }
        /// <summary>
        /// Used to Remove user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool RemoveUser(int userId)
        {
            try
            {
                using (var nie = new NewsIndiaTVEntities())
                {
                    var user = nie.UserMasters.FirstOrDefault(m => m.Id == userId);
                    if (user != null)
                        user.IsActive = false;
                    nie.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                ;
            }
        }


    }
}

