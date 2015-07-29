<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="Friend.aspx.cs" Inherits="ModuleBuddiesASP.Friend" %>
<%@ MasterType VirtualPath="~/AdminLTE.Master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <script type="text/javascript">
          function btnLogin_Click(groupName) {
              window.location.href = "maingroupchat.aspx?gid=" + groupName;

          }
          function addFriend(id, name) {
              window.location.href = "Friend.aspx?id=" + id + "&name=" + name;

          }

    </script>
    <div>
        <div class="nav-tabs-custom">
               
                <ul class="nav nav-tabs pull-right">
              <!--    <li><a href="#Add_Tab" data-toggle="tab">Add Friends</a></li>   -->               
                  <li ><a href="ClassList.aspx">Class Roster</a></li>
                    <li class="active"><a href="#Friend_Tab" data-toggle="tab">My Friends</a></li>
          
                  <li class="pull-left header"><i class=" fa fa-users"></i> 
                       <asp:Label ID="friendTitleLabel" runat="server" Text="Friends"></asp:Label>
                  </li>
                </ul>
                
            <div class="tab-content">

                 
                  <!-- Friend List Tab -->
                 <div class="tab-pane  active" id="Friend_Tab">
                      <div class="row">
                          <!--Left side friend lsit -->
                        <div class="col-md-6">
                              <div class="box-header">
                                <h3 class="box-title">Friend List</h3>
                                </div>
                                    <div class="box-body" >
                    <div class="form-group">
                     <asp:ListBox class="form-group form-control"  ID="editNameListBox" runat="server" Height="370px" OnSelectedIndexChanged="SelectedIndexChanged" AutoPostBack="true"
                         DataSourceID="SqlDataSource2" DataTextField="friendName" DataValueField="friendID"></asp:ListBox>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ModulesDBConnectionString %>" SelectCommand="SELECT [friendID], [friendName] FROM [FriendList] WHERE ([userID] = @userID) ORDER BY [friendName]">
                            <SelectParameters>
                            <asp:CookieParameter CookieName="userIdCookie" Name="userID" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        
                         <asp:Button ID="chatButton" runat="server" Text="Chat" class="btn btn-primary pull-left" OnClick="chatFriendButton_Click" />
                         <asp:Button ID="Button3" runat="server" Text="Delete" class="btn btn-default pull-right" OnClick="deleteButton_Click"/>
                       
                  </div><!-- /.box-body -->
                     </div>
                            
                          
                        </div>
                    
                         <!--Ride side edit friend list -->
                          <div class="col-md-6">
                          <div class="box-header">
                  <h3 class="box-title">Add Friends</h3>
                </div>
                     <div class ="box-body">
                         <asp:Label ID="friendNameLabel" runat="server" Text="Name"></asp:Label>
                         <asp:TextBox ID="friendNameTextBox" runat="server"  class="form-control" placeholder="Friend's Name"></asp:TextBox>
                         <asp:Label ID="friendIdLabel" runat="server" Text="ID"></asp:Label>
                         <asp:TextBox ID="friendIdTextBox" runat="server" class="form-control" placeholder="Student ID w/o Alphabet at the end. E.g. a0123456 "></asp:TextBox>
                         <asp:Label ID="warningLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
                         <br />
                         <asp:Button ID="addFriendButton" runat="server" Text="Add" class="btn btn-primary" OnClick="addFriendButton_Click" />

                     </div>
                              <br />

                       <div class="box-header">
                  <h3 class="box-title">Change Friend's Name</h3>
                </div>
                    <div class ="box-body">
                         <asp:Label ID="editLabel" runat="server" Text="New Name"></asp:Label>
                        <asp:TextBox ID="editNameTextBox" runat="server" class="form-control" placeholder="New Name"></asp:TextBox>
                        <asp:Label ID="idLabel" runat="server" Text="" Visible="false"></asp:Label>
                        <br />
                         <asp:Button ID="editNameButton" runat="server" Text="Change" class="btn btn-primary pull-left" OnClick="editNameButton_Click" />
                         <asp:Button ID="Button1" runat="server" Text="Delete" class="btn btn-default pull-right" OnClick="deleteButton_Click"/>
                    </div>
                            </div>
                        </div>
                   </div>
                       
            </div>

            </div>
    </div>

      <!-- REQUIRED JS SCRIPTS -->
    <!-- jQuery 2.1.4 -->
    <script src="AdminLTE/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="AdminLTE/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
  
   
    
    
     <!-- DATA TABES SCRIPT -->
    <script src="AdminLTE/plugins/datatables/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="AdminLTE/plugins/datatables/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <!-- Slimscroll -->
    <script src="AdminLTE/plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src="AdminLTE/plugins/fastclick/fastclick.min.js"></script>

  <script type="text/javascript">
      $(function () {
          $("#example1").dataTable({
              "order": [[1, "asc"]]
          });
          $('#example2').dataTable({
              "bPaginate": true,
              "bLengthChange": false,
              "bFilter": false,
              "bSort": true,
              "bInfo": true,
              "bAutoWidth": false
          });
      });
    </script>
   
</asp:Content>
