<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="ClassList.aspx.cs" Inherits="ModuleBuddiesASP.ClassList" %>
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
                  <li class="active"><a href="#Class_Tab" data-toggle="tab">Class Roster</a></li>
                    <li ><a href="Friend.aspx" >My Friends</a></li>
          
                  <li class="pull-left header"><i class=" fa fa-users"></i> 
                       <asp:Label ID="friendTitleLabel" runat="server" Text="Friends"></asp:Label>
                  </li>
                </ul>
                
           
                 
                 
                    <!-- Class Roster Tab -->
                 <div class="tab-pane active" id="Class_Tab">
                     
         <div class="box-header">
           <h2 class="box-title">Modules Class Roster</h2>
             <div class="input-group">
                   <asp:DropDownList ID="moduleDropDownList" runat="server" class="form-control"></asp:DropDownList>
                    <span class="input-group-btn">
                      <asp:Button ID="moduleButton" class="btn btn-primary btn-flat" Text="Select Module" runat="server"  onclick="moduleButton_Click"></asp:Button>
                       
                        
                    </span>
                  </div><!-- /input-group -->
         </div><!-- /.box-header -->
         <div class="box-body">
             <!--class="table table-bordered table-striped" -->
             <table id="example1" class="table table-bordered table-hover" >
               <thead>
                 <tr>
                   <th >Matric No.</th>       
                   <th >Name</th>          
                   <th >Module</th>  
                   <th >Add</th>       
                 </tr>
               </thead>
               <tbody id="groupListDiv" runat="server">
                     <!-- Content -->    
                
               </tbody>
               <tfoot>
                     
               </tfoot>
             </table>
            </div><!-- /.box-body -->
         </div><!-- /.box -->

                    
                
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
