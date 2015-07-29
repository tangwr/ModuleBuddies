using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;


namespace ModuleBuddiesASP.HelperClasses
{
    public class ModuleInfo
    {
        private static string ivleUrl = "https://ivle.nus.edu.sg/api/Lapi.svc/";
        private static string apiKey = "APIKey=JinwNKRnSNSXfoTIhY1X0";
        Token myToken = new Token();
        XmlResponse myXmlResponse = new XmlResponse();
        public List<String> getModInfo(string keyWord)
        {
            
            //string token = (string)(Session["sessionToken"]);
            string token = myToken.getToken();
            string authTokenUrl = "&AuthToken=";
            string getOptionUrl = "Modules?";
            string otherOptionUrl = "&Duration=10&IncludeAllInfo=false";
            string url = ivleUrl + getOptionUrl + apiKey + authTokenUrl + token + otherOptionUrl;

            string strXmlResponse = myXmlResponse.getXmlResponse(url);
            int count = Regex.Matches(strXmlResponse, keyWord).Count;

            List<String> listMod = new List<String>();
            listMod = getModulesList(strXmlResponse, keyWord, count);

            return listMod;
        }
      
        private List<String> getModulesList(string strXmlResponse, string keyWord, int count)
        {
            List<String> modulesList = new List<String>();

            for (int i = 0; i < count; i++)
            {
                int index = strXmlResponse.IndexOf(keyWord) + keyWord.Length + 3; //3 is the num of char of ":"

                strXmlResponse = strXmlResponse.Substring(index);
                int nextIndex = strXmlResponse.IndexOf("\",\"");
                modulesList.Add(strXmlResponse.Substring(0, nextIndex));
            }

            return modulesList;
        }


        public List<String> getSemNum(List<String> originalList)
        {
            List<String> modifyList = new List<String>();

            for (int i = 0; i < originalList.Count; i++)
            {
                string tempStr = Regex.Match(originalList[i], @"\d+").Value;
                modifyList.Add(tempStr);
            }

            return modifyList;
        }
 
        public List<String> getNameList(string courseID)
        {
            //string token = (string)(Session["sessionToken"]);

            List<String> nameList = new List<String>();
            string token = myToken.getToken();
            string authTokenUrl = "&AuthToken=";
            string getOptionUrl = "Class_Roster?";
            string courseIdUrl = "&CourseID=";
            string url = ivleUrl + getOptionUrl + apiKey + authTokenUrl + token + courseIdUrl + courseID;



            string nameListText = myXmlResponse.getXmlResponse(url);

            //string test = trimNameList(nameListText);

            nameList = trimNameList(nameListText);

            return nameList;

            //return test;

        }

        public List<String> getStudIdList(string courseID)
        {
            //string token = (string)(Session["sessionToken"]);

            List<String> studIdList = new List<String>();
            string token = myToken.getToken();
            string authTokenUrl = "&AuthToken=";
            string getOptionUrl = "Class_Roster?";
            string courseIdUrl = "&CourseID=";
            string url = ivleUrl + getOptionUrl + apiKey + authTokenUrl + token + courseIdUrl + courseID;



            string studIdListText = myXmlResponse.getXmlResponse(url);

            //string test = trimNameList(nameListText);

            studIdList = trimStudIdList(studIdListText);

            return studIdList;

            //return test;

        }

        public List<String> getUserIdList(string courseID)
        {
            //string token = (string)(Session["sessionToken"]);

            List<String> idList = new List<String>();
            string token = myToken.getToken();
            string authTokenUrl = "&AuthToken=";
            string getOptionUrl = "Class_Roster?";
            string courseIdUrl = "&CourseID=";
            string url = ivleUrl + getOptionUrl + apiKey + authTokenUrl + token + courseIdUrl + courseID;



            string nameListText = myXmlResponse.getXmlResponse(url);

            //string test = trimNameList(nameListText);

            idList = getStudentListID(nameListText);

            return idList;

            //return test;

        }

        private List<String> getStudentListID(string nameListText)
        {
            List<String> studentID = new List<String>();
            int index, nextIndex;
            int count = Regex.Matches(nameListText, "\"UserID\"").Count;


            //string test= "";
            for (int i = 0; i < count; i++)
            {
                index = nameListText.IndexOf("UserID") + 9;
                nameListText = nameListText.Substring(index);
                nextIndex = nameListText.IndexOf("\",\"");
                studentID.Add(nameListText.Substring(0, nextIndex));
                //test = nameListText.Substring(0, nextIndex);
            }

            return studentID;
        }
        private List<String> trimNameList(string nameListText)
        {
            List<String> trimList = new List<String>();
            int index, nextIndex;
            
            int count = Regex.Matches(nameListText, "\"Name\"").Count;


            //string test= "";
            for (int i = 0; i < count; i++)
            {
                index = nameListText.IndexOf("Name") + 7;
                nameListText = nameListText.Substring(index);
                nextIndex = nameListText.IndexOf("\",\"");
                trimList.Add(nameListText.Substring(0, nextIndex));
                //test = nameListText.Substring(0, nextIndex);
            }

            /*
            index = nameListText.IndexOf("Name") + 7;
            nameListText = nameListText.Substring(index);
            nextIndex = nameListText.IndexOf("\",\"");
            string 
            */

            //return count + " " + test;
            return trimList;
        }

        private List<String> trimStudIdList(string nameListText)
        {
            List<String> trimList = new List<String>();
            int index, nextIndex;

            int count = Regex.Matches(nameListText, "\"UserID\"").Count;


            //string test= "";
            for (int i = 0; i < count; i++)
            {
                index = nameListText.IndexOf("UserID") + 9;
                nameListText = nameListText.Substring(index);
                nextIndex = nameListText.IndexOf("\",\"");
                trimList.Add(nameListText.Substring(0, nextIndex));
                //test = nameListText.Substring(0, nextIndex);
            }

            /*
            index = nameListText.IndexOf("Name") + 7;
            nameListText = nameListText.Substring(index);
            nextIndex = nameListText.IndexOf("\",\"");
            string 
            */

            //return count + " " + test;
            return trimList;
        }
    }
}