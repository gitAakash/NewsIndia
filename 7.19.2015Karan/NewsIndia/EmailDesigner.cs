using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using Common;
using NewsIndia.Properties;

namespace NewsIndia
{
    public static class EmailDesigner
    {
        /// <summary>
        /// Creation of the email for the activation of the account
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="unqiueId"></param>
        /// <returns></returns>
        public static string GetAccountActivationEmail(string userName, string unqiueId)
        {
            try
            {
                var circleBase64 = Common.ImageHelper.ImageToBase64(Resources.PROMO_GREEN_Circle, ImageFormat.Jpeg);
                var lineBase64 = Common.ImageHelper.ImageToBase64(Resources.PROMO_GREEN_Line, ImageFormat.Jpeg);
                var domainName = "http://localhost:52313/";

                return
                    "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
                    "<title>Account Activation</title></head><body bgcolor='#8d8e90'><table width='100%' border='0' " +
                    "cellspacing='0' cellpadding='0' bgcolor='#8d8e90'><tbody><tr><td><table width='600' border='0' " +
                    "cellspacing='0' cellpadding='0' bgcolor='#FFFFFF' align='center'><tbody><tr><td><table width='100%' " +
                    "border='0' cellspacing='0' cellpadding='0'><tbody><tr><td width='61'><a href='http://yourlink' target='_blank'>" +
                    //"<img src='images/PROMO-GREEN2_01_01.jpg' width='61' height='76' border='0' alt=''>" +
                      "<img alt='My Image' src='data:image/jpeg;base64," + circleBase64 + "' />" +
                    "</a></td><td width='144'><a href='#' " +
                    "target='_blank' style='text-decoration: none;color: black;font-size: 30px;'>News India</a></td><td width='393'>" +
                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tbody><tr><td height='46' align='right' valign='middle'>" +
                    "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tbody><tr><td width='67%' align='right'" +
                    "><font style='font-family:' myriad='' pro',='' helvetica,='' arial,='' sans-serif;='' color:#68696a;='' font-size:8px;='' text-transform:uppercase'=''>" +
                    "<a href='http://yourlink' style='color:#68696a; text-decoration:none'></a></font></td><td width='29%' align='right'>" +
                    "<font style='font-family:' myriad='' pro',='' helvetica,='' arial,='' sans-serif;='' color:#68696a;='' font-size:8px'=''>" +
                    "<a href='http://yourlink' style='color:#68696a; text-decoration:none; text-transform:uppercase'></a></font>" +
                    "</td><td width='4%'>&nbsp;</td></tr></tbody></table></td></tr><tr><td height='30'>" +
                    //"<img src='images/PROMO-GREEN2_01_04.jpg' width='393' height='30' border='0' alt=''>" +
                    "<img alt='My Image' src='data:image/jpeg;base64," + lineBase64 + "' />" +
                    "</td></tr></tbody></table></td></tr></tbody></table></td></tr><tr><td align='center'>" +
                    "&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                    "<tbody><tr><td width='10%'>&nbsp;</td><td width='80%' align='left' valign='top'><font style='font-family: Georgia, " +
                    "' times='' new='' roman',='' times,='' serif;='' color:#010101;='' font-size:24px'=''><strong><em>" +
                    "Hi " + userName + ",</em>" +
                    "</strong></font><br><br><font style='font-family: Verdana, Geneva, sans-serif; color:#666766; font-size:13px; line-height:21px'>" +
                    "Thank you for registering with News India<br> " +
                    "Please <a href='" + domainName + "/UserManager/UserActivate?uid=" + unqiueId + "'>" +
                    "Click Here</a> to Active your account with news India<br>If the link above doesnt work please copy paste the above link<br>" +
                     domainName + "/UserManager/UserActivate?uid=" + unqiueId + "<br>Please Ignore if its not registered and got this mail." +
                    "<br>On behalf of the Company<br>News India</font></td><td width='10%'>" +
                    "&nbsp;</td></tr><tr><td>&nbsp;</td><td align='right' valign='top'><table width='108' border='0' cellspacing='0' cellpadding='0'>" +
                    "<tbody><tr><td></td></tr><tr><td align='center' valign='middle' bgcolor='#03bcda'></td></tr><tr></tr><tr></tr></tbody></table>" +
                    "</td><td>&nbsp;</td></tr></tbody></table></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr><tr><td></td></tr><tr><td>&nbsp" +
                    ";</td></tr><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'></table></td></tr><tr><td>&nbsp;</td></tr><tr>" +
                    "<td align='center'><font style='font-family:' myriad='' pro',='' helvetica,='' arial,='' sans-serif;='' color:#231f20;='' font-size:8px'=''>" +
                    "</font></td></tr><tr><td>&nbsp;</td></tr></tbody></table></td></tr></tbody></table></body></html>";


            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}