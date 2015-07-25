using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Files
    {
        public Files()
        {
            files = new List<ViewDataUploadFilesResult>();
        }

        public List<ViewDataUploadFilesResult> files { get; set; }
    }

    public class FileDeleteResponse
    {
        public string EncodeFilePath { get; set; }
        public bool Status { get; set; }
    }

    public static class CommonHelper
    {
        public static String GetTimestamp(DateTime value)
        {
            // return value.ToString("yyyyMMddHHmmssffff");
            return "QU" + value.Year + value.Month + value.Day + value.Hour + value.Minute + value.Second + value.Millisecond;
        }

    }


    public class ViewDataUploadFilesResult
    {
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string deleteUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteType { get; set; }
    }
}
