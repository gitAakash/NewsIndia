using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Common
{
    public static class NewsIndiaDirectoryManager
    {
        public static void UploadMultipleFiles(string directoryPath, IList<HttpPostedFileBase> fileList)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            if (fileList[0] != null)
            {
                foreach (HttpPostedFileBase fileBeingUploaded in fileList)
                {
                    HttpPostedFileBase httpPostedFileBase = fileBeingUploaded;
                    string combine = Path.Combine(directoryPath, fileBeingUploaded.FileName);
                    httpPostedFileBase.SaveAs(combine);
                }
            }
        }

        public static void UploadSingleFile(string directoryPath, HttpPostedFileBase file)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            if (file != null)
            {
                HttpPostedFileBase httpPostedFileBase = file;
                string combine = Path.Combine(directoryPath, file.FileName);
                httpPostedFileBase.SaveAs(combine);
            }
        }

        public static void UploadFileByGivenName(string directoryPath, HttpPostedFileBase file, string fileName)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            if (file != null)
            {
                HttpPostedFileBase httpPostedFileBase = file;
                string combine = Path.Combine(directoryPath, fileName);
                httpPostedFileBase.SaveAs(combine);
            }
        }

        public static IEnumerable<string> RetriveAllFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return null;
            }

            string[] filePaths = Directory.GetFiles(directoryPath);
            return filePaths;
        }

        public static string RetriveSingleFile(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return null;
            }

            string[] filePaths = Directory.GetFiles(directoryPath);
            if (filePaths.Length > 0)
            {
                return filePaths[0];
            }
            return null;
        }

        public static void DeleteFile(string directoryPath)
        {
            if (directoryPath != null)
            {
                if (File.Exists(directoryPath))
                {
                    File.Delete(directoryPath);
                }
            }
        }

        public static void DeleteDirectoryAndContents(string directoryPath)
        {
            if (directoryPath != null)
            {
                var directoryInfo = new DirectoryInfo(directoryPath);

                if (directoryInfo.Exists)
                {
                    directoryInfo.Delete(true);
                }
            }
        }

        public static void DeleteDirectoryContentsLeavingDirectoryIntact(string directoryPath)
        {
            if (directoryPath != null)
            {
                if (Directory.Exists(directoryPath))
                {
                    string[] files = Directory.GetFiles(directoryPath);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
            }
        }

        public static void MoveFile(string sourceDirectoryPath, string destinationDirectoryPath, string fileName)
        {
            string sourceFilePath = sourceDirectoryPath + "\\" + fileName;
            string destinationFilePath = destinationDirectoryPath + "\\" + fileName;
            if (sourceDirectoryPath != null && destinationDirectoryPath != null)
            {
                if (Directory.Exists(sourceDirectoryPath) && Directory.Exists(destinationDirectoryPath))
                {
                    File.Copy(sourceFilePath, destinationFilePath);
                    File.Delete(sourceFilePath);
                }
            }
        }

        public static void MoveDirectory(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            if (sourceDirectoryPath != null && destinationDirectoryPath != null)
            {
                try
                {
                    var directoryInfo = new DirectoryInfo(sourceDirectoryPath);
                    directoryInfo.MoveTo(destinationDirectoryPath);
                }
                catch (Exception ex)
                {
                    Directory.Move(sourceDirectoryPath, destinationDirectoryPath);
                }
            }
        }

        public static bool CreateDirectory(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
                return true;
            }

            return false;
        }
    }
}