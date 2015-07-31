<%@ Page Title="My Chat" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="Chats.aspx.cs" Inherits="ModuleBuddiesASP.Chats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <script type="text/javascript">
         
        
    </script>
  

    <div>
         <!-- Custom Tabs (Pulled to the right) -->
        <div class="nav-tabs-custom">
                <ul class="nav nav-tabs pull-right">
                  <li ><a href="ChatManage.aspx">Manage Chat</a></li>
                  <li ><a href="ChatNew.aspx">New Chat</a></li>
                  <li ><a href="ChatPublic.aspx">Public Chat</a></li>
                  
                  <li class="active"><a href="#MyChat_Tab" data-toggle="tab">My Chat</a></li>      
                  <li class="pull-left header"><i class=" fa fa-wechat"></i> 
                       <asp:Label ID="titleLabel" runat="server" Text="Chat"></asp:Label>
                  </li>
                </ul>
                <!-- Tab Content -->                 
                <div class="tab-content">

                  <!-- Manage Chat Tab -->
                  <div class="tab-pane" id="ManageChat_Tab">
                        <div class="row">
                            <!-- Left Side -->
                            <div class="col-md-6">
                      
                                <div class="box-header">
                                    <h3 class="box-title">Edit Public Chat Name</h3>
                                </div><!-- /.box-header -->
                            
                                <div class="box-body" >
                                    <!-- Public Chat List -->
                                    <div class="form-group">
                                        <asp:ListBox class="form-group form-control"  ID="editPublicNameListBox" runat="server" Height="200px" OnSelectedIndexChanged="SelectedIndexChanged_PublicName" AutoPostBack="true"
                                             DataSourceID="SqlDataSource3" DataTextField="GroupName" DataValueField="UniqueID"></asp:ListBox>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ModulesDBConnectionString %>" SelectCommand="SELECT [UniqueID], [GroupName] FROM [PublicChatAuthor] WHERE ([AuthorID] = @userID) ORDER BY [GroupName]">
                                            <SelectParameters>
                                                <asp:CookieParameter CookieName="userIdCookie" Name="userID" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource> 
                                        
                                        
                                        <asp:Label ID="editPublicLabel" runat="server" Text="New Public Chat Name"></asp:Label>
                                        <asp:TextBox ID="editPublicNameTextBox" runat="server" class="form-control" placeholder="New Name"></asp:TextBox>
                                        <asp:Label ID="idPublicLabel" runat="server" Text="" Visible="false"></asp:Label>
                                        <br />
                                        <asp:Button ID="editPublicNameButton" runat="server" Text="Change" class="btn btn-primary pull-left" OnClick="editPublicNameButton_Click" />
                                        <asp:Button ID="deletePublicNameButton" runat="server" Text="Delete" class="btn btn-default pull-right" OnClick="deletePublicNameButton_Click"/>                      
                                    </div>

                                    <br />

                                    

                                </div>
                         
                            </div>
                      

                            <!-- Right Side -->
                            <div class="col-md-6">
                                <div class="box-header">
                                    <h3 class="box-title">Edit Group Name</h3>
                                </div><!-- /.box-header -->
                            
                                <div class="box-body" >
                                    <!-- Group Chat List -->
                                    <div class="form-group">
                                        <asp:ListBox class="form-group form-control"  ID="editGroupNameListBox" runat="server" Height="200px" OnSelectedIndexChanged="SelectedIndexChanged_GroupName" AutoPostBack="true"
                                             DataSourceID="SqlDataSource4" DataTextField="GroupName" DataValueField="UniqueID"></asp:ListBox>
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ModulesDBConnectionString %>" SelectCommand="SELECT [UniqueID], [GroupName] FROM [PrivateChatList] WHERE ([MemberID] = @userID) ORDER BY [GroupName]">
                                            <SelectParameters>
                                                <asp:CookieParameter CookieName="userIdCookie" Name="userID" Type="String" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>   
                                        
                                        
                                        <asp:Label ID="editGroupLabel" runat="server" Text="New Group Chat Name"></asp:Label>
                                        <asp:TextBox ID="editGroupNameTextBox" runat="server" class="form-control" placeholder="New Name"></asp:TextBox>
                                        <asp:Label ID="idGroupLabel" runat="server" Text="" Visible="false"></asp:Label>
                                        <br />
                                        <asp:Button ID="editGroupNameButton" runat="server" Text="Change" class="btn btn-primary pull-left" OnClick="editGroupNameButton_Click" />
                                        <asp:Button ID="deleteGroupNameButton" runat="server" Text="Delete" class="btn btn-default pull-right" OnClick="deleteGroupNameButton_Click"/>                    
                                    </div>
                                 </div>
                            </div>
                         
                        </div>

                    </div>   
                  
                        
                  <!-- Pulic Chat Tab -->  
                  <div class="tab-pane" id="PublicChat_Tab">
                     
                        <div class="box-body">
                             
                            <div class="input-group input-group">
                                <input id="publicChatTextBox" type="text" class="form-control" runat="server" placeholder="Public Chat's Name ..."/>  
                                <span class="input-group-btn">
                                    <asp:Button ID="Button1" class="btn btn-primary btn-flat"  runat="server" Text="Create Public Chat"  OnClick="createPublicChatButton_Click" /> 
                                </span>
                             </div>

                             <asp:Label ID="warningLabel2" runat="server" Text="Please Enter a Name" ForeColor="Red" Visible="false"></asp:Label>
                             <br />
                                 
                            <table id="chatGroupTable" class="table table-bordered table-hover">
                                <thead>                  
                                  <tr>
                                    <th >Chat Groups &emsp;&emsp;&emsp;&emsp;&emsp;</th>
                                    <th >Created By</th>
                                    <th >Join</th>      
                                  </tr>
                                </thead>

                                <tbody id="chatGroupListDiv" runat="server">                                        
                                   <!-- Content -->              
                                </tbody>

                                <tfoot>              
                                </tfoot>
                            </table>
                        </div><!-- /.box-body -->
                     

                  </div>
                          
                  <!-- New Chat Tab -->
                  <div class="tab-pane" id="NewChat_Tab" >
                    <div class="row">
                      <!-- Left Side -->
                      <div class="col-md-6">
                      
                            <div class="box-header">
                                <h3 class="box-title">Individual Chat</h3>
                            </div><!-- /.box-header -->
                            
                             <div class="box-body" >
                                <asp:Label ID="Label1" runat="server" Text="Friend List"></asp:Label>
                                    <asp:ListBox class="form-group form-control"  ID="friendListBox2" runat="server" Height="315px" SelectionMode="Single"  DataSourceID="SqlDataSource2" DataTextField="friendName" DataValueField="friendID"></asp:ListBox>
                                    
                                     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ModulesDBConnectionString %>" SelectCommand="SELECT [friendID], [friendName] FROM [FriendList] WHERE ([userID] = @userID) ORDER BY [friendName]">
                                        <SelectParameters>
                                            <asp:CookieParameter CookieName="userIdCookie" Name="userID" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                      
                                   <asp:Button ID="chatFriendButton" runat="server" Text="New Chat" class="btn btn-primary pull-left" OnClick="chatFriendButton_Click" />
                             </div>
                         
                       </div>
                      

                      <!-- Right Side -->
                      <div class="col-md-6">
                            <div class="box-header">
                                <h3 class="box-title">Group Chat</h3>
                            </div><!-- /.box-header -->
                            
                             <div class="box-body" >
                                 <div class="form-group">
                                    <asp:Label ID="groupChatLabel" runat="server" Text="Group Chat's Name"></asp:Label>
                                    <input id="groupChatTextBox" type="text" class="form-control" runat="server" placeholder="Group Chat's Name ..."/>  
                                     <asp:Label ID="warningLabel1" runat="server" Text="Please Enter Group Name" ForeColor="Red" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="friendListLabel" runat="server" Text="Friend List"></asp:Label>
                                    <asp:ListBox class="form-group form-control"  ID="friendListBox" runat="server" Height="240px" SelectionMode="Multiple"  DataSourceID="SqlDataSource1" DataTextField="friendName" DataValueField="friendID"></asp:ListBox>
                                    
                                     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ModulesDBConnectionString %>" SelectCommand="SELECT [friendID], [friendName] FROM [FriendList] WHERE ([userID] = @userID) ORDER BY [friendName]">
                                        <SelectParameters>
                                            <asp:CookieParameter CookieName="userIdCookie" Name="userID" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                      
                                   <asp:Button ID="createGroupChatButton" runat="server" Text="Create Group" class="btn btn-primary pull-left" OnClick="createGroupChatButton_Click" />
                                 </div>
                             </div>
                         
                       </div>

                    </div>   
                  </div>

                  <!-- My Chat Tab -->
                  <div class="tab-pane active" id="MyChat_Tab">
                      
                   
                         <div class="box-header">
                            <h2 class="box-title">My Chats</h2>                          
                        </div><!-- /.box-header -->

                        <div class="box-body">
                            <table id="myChatTable" class="table table-bordered table-hover " >
                                <thead>                  
                                  <tr>
                                    <th >Group Name &emsp;&emsp;&emsp;&emsp;&emsp;</th>
                                    <th >Type &emsp;&emsp;&emsp;&emsp;&emsp;</th> 
                                    <th >Open</th> 
                                    <th >Delete</th>     
                                  </tr>
                                </thead>

                                <tbody id="myChatListDiv" runat="server">                                        
                                   <!-- Content -->              
                                </tbody>

                                <tfoot>              
                                </tfoot>
                            </table>
                      
                      </div><!-- /.box -->

                  </div>                                 
                </div><!-- /.tab-content -->
        </div> <!-- /.Custom Tabs-content -->
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
   
    <script src="Scripts/ExtraScript/ChatScript.js"  type="text/javascript"></script>

    <script type="text/javascript">
      $(function () {
          $("#chatGroupTable").dataTable({
              "order": [[0, "asc"]]
          });
          $("#myChatTable").dataTable({
              "order": [[0, "asc"]]
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
