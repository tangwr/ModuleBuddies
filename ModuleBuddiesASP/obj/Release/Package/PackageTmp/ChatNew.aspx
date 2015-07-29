<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="ChatNew.aspx.cs" Inherits="ModuleBuddiesASP.ChatNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- Custom Tabs (Pulled to the right) -->
        <div class="nav-tabs-custom">
                <ul class="nav nav-tabs pull-right">
                  <li ><a href="ChatManage.aspx">Manage Chat</a></li>
                  <li class="active"><a href="#NewChat_Tab" data-toggle="tab">New Chat</a></li>
                  <li ><a href="ChatPublic.aspx">Public Chat</a></li>
                  
                  <li ><a href="Chats.aspx">My Chat</a></li>      
                  <li class="pull-left header"><i class=" fa fa-wechat"></i> 
                       <asp:Label ID="titleLabel" runat="server" Text="Chat"></asp:Label>
                  </li>
                </ul>
                <!-- Tab Content -->                 
                <div class="tab-content">

                 
                          
                  <!-- New Chat Tab -->
                  <div class="tab-pane active" id="NewChat_Tab" >
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
                                <h3 class="box-title">Create Group Chat</h3>
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
                      
                                   <asp:Button ID="createGroupChatButton" runat="server" Text="Create Group Chat" class="btn btn-primary pull-left" OnClick="createGroupChatButton_Click" />
                                 </div>
                             </div>
                         
                       </div>

                    </div>   
                  </div>

                 
                                                 
                </div><!-- /.tab-content -->
        </div> <!-- /.Custom Tabs-content -->
</asp:Content>
