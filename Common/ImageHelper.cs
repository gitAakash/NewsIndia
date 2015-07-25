using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Common
{
    public static class ImageHelper
    {
        public static string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(File.ReadAllBytes(fileName));
        }
        public static void CropAndSaveImage(int width, int height, int x, int y, Bitmap sourceFile, string saveFilePath)
        {
            // variable for percentage resize 
            float percentageResize = 0;
            float percentageResizeW = 0;
            float percentageResizeH = 0;

            // variables for the dimension of source and cropped image 
            int sourceX = x;
            int sourceY = y;
            int destX = 0;
            int destY = 0;

            // Create a bitmap object file from source file 
            //  Bitmap sourceImage = new Bitmap(sourceFilePath);
            Bitmap sourceImage = sourceFile;

            // Set the source dimension to the variables 
            int sourceWidth = sourceImage.Width;
            int sourceHeight = sourceImage.Height;

            // Calculate the percentage resize 
            percentageResizeW = ((float)width / (float)sourceWidth);
            percentageResizeH = ((float)height / (float)sourceHeight);

            // Checking the resize percentage 
            if (percentageResizeH < percentageResizeW)
            {
                percentageResize = percentageResizeW;
                destY = Convert.ToInt16((height - (sourceHeight * percentageResize)) / 2);
            }
            else
            {
                percentageResize = percentageResizeH;
                destX = Convert.ToInt16((width - (sourceWidth * percentageResize)) / 2);
            }

            // Set the new cropped percentage image
            int destWidth = (int)Math.Round(sourceWidth * percentageResize);
            int destHeight = (int)Math.Round(sourceHeight * percentageResize);

            // Create the image object 
            using (Bitmap objBitmap = new Bitmap(width, height))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    // Set the graphic format for better result cropping 
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, new Rectangle(destX, destY, 130, 130), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

                    // Save the file path, note we use png format to support png file 
                    objBitmap.Save(saveFilePath, ImageFormat.Png);
                }
            }
        }


        public static Bitmap CropProfileImage(Bitmap imageBitmap, int x, int y, int width, int heigth)
        {
            Image oimg = imageBitmap;
            var cropcords = new Rectangle(
                Convert.ToInt32(x),
                Convert.ToInt32(y),
                Convert.ToInt32(width),
                Convert.ToInt32(heigth));

            var bitMap = new Bitmap(cropcords.Width, cropcords.Height, oimg.PixelFormat);
            Graphics grph = Graphics.FromImage(bitMap);
            grph.DrawImage(oimg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), cropcords, GraphicsUnit.Pixel);

            return bitMap;
        }

        public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}