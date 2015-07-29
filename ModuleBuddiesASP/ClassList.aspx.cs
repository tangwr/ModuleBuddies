using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ModuleBuddiesASP.HelperClasses;

namespace ModuleBuddiesASP
{
    public partial class ClassList : System.Web.UI.Page
    {
        HelperClasses.ModuleInfo modInfo = new HelperClasses.ModuleInfo();
        HelperClasses.CourseID course = new HelperClasses.CourseID();
        HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();

        List<String> ListModCode = new List<String>();
        List<String> ListModName = new List<String>();
        List<String> ListCourseSem = new List<String>();
        List<String> ListAcadYear = new List<String>();
        protected void Page_Load(object sender, EventArgs e)
        {
     
            HelperClasses.Token myToken = new HelperClasses.Token();

            if (myToken.getToken() == "")
            {
                Response.Redirect("Home.aspx");
            }

            ListModCode = modInfo.getModInfo("CourseCode");
            ListModName = modInfo.getModInfo("CourseName");
            ListCourseSem = modInfo.getSemNum(modInfo.getModInfo("CourseSemester"));
            ListAcadYear = modInfo.getModInfo("CourseAcadYear");
          

            /* from test friends */

            //groupListDiv.Controls.Add(new LiteralControl() { Text = moduleFriendList() });

            int value = moduleDropDownList.Items.Count;

            if (value < ListModName.Count)
            {
                for (int i = 0; i < ListModName.Count; i++)
                {
                    moduleDropDownList.Items.Add(ListModName[i]);
                }
            }
        }

        protected void moduleButton_Click(object sender, EventArgs e)
        {
            List<String> nameList = new List<String>();
            List<String> studIdList = new List<String>();
            ListModCode = modInfo.getModInfo("CourseCode");

            int index = moduleDropDownList.SelectedIndex;
            nameList = getNameList(moduleDropDownList.SelectedIndex);
            studIdList = getStudIdList(index);
            string modName = moduleDropDownList.SelectedItem.Text;
            string modCode = ListModCode[index];
            groupListDiv.Controls.Add(new LiteralControl() { Text = indivModuleFriendList(nameList, studIdList, modName, modCode, index) });
        }

        public String indivModuleFriendList(List<String > nameList, List<String > studIdList, String modName, String modCode, int index)
        {
            string htmlText = "";
                   
            for (int j = 0; j < nameList.Count; j++)
            {

                htmlText += @"
                   <tr>
                   
                     <td>" + studIdList[j] + @" </td>              
                     <td>" + nameList[j] + @"</td>
                     <td>" + modCode + " - " + modName + @"</td>                    
                     <td><a ID=""addFriendButton"" runat=""server""  style=""cursor: pointer;""
                        onclick=""addFriend('" + studIdList[j] + @"','" + nameList[j] + @"')"" >Add</a> 
                    </td>
                     
                   </tr>";
                /*
                  <td><a id=""ContentPlaceHolder1_LinkButton1"" href=""javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$LinkButton1&#39;,&#39;&#39;)"">LinkButton</a>"
                  + @"</td>
                 */
            }

            return htmlText;
        }


        public String moduleFriendList()
        {
            HelperClasses.ModuleInfo modInfo = new HelperClasses.ModuleInfo();
            HelperClasses.CourseID course = new HelperClasses.CourseID();
            HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();

            List<String> ListModCode = new List<String>();
            List<String> ListModName = new List<String>();
            List<String> ListCourseSem = new List<String>();
            List<String> ListAcadYear = new List<String>();

            string htmlText = "";
            ListModCode = modInfo.getModInfo("CourseCode");
            ListModName = modInfo.getModInfo("CourseName");
            ListCourseSem = modInfo.getSemNum(modInfo.getModInfo("CourseSemester"));
            ListAcadYear = modInfo.getModInfo("CourseAcadYear");

            //for (int i = 0; i < ListModCode.Count; i++)
            for (int i = 0; i < 0; i++)
            {
                List<String> nameList = new List<String>();
                nameList = getNameList(i);
                List<String> studIdList = new List<String>();
                studIdList = getStudIdList(i);

                for (int j = 0; j < nameList.Count; j++)
                {

                    htmlText += @"
                   <tr>
                   
                     <td>" + studIdList[j] + @" </td>              
                     <td>" + nameList[j] + @"</td>
                     <td>" + ListModCode[i] + " - " + ListModName[i] + @"</td>                    
                     <td><a ID=""addFriendButton"" runat=""server""  style=""cursor: pointer;""
                        onclick=""addFriend('" + studIdList[j] + @"','" + nameList[j] + @"')"" >Add</a> 
                    </td>
                     
                   </tr>";
                    /*
                      <td><a id=""ContentPlaceHolder1_LinkButton1"" href=""javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$LinkButton1&#39;,&#39;&#39;)"">LinkButton</a>"
                      + @"</td>
                     */
                }
            }
            return htmlText;
        }

        public List<String> getNameList(int i)
        {
            HelperClasses.ModuleInfo modInfo = new HelperClasses.ModuleInfo();
            HelperClasses.CourseID course = new HelperClasses.CourseID();
            HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();

            List<String> ListModCode = new List<String>();
            List<String> ListModName = new List<String>();
            List<String> ListCourseSem = new List<String>();
            List<String> ListAcadYear = new List<String>();


            ListModCode = modInfo.getModInfo("CourseCode");
            ListModName = modInfo.getModInfo("CourseName");
            ListCourseSem = modInfo.getSemNum(modInfo.getModInfo("CourseSemester"));
            ListAcadYear = modInfo.getModInfo("CourseAcadYear");

            List<String> nameList = new List<String>();

            string courseID = course.getCourseID(ListModCode[i], ListAcadYear[i], ListCourseSem[i]);

            nameList = modInfo.getNameList(courseID);

            return nameList;
        }

        public List<String> getStudIdList(int i)
        {
            HelperClasses.ModuleInfo modInfo = new HelperClasses.ModuleInfo();
            HelperClasses.CourseID course = new HelperClasses.CourseID();
            HelperClasses.IvleUserInfo userInfo = new HelperClasses.IvleUserInfo();

            List<String> ListModCode = new List<String>();
            List<String> ListModName = new List<String>();
            List<String> ListCourseSem = new List<String>();
            List<String> ListAcadYear = new List<String>();


            ListModCode = modInfo.getModInfo("CourseCode");
            ListModName = modInfo.getModInfo("CourseName");
            ListCourseSem = modInfo.getSemNum(modInfo.getModInfo("CourseSemester"));
            ListAcadYear = modInfo.getModInfo("CourseAcadYear");

            List<String> studIdList = new List<String>();

            string courseID = course.getCourseID(ListModCode[i], ListAcadYear[i], ListCourseSem[i]);

            studIdList = modInfo.getStudIdList(courseID);

            return studIdList;
        }
    }

}