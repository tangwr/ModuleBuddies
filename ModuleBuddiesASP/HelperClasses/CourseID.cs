using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace ModuleBuddiesASP.HelperClasses
{
    public class CourseID
    {
        private static string ivleUrl = "https://ivle.nus.edu.sg/api/Lapi.svc/";
        private static string apiKey = "APIKey=JinwNKRnSNSXfoTIhY1X0";
        public string getCourseID(string modCode, string acadYear, string sem)
        {
            // string token = (string)(Session["sessionToken"]);
            Token myToken = new Token();
            XmlResponse myXmlResponse = new XmlResponse();

            string token = myToken.getToken();
            string authTokenUrl = "&AuthToken=";
            string getOptionUrl = "Modules_Search?";
            string otherOptionUrl = "&IncludeAllInfo=false";
            string moduleCodeUrl = "&ModuleCode=";
            string semesterUrl = "&Semester=Semester ";
            string acadYearUrl = "&AcadYear=";
            string url = ivleUrl + getOptionUrl + apiKey + authTokenUrl + token + otherOptionUrl +
                moduleCodeUrl + modCode + semesterUrl + sem + acadYearUrl + acadYear;

            string courseIdText = myXmlResponse.getXmlResponse(url);
            string courseID = trimCourseIdText(courseIdText, modCode);

            return courseID;
        }
        
        private string trimCourseIdText(string courseIdText, string courseCode)
        {
            int count = Regex.Matches(courseIdText, "CourseCode\":\"" + courseCode + "\"").Count;
            int index = 0, nextIndex = 0, idIndex = 0;
            string strCourseID = "";
            string temp, checkCode;

            for (int i = 0; i < count; i++)
            {
                index = courseIdText.IndexOf("CourseCode") + 13;
                temp = courseIdText.Substring(index);
                nextIndex = temp.IndexOf("\",\"");
                checkCode = courseIdText.Substring(index, nextIndex);

                if (checkCode.Equals(courseCode))
                {
                    idIndex = courseIdText.IndexOf("\"ID\"") + 6;
                    temp = courseIdText.Substring(idIndex);
                    nextIndex = temp.IndexOf("\",\"");
                    strCourseID = courseIdText.Substring(idIndex, nextIndex);
                    break;
                }
                else
                {
                    courseIdText = courseIdText.Substring(index);
                }
            }

            return strCourseID;
        }
        

    }
}