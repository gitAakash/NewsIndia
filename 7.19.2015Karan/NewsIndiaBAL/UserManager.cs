using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessClasses;

namespace NewsIndiaBAL
{
    public class UserManager
    {
        /// <summary>
        /// Used to get the active categories
        /// </summary>
        /// <returns></returns>
        public static UserViewModel GetUserById(int userId = 0)
        {
            try
            {
                return NewsIndiaDAL.UserManager.GetUserById(userId);
            }
            catch (Exception ex)
            {
                return new UserViewModel();
            }
        }

        /// <summary>
        /// Used to save the information of the user
        /// </summary>
        /// <returns></returns>
        public static int SaveUserInformation(UserViewModel userInfo, bool requestForPasswordChange, bool isAdminSave, int? loggedInUserId)
        {
            try
            {
                if (loggedInUserId == 0)
                    loggedInUserId = null;

                var userId = userInfo.UserId;
                if (userId == 0)
                    userInfo.Password = new Common.EncryptionHelper().Encrypt(userInfo.Password);

                var userIdSaved = NewsIndiaDAL.UserManager.SaveUserInformation(userInfo, requestForPasswordChange, isAdminSave, loggedInUserId);

                return userIdSaved;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Used to remove user from News India
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool RemoveUser(int userId)
        {
            try
            {
                return NewsIndiaDAL.UserManager.RemoveUser(userId);
            }
            catch (Exception ex)
            {
                return false;
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
                return NewsIndiaDAL.UserManager.ActivateUser(uid);

            }
            catch (Exception ex)
            {
                return new UserActivationModel();
            }
        }


        /// <summary>
        /// Used to get the User for user master grid
        /// </summary>
        /// <returns></returns>
        public static List<UserModelGrid> GetUsersForMasterGrid()
        {
            try
            {
                return NewsIndiaDAL.UserManager.GetUsersForMasterGrid();
            }
            catch (Exception ex)
            {
                return new List<UserModelGrid>();
            }
        }
        /// <summary>
        /// Used to check if email address exist or not
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public static bool IsEmailExist(string emailId)
        {
            try
            {
                return NewsIndiaDAL.UserManager.IsEmailExist(emailId);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
