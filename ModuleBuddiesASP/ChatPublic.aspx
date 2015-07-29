<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="ChatPublic.aspx.cs" Inherits="ModuleBuddiesASP.ChatPublic" %>
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
                  <li  class="active"><a href="#PublicChat_Tab" data-toggle="tab">Public Chat</a></li>
                  
                  <li><a href="Chats.aspx">My Chat</a></li>      
                  <li class="pull-left header"><i class=" fa fa-wechat"></i> 
                       <asp:Label ID="titleLabel" runat="server" Text="Chat"></asp:Label>
                  </li>
                </ul>
                <!-- Tab Content -->                 
                <div class="tab-content">

                
                        
                  <!-- Pulic Chat Tab -->  
                  <div class="tab-pane active" id="PublicChat_Tab">
                     
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
