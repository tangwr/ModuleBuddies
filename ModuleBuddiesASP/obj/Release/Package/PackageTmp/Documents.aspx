<%@ Page Title="Documents" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="Documents.aspx.cs" Inherits="ModuleBuddiesASP.Documents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">
        
        function openDoc(docName, uniqueID, userID) {
            var url = window.location.href;
            window.location.href = "MyDoc.aspx?docName=" + docName + "&docId=" + uniqueID + "&userId=" + userID;
        }

        function deleteDoc(type, uniqueID, userID) {
            var dc = confirm("Are you sure you want to delete?");
            if (dc == true) {
                var url = window.location.href;
                window.location.href = "Delete.aspx?type=" + type + "&uid=" + uniqueID + "&url=" + url;
            }
        }

        
    </script>

    <!-- New Area -->
    <div>
         <!-- Custom Tabs (Pulled to the right) -->
        <div class="nav-tabs-custom">
                <ul class="nav nav-tabs pull-right">
                  <li ><a href="#NewDoc_Tab" data-toggle="tab">New Documents</a></li>
                  <li  class="active"><a href="#MyDoc_Tab" data-toggle="tab">My Documents</a></li>
                  
                 
                  <li class="pull-left header"><i class=" fa fa-file-text"></i> 
                       <asp:Label ID="titleLabel" runat="server" Text="Documents"></asp:Label>
                  </li>
                </ul>
                <!-- Tab Content -->                 
                <div class="tab-content">

                  <!-- New Document Tab -->  
                  <div class="tab-pane" id="NewDoc_Tab">
                     
                        <div class="box-body">
                                                      
                              <div class="row">
                                    <div class="col-md-6">
                                        <div >
                                            <div class="box-header">
                                                <h3 class="box-title">My Documents</h3>
                                            </div><!-- /.box-header -->

                                            <div class="box-body" >
                    
                                                <div class="form-group">
                                                    <asp:ListBox class="form-group form-control" ID="docListBox" runat="server"  Height="395px" DataSourceID="SqlDataSource2" DataTextField="docName" DataValueField="docId"></asp:ListBox>
                                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ModulesDBConnectionString %>" SelectCommand="SELECT [docName], [docId] FROM [Documents] WHERE ([memberID] = @memberID) ORDER BY [docName]">
                                                        <SelectParameters>
                                                            <asp:CookieParameter CookieName="userIdCookie" Name="memberID" Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                   
                                                </div>
                    
                                                <asp:Button ID="openButton" runat="server" Text="Open Document" class="btn btn-primary pull-left" OnClick="openButton_Click"/>

                                                <asp:Button ID="deleteButton" runat="server" Text="Delete" class="btn btn-default pull-right" OnClick="deleteButton_Click"/>
                                            </div>
       
                                         </div>
                                     </div>

                                    <div class="col-md-6">
                                        <div ">
                                            <div class="box-header">
                                                <h3 class="box-title">Create New Document</h3>
                                            </div><!-- /.box-header -->
                                            <div class="box-body" >
                                                <asp:Label ID="Label1" runat="server" Text="Document's Name"></asp:Label>
                                                <input id="docTextBox" type="text" class="form-control" runat="server" placeholder="Document's Name ..."/>   
                                            </div> 
                                            <div class="box-header">
                                                <h3 class="box-title">Add Friend</h3>
                                                <asp:Label ID="Label2" runat="server" Text=" (Press 'Ctrl' + 'click' to select one or more friends)"></asp:Label>
                                            </div><!-- /.box-header -->

                                            <div class="box-body" >
                                                <div class="form-group">
                                                    <asp:ListBox class="form-group form-control"  ID="friendListBox" runat="server" Height="280px" SelectionMode="Multiple"  DataSourceID="SqlDataSource1" DataTextField="friendName" DataValueField="friendID"></asp:ListBox>
                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ModulesDBConnectionString %>" SelectCommand="SELECT [friendID], [friendName] FROM [FriendList] WHERE ([userID] = @userID) ORDER BY [friendName]">
                                                            <SelectParameters>
                                                            <asp:CookieParameter CookieName="userIdCookie" Name="userID" Type="String" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                      
                                                         <asp:Button ID="createButton" runat="server" Text="Create New Document" class="btn btn-primary pull-left" OnClick="createButton_Click" />
                
                                                  </div>
                                              </div>
                                          </div>
                                        </div>
                                        </div>   
                            
                        </div><!-- /.box-body -->
                     

                  </div>
                        
                  <!-- My Document Tab -->  
                  <div class="tab-pane active" id="MyDoc_Tab">
                     
                        <div class="box-body">
                                                      
                                 
                            <table id="documentTable" class="table table-bordered table-hover">
                                <thead>                  
                                  <tr>
                                    <th >Documents &emsp;&emsp;&emsp;&emsp;&emsp;</th>
                                    <th >Open</th>
                                    <th >Delete</th>      
                                  </tr>
                                </thead>

                                <tbody id="documentListDiv" runat="server">                                        
                                   <!-- Content -->              
                                </tbody>

                                <tfoot>              
                                </tfoot>
                            </table>
                        </div><!-- /.box-body -->
                     

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
          $("#documentTable").dataTable({
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
