using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuleBuddiesASP.HelperClasses
{
    public class IvleUserInfo
    {
        private static string ivleUrl = "https://ivle.nus.edu.sg/api/Lapi.svc/";
        private static string apiKey = "APIKey=JinwNKRnSNSXfoTIhY1X0";
        private static string tokenUrl = "&Token=";
        
       
        public string getUserID()
        {
            string getOptionUrl = "UserID_Get?";
            string token = getMyToken();
            string url = ivleUrl + getOptionUrl + apiKey + tokenUrl + token;

            return userInfo(url, token).ToLower();
        }

        public string getUserName()
        {
            string getOptionUrl = "UserName_Get?";
            string token = getMyToken();
            string url = ivleUrl + getOptionUrl + apiKey + tokenUrl + token;

            return userInfo(url, token);
        }

        public string getUserEmail()
        {
            string getOptionUrl = "UserEmail_Get?";
            string token = getMyToken();
            string url = ivleUrl + getOptionUrl + apiKey + tokenUrl + token;

            return userInfo(url, token);
        }
        private string userInfo(string url, string token)
        {
            string userInfo = "";

            try
            {
                if (token.Length > 1)
                {
                    XmlResponse xml = new XmlResponse();
                    userInfo = xml.getXmlResponse(url);

                }
                userInfo = editText(userInfo);
            }
            catch { }
            return userInfo;
        }
            
        private string editText(string text)
        {
            int index = text.Length;
            text = text.Substring(1, index - 2);
            text = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
            return text;
        }
       
        private string getMyToken()
        {
            Token myToken = new Token();

            return myToken.getToken();
        }
    }
}