using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BusinessClasses;
using Common;
using NewsIndia.AuthFilters;

namespace NewsIndia.Controllers
{
    [UserAuth]
    [AdminAuth]
    public class SubCategoryDataManagerController : Controller
    {

        /// <summary>
        /// Used to Display the subcategory Data Master
        /// </summary>
        /// <returns></returns>
        public ActionResult SubCategoryDataMaster()
        {
            ViewBag.ActiveCategoryInfo = NewsIndiaBAL.SubCategoryManager.GetActiveCategories();
            return View();
        }

        /// <summary>
        /// Used to get the list of all SubCategories present for the category
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSubCategories(int categoryId)
        {
            try
            {
                return Json(NewsIndiaBAL.SubCategoryDataManager.GetSubCategories(categoryId),
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new List<SubCategoryInformation>(), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Used to display the Subcategory Data and has feature to modify or add the subcategory data
        /// </summary>
        /// <param name="subCategoryDataId"></param>
        /// <returns></returns>
        public ActionResult DisplaySubCategoryData(int subCategoryDataId = 0)
        {
            try
            {
                ViewBag.IsEdit = subCategoryDataId != 0;
                ViewBag.ActiveCategoryInfo = NewsIndiaBAL.SubCategoryManager.GetActiveCategories();
                SubCategoryDataInfoModel subCategoryDataInfo = subCategoryDataId != 0 ?
                    NewsIndiaBAL.SubCategoryDataManager.GetSubCategoryDataInformation(subCategoryDataId) : new SubCategoryDataInfoModel();

                return View(subCategoryDataInfo);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        /// <summary>
        /// Used to Add / Edit data in the database
        /// </summary>
        /// <param name="subCategoryDataInfoModel"></param>
        /// <param name="subCategoryDataId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddEditSubCategoryData(string description, bool isVisible,
            int SubCategoryId, string timestamp, string UploadedFileNames, string title, string subCategoryDataId)
        {
            try
            {
                var subCategoryDataInfoModel = new SubCategoryDataInfoModel()
                {
                    Description = description,
                    IsVisible = Convert.ToBoolean(isVisible),
                    SubCategoryDataId = Convert.ToInt32(subCategoryDataId),
                    SubCategoryId = Convert.ToInt32(SubCategoryId),
                    TimeStamp = timestamp,
                    UploadedFileNames = UploadedFileNames,
                    Title = title
                };
                //if (subCategoryDataInfoModel.Title == null)
                //    return false;

                var currentDate = Convert.ToString(DateTime.Now);

                string timeStamp = Convert.ToInt32(subCategoryDataId) == 0
                    ? CommonHelper.GetTimestamp(Convert.ToDateTime(currentDate))
                    : timestamp;
                //Assigning Time Stamp
                subCategoryDataInfoModel.TimeStamp = timeStamp;

                if (subCategoryDataInfoModel.UploadedFileNames != null)
                {


                    subCategoryDataInfoModel.UploadedFileNames = subCategoryDataInfoModel.UploadedFileNames + ",";
                    var values = subCategoryDataInfoModel.UploadedFileNames.Split(',');
                    subCategoryDataInfoModel.UploadedFileNames = string.Empty;
                    foreach (var value in values)
                    {
                        if (value != "")
                            subCategoryDataInfoModel.UploadedFileNames += timeStamp + "\\" + value + ",";
                    }

                }


                if (subCategoryDataInfoModel.UploadedFileNames != null &&
                    subCategoryDataInfoModel.UploadedFileNames != "")
                {
                    subCategoryDataInfoModel.UploadedFileNames = subCategoryDataInfoModel.UploadedFileNames.Substring(0,
                        subCategoryDataInfoModel.UploadedFileNames.Length - 1);

                    //questionMaster.UploadedFileNames = questionMaster.UploadedFileNames.Split('/')[1];
                    //questionMaster.UploadedFileNames = questionMaster.UploadedFileNames + ",";
                    string questionUploadDirectoryPath =
                        Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AttachmentFilePath"]), timeStamp);
                    NewsIndiaDirectoryManager.CreateDirectory(questionUploadDirectoryPath);

                    //Get to temp directiory Location
                    string tempDirectoryPath =
                        Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AttachmentFilePath"]),
                            "TemporaryFiles");
                    NewsIndiaDirectoryManager.CreateDirectory(tempDirectoryPath);

                    string currentUserTempDirectoryPath =
                        Path.Combine(tempDirectoryPath, SessionManager.GetSessionInfo().Id.ToString());


                    string[] files = subCategoryDataInfoModel.UploadedFileNames.Split(',');
                    foreach (string questionFiles in files)
                    {
                        string[] fileEntries = Directory.GetFiles(currentUserTempDirectoryPath);

                        foreach (string fileEntry in fileEntries)
                        {
                            string fileName = timeStamp + "\\" + Path.GetFileName(fileEntry);
                            if (fileName == questionFiles)
                            {
                                string sourceFilePath = Path.Combine(currentUserTempDirectoryPath,
                                    fileName.Split('\\')[1]);

                                if (System.IO.File.Exists(sourceFilePath))
                                {
                                    string fileNameToSave = fileName;
                                    string filePath = Path.Combine(questionUploadDirectoryPath,
                                        fileNameToSave.Split('\\')[1]);
                                    // To copy a file to another location and 
                                    // overwrite the destination file if it already exists.
                                    System.IO.File.Copy(sourceFilePath, filePath, true);

                                    System.IO.File.Delete(sourceFilePath);
                                }
                            }
                        }
                    }
                }

                var result = NewsIndiaBAL.SubCategoryDataManager.AddEditSubCategoryData(subCategoryDataInfoModel,
                    Convert.ToInt32(subCategoryDataId));
                if (result != null)
                {
                    var notificationCategoryName = "";

                    notificationCategoryName = "Email abc";
                    //var userDetails = new UserRepository();
                    //var user = userDetails.Find(userId);

                    var emailAddress = ConfigurationManager.AppSettings["AdminEmail"];

                    #region Send Email Notification to User

                    const string mailSubject = "New News Posted";
                    string mainUrl = ConfigurationManager.AppSettings["MainURL"];
                    var sbMailBody = new StringBuilder();
                    sbMailBody.Append("Hi " + "Akshay" + " " + "Bibekar" + ",");
                    sbMailBody.Append(" <br />Notification !!!");
                    sbMailBody.Append("<br /><br /> Please approve news to show it on website ");
                    sbMailBody.Append("<br /><br /> ");
                    sbMailBody.Append("<strong> Thanks & Regards, <br /> NewsIndia Team </strong>");
                    sbMailBody.Append("<br /><br /> ");

                    #endregion

                    // status = EmailHelper.EmailComposeActionsAndSendEmail(emailAddress, sbMailBody.ToString(), mailSubject, null);
                    SendMail(emailAddress, mailSubject, sbMailBody.ToString(), null);
                    SmsHelper.SmsSend("9766438071,8983563905", "Hi,\nNotification !!!\n Please approve news to show it on website\n\nThanks & Regards,\nNewsIndia Team");

                    return Json(result, JsonRequestBehavior.AllowGet);
                    /*return Json(NewsIndiaBAL.SubCategoryDataManager.AddEditSubCategoryData(subCategoryDataInfoModel,
                                        Convert.ToInt32(subCategoryDataId)), JsonRequestBehavior.AllowGet);*/


                }
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
                ;
            }
            // return null;
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        /// used to remove Sub category Data
        /// 
        /// <param name="subCategoryDataID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveSubCategoryData(int subCategoryDataID)
        {
            return Json(new { IsSubCategoryRemoved = NewsIndiaBAL.SubCategoryDataManager.RemoveSubCategoryData(subCategoryDataID) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string DeleteChapterFile(string path)
        {
            var model = new FileDeleteResponse();
            if (System.IO.File.Exists(path))
            {
                // model.EncodeFilePath = EncodeFile(path);
                try
                {
                    System.IO.File.Delete(path);
                    model.Status = true;
                }
                catch (Exception e)
                {
                    //RedirectToAction("ErrorOnPage", "Error");
                }
            }

            var serializer = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue, RecursionLimit = 100 };
            string jsonResult = serializer.Serialize(model);

            return jsonResult;
        }

        [HttpPost]
        public string SaveUpload(dynamic file)
        {
            int d = Convert.ToInt32(HttpContext.Request.Files.Count);
            if (HttpContext.Request.Files.Count > 0)
            {
                if (HttpContext.Request.Files.AllKeys.Any())
                {
                    HttpPostedFileBase uploadedFile =
                        HttpContext.Request.Files[0];
                    if (uploadedFile != null)
                    {
                        long currentUserId = SessionManager.GetSessionInfo().Id;

                        string tempDirectoryPath =
                            Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AttachmentFilePath"]),
                                "TemporaryFiles");

                        NewsIndiaDirectoryManager.CreateDirectory(tempDirectoryPath);

                        string currentUserTempDirectoryPath =
                            Path.Combine(tempDirectoryPath, currentUserId.ToString(CultureInfo.InvariantCulture));

                        NewsIndiaDirectoryManager.CreateDirectory(currentUserTempDirectoryPath);

                        string filePath = Path.Combine(currentUserTempDirectoryPath,
                            uploadedFile.FileName);

                        uploadedFile.SaveAs(filePath);
                        string fullName = Path.Combine(currentUserTempDirectoryPath, uploadedFile.FileName);
                        var statuses = new Files();

                        string deletePath = Url.Action("DeleteChapterFile", "SubCategoryDataManager");


                        string fileExtension = Path.GetExtension(uploadedFile.FileName);


                        if (Request.Url != null)
                        {
                            string requiredPath;

                            switch (fileExtension)
                            {
                                case ".jpeg":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                case ".png":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                case ".gif":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                case ".bmp":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                case ".jpg":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                case ".pdf":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/pdfImage.png");
                                    break;
                                case ".xls":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/xlsxImage.png");
                                    break;
                                case ".xlsx":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/xlsxImage.png");
                                    break;
                                case ".cs":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/csImage.png");
                                    break;
                                case ".cshtml":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/csImage.png");
                                    break;
                                case ".js":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/jsImage.png");
                                    break;
                                case ".css":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/csImage.png");
                                    break;
                                case ".ppt":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/pptImage.png");
                                    break;
                                case ".pptx":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/pptImage.png");
                                    break;
                                case ".doc":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/docImage.png");
                                    break;
                                case ".docx":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/docImage.png");
                                    break;
                                case ".txt":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/txtImage.png");
                                    break;
                                case ".mp4":
                                    requiredPath = Request.Url.GetLeftPart(UriPartial.Authority) +
                                                   Url.Content("~/Content/Images/VideoDoc.png");
                                    break;
                                case ".avi":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                case ".wmv":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                case ".mov":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                case ".mpeg":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                case ".3gp":
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                                default:
                                    requiredPath = @"data:image/png;base64," + ImageHelper.EncodeFile(fullName);
                                    break;
                            }
                            string folderLocation =
                                Server.MapPath(ConfigurationManager.AppSettings["AttachmentFilePath"]);

                            string url = Request.Url.GetLeftPart(UriPartial.Authority) +
                                         Url.Content(folderLocation
                                                     + "TemporaryFiles");
                            string pathToCurrentFolder = Path.Combine(url,
                                currentUserId.ToString(CultureInfo.InvariantCulture));
                            string finalUrlOfFile = Path.Combine(pathToCurrentFolder, uploadedFile.FileName);
                            statuses.files.Add(new ViewDataUploadFilesResult
                            {
                                name = uploadedFile.FileName,
                                size = uploadedFile.ContentLength,
                                type = uploadedFile.ContentType,
                                url = finalUrlOfFile,
                                deleteUrl = deletePath + "?" + "path=" + filePath,
                                thumbnailUrl = requiredPath,
                                deleteType = "GET",
                            });
                        }
                        var serializer = new JavaScriptSerializer
                        {
                            MaxJsonLength = Int32.MaxValue,
                            RecursionLimit = 100
                        };
                        string jsonResult = serializer.Serialize(statuses);

                        return jsonResult;
                        // return null;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Used to display the table of the SubCategory
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowSubCategoryDataTable()
        {
            var subCategories = NewsIndiaBAL.SubCategoryDataManager.GetSubCategoryDataList();
            return View(subCategories);
        }

        public static void SendMail(string toEmailAddress, string subject, string MessageBody, Attachment attachmentFile = null)
        {
            try
            {
                /*SmtpClient client = new SmtpClient();
                var message = new MailMessage();
                message.To.Add(toEmailAddress);
                message.Subject = subject;
                message.IsBodyHtml = true;

                //  AlternateView messageBody = AlternateView.CreateAlternateViewFromString(MessageBody, null, "text/html");
                message.Body = MessageBody;
                // Set the method that is called back when the send operation ends.
                //client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                // The userState can be any object that allows your callback  
                // method to identify this send operation. 
                // For this example, the userToken is a int constant. 
                //int userState = NotificationLogId;
                client.SendAsync(message, "message");*/
                Common.EmailHelper.SendEmail("Donotreply.ShopingCart@gmail.com", new List<string>() { toEmailAddress }, subject, MessageBody);

            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }

        }

    }

}