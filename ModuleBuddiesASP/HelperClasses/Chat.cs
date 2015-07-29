using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuleBuddiesASP.HelperClasses
{
    public class Chat
    {
        public String ChatID { get; set; }
        public String FriendID { get; set; }
        public String UserID { get; set; }

      
        public Chat(String uid, String fid)
        {
            UserID = uid;
            FriendID = fid;
            ChatID = CreateChatID(uid, fid);
        }

        private String CreateChatID(String uid, String fid)
        {
            String cid = String.Empty;

            if (String.Compare(uid, fid) < 0)
                cid = uid + fid;
            else
                cid = fid + uid;

            return cid;
        }
    }
}