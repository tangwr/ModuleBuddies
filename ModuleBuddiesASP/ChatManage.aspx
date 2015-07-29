<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="ChatManage.aspx.cs" Inherits="ModuleBuddiesASP.ManageChat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div>
         <!-- Custom Tabs (Pulled to the right) -->
        <div class="nav-tabs-custom">
                <ul class="nav nav-tabs pull-right">
                  <li class="active"><a href="#ManageChat_Tab" data-toggle="tab">Manage Chat</a></li>
                  <li ><a href="ChatNew.aspx">New Chat</a></li>
                  <li ><a href="ChatPublic.aspx" >Public Chat</a></li>
                  
                  <li ><a href="Chats.aspx" >My Chat</a></li>      
                  <li class="pull-left header"><i class=" fa fa-wechat"></i> 
                       <asp:Label ID="titleLabel" runat="server" Text="Chat"></asp:Label>
                  </li>
                </ul>
                <!-- Tab Content -->                 
                <div class="tab-content">

                  <!-- Manage Chat Tab -->
                  <div class="tab-pane active" id="ManageChat_Tab">
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
                  
                        
                                           
                </div><!-- /.tab-content -->
        </div> <!-- /.Custom Tabs-content -->
    </div>

     
  
   
    
    
    
   
</asp:Content>

