using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace ModuleBuddiesASP.HelperClasses
{
    public class Cookies
    {
        public HttpCookie getTokenCookie()
        {
            return tokenCookie();
        }

      

        public HttpCookie getUserInfoCookie()
        {
            return userInfoCookie();
        }

        public HttpCookie getUserIdCookie()
        {
            return userIdCookie();
        }

        public HttpCookie getIsAuthCookie()
        {
            return isAuthCookie();
        }

        public HttpCookie getIsLoginCookie()
        {
            return isLoginCookie();
        }
        private HttpCookie tokenCookie()
        {
            string url = HttpContext.Current.Request.RawUrl;
            int index = url.IndexOf('=');
            HttpCookie tokenCookie = new HttpCookie("tokenCookie");

            if (index > -1)
            {           
                tokenCookie.Value = url.Substring(index + 1);
                tokenCookie.Expires = DateTime.Now.AddDays(1);
                //tokenCookie.Expires = DateTime.Now.AddSeconds(2);          
            }

            return tokenCookie;   
        }

        private HttpCookie userInfoCookie()
        {
            HttpCookie userCookie = new HttpCookie("userCookie");
            IvleUserInfo userInfo = new IvleUserInfo();

            userCookie.Values["username"] = userInfo.getUserName();
            userCookie.Values["userID"] = userInfo.getUserID();
            userCookie.Values["userEmail"] = userInfo.getUserEmail();
            userCookie.Expires = DateTime.Now.AddDays(1);
            //userCookie.Expires = DateTime.Now.AddSeconds(2);

            return userCookie;
        }

        private HttpCookie userIdCookie()
        {
            HttpCookie userIdCookie = new HttpCookie("userIdCookie");
            IvleUserInfo userInfo = new IvleUserInfo();

            userIdCookie.Value = userInfo.getUserID();
            userIdCookie.Expires = DateTime.Now.AddDays(1);
            //userCookie.Expires = DateTime.Now.AddSeconds(2);

            return userIdCookie;
        }

        private HttpCookie isAuthCookie()
        {
            HttpCookie isAuthCookie = new HttpCookie("isAuthCookie");
            Token ivleToken = new Token();

            string token = ivleToken.getToken();

            if(token != "")
            {
                isAuthCookie.Value = "true";
                
            }
            else
            {
                isAuthCookie.Value = "false";
            }

            isAuthCookie.Expires = DateTime.Now.AddDays(1);
            //userCookie.Expires = DateTime.Now.AddSeconds(2);

            return isAuthCookie;
        }

        private HttpCookie isLoginCookie()
        {
            HttpCookie isLoginCookie = new HttpCookie("isLoginCookie");
            Token ivleToken = new Token();

            string token = ivleToken.getToken();

            if (token != "")
            {
                isLoginCookie.Value = "true";

            }
            else
            {
                isLoginCookie.Value = "false";
            }

            isLoginCookie.Expires = DateTime.Now.AddDays(1);
            //userCookie.Expires = DateTime.Now.AddSeconds(2);

            return isLoginCookie;
        }
        
        
    }
}