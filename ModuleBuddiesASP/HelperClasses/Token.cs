using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuleBuddiesASP.HelperClasses
{
    public class Token
    {
        public string getToken()
        {
            return ivleToken();
        }
        private string ivleToken()
        {
            string token = "";
            HttpCookie tokenCookie = new HttpCookie("tokenCookie");
            Cookies myCookie = new Cookies();

            tokenCookie = myCookie.getTokenCookie();

            if (HttpContext.Current.Request.Cookies["tokenCookie"] != null)
            {
                token = HttpContext.Current.Server.HtmlEncode(HttpContext.Current.Request.Cookies["tokenCookie"].Value);
                return token;
            }
            else
            {
                return token;
            }

            
        }
    }
}