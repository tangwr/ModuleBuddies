<%@ Page Title="Documents" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="MyDoc.aspx.cs" Inherits="ModuleBuddiesASP.MyDoc"  validateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div> 
                    
              <!-- Custom Tabs (Pulled to the right) -->
              <div class="nav-tabs-custom">
               
                <ul class="nav nav-tabs pull-right">
                  <li ><a href="#Rename_Tab" data-toggle="tab">Rename</a></li>
                  <li ><a href="#Add_Tab" data-toggle="tab">Add Users</a></li>
                   
                  <li class="active"><a href="#Doc_Tab" data-toggle="tab">Documents</a></li>
        
                  <li class="pull-left header"><i class=" fa fa-file-text"></i> 
                       <asp:Label ID="docTitleLabel" runat="server" Text=""></asp:Label>
                  </li>
                </ul>
                                  
                <div class="tab-content">

                  <div class="tab-pane" id="Rename_Tab">
                      <div class="input-group input-group">
                                <input id="renameTextBox" type="text" class="form-control" runat="server" placeholder="New Group Name ..."/>  
                                <span class="input-group-btn">
                                    <asp:Button ID="renameButton" class="btn btn-primary btn-flat"  runat="server" Text="Rename"  OnClick="rename_Click" /> 
                                </span>
                       </div>
                  </div><!-- /.tab-pane -->

                  <div class="tab-pane" id="Add_Tab">
                      <div class="row">
                           
                        <!-- Left side -->
                          <div class="col-md-6">
                                <div class="box-header">
                                 <h3 class="box-title">Add New Users</h3> 
                                      </div>
                        <div class="form-group">
                         

                        <asp:ListBox class="form-group form-control"  ID="friendListBox" runat="server" Height="300px" SelectionMode="Multiple"  DataSourceID="SqlDataSource1" DataTextField="friendName" DataValueField="friendID"></asp:ListBox>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ModulesDBConnectionString %>" SelectCommand="SELECT [friendID], [friendName] FROM [FriendList] WHERE ([userID] = @userID) ORDER BY [friendName]">
                            <SelectParameters>
                            <asp:CookieParameter CookieName="userIdCookie" Name="userID" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                      
                         <asp:Button ID="addButton" runat="server" Text="Add Users" class="btn btn-primary pull-left" OnClick="addButton_Click" />
                        </div>
                              </div>
                        <!-- Right side -->
                         <div class="col-md-6">
                              <div class="box-header">
                                 <h3 class="box-title">Current Users</h3> 
                                      </div>

                              <div class="form-group">
                                 
                                  <asp:ListBox class="form-group form-control"  ID="usersListBox" runat="server" Height="300px" SelectionMode="Multiple" DataSourceID="SqlDataSource2" DataTextField="memberName" DataValueField="memberID"></asp:ListBox>
                                       
                          <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ModulesDBConnectionString %>" SelectCommand="SELECT [memberName], [memberID] FROM [Documents] WHERE (([docId] = @docId) AND ([memberID] &lt;&gt; @memberID)) ORDER BY [memberName]">
                              <SelectParameters>
                                  <asp:QueryStringParameter Name="docId" QueryStringField="docId" Type="String" />
                                  <asp:QueryStringParameter Name="memberID" QueryStringField="userId" Type="String" />
                              </SelectParameters>
                          </asp:SqlDataSource>

                                  <asp:Button ID="deleteButton" runat="server" Text="Delete Users" class="btn btn-primary pull-left" OnClick="deleteButton_Click" />
                                  </div>
                             </div>
                                </div>
                          
                  </div><!-- /.tab-pane -->
                    
                  <div class="tab-pane active" id="Doc_Tab">
                      <div class="box-body" >
                          
                        <textarea id="myText" runat="server" style="width:100%; height: 430px;"></textarea>  
                    </div>
                  </div><!-- /.tab-pane -->
                  
                    </div>
                </div><!-- /.tab-content -->
        </div>
             

            
         
   

    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/jquery-ui-1.9.2.js"></script>
    <script src="Scripts/jquery.signalR-2.2.0.js"></script>
    <!-- This is the magic js that gets generated dynamically -->
    <script src="/signalr/hubs"></script>
    <!--
    <script src="ckeditor/ckeditor.js" type="text/javascript"></script> -->
    <script type="text/javascript" src="tinymce/js/tinymce/tinymce.min.js" ></script> 
    <!-- Original Script -->
   <!--
     <script type="text/javascript">

        
         //CKEDITOR.replace('ctl00$ContentPlaceHolder1$myText');
         //CKEDITOR.replace('ContentPlaceHolder1_myText');
        //CKEDITOR.replace  myText.ClientID.Replace("_","$")
        // Create the hub
        var hub = $.connection.documentHub;

        // Create a function that the hub can call to draw a text
        hub.client.textTyped = function (text) {
            //if (cid == hub.connection.id)
            // return;
            //document.getElementById("myText").value = text;      

            //myText.ClientID  //myText.Name
            document.getElementById('<//%=myText.ClientID%>').value = text;
            //document.getElementById('ContentPlaceHolder1_myText').value = text;
            //CKEDITOR.instances.myText.setData(text);
        };

        // Start the connection
        $.connection.hub.start().done(function () {
            // Register the key event so we can allow users to type 
            //$(document).keyup(function (evt) {
            //hub.server.getUrl(window.location.href);
            var url = window.location.href;
            var index = url.indexOf("&docId=");
            var uidIndex = url.indexOf("&userId=");

            var docId = url.substring(index + 7, uidIndex);
            var uid = url.substring(uidIndex + 8);

            hub.server.registerChatGroup(docId, uid);

            $(document).on('keyup', function (evt) {
           
                        //your custom logic  
            
                // Notify the server
                //hub.server.typeText($.connection.hub.id, $('#myText').val());

                //myText.ClientID  //myText.Name
                hub.server.typeText($('#<//%=myText.ClientID%>').val(), docId);
                //hub.server.typeText($('#ContentPlaceHolder1_myText').val(), docId);
                //hub.server.typeText(instance.getData());
              });
          

        });

      
    </script> 
    -->
     <!--auto_focus: "<//%=myText.ClientID%>", -->
    <script type="text/javascript">
        $(document).ready(function () {
            tinymce.init({
                selector: "textarea",
                fontsize_formats: "8pt 9pt 10pt 11pt 12pt 14pt 16pt 18pt 20pt 22pt 24pt 26pt 28pt 36pt 48pt 72pt",
                plugins: [
                        "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
                        "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
                        "table contextmenu directionality emoticons template textcolor paste fullpage textcolor colorpicker textpattern"
                ],

                toolbar1: "newdocument | bold italic underline strikethrough | alignleft aligncenter alignright alignjustify | styleselect formatselect fontselect fontsizeselect",
                toolbar2: "cut copy paste | searchreplace | bullist numlist | outdent indent blockquote | undo redo | link unlink anchor code | insertdatetime preview | forecolor backcolor",
                toolbar3: "table | hr removeformat | subscript superscript | charmap emoticons | print | ltr rtl | visualchars visualblocks nonbreaking pagebreak restoredraft",

                menubar: false,
                toolbar_items_size: 'small',

                style_formats: [
                        { title: 'Bold text', inline: 'b' },
                        { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
                        { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
                        { title: 'Example 1', inline: 'span', classes: 'example1' },
                        { title: 'Example 2', inline: 'span', classes: 'example2' },
                        { title: 'Table styles' },
                        { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
                ],
                
               
                setup: function (editor) {


                    editor.on('init', function () {
                        this.execCommand("fontName", false, "arial");
                        this.execCommand("fontSize", false, "14px");
                    });
                    // Create the hub
                    var hub = $.connection.documentHub;

                    // Create a function that the hub can call to draw a text
                    hub.client.textTyped = function (text) {

                        tinyMCE.get('<%=myText.ClientID%>').setContent(text);
                      
                       
                    };

                    // Start the connection
                   
                    

                    editor.on('keyup', function (e) {
                        // Revalidate the hobbies field
                        
                        $.connection.hub.start().done(function () {

                            // Register the key event so we can allow users to type 
                            var url = window.location.href;
                            var index = url.indexOf("&docId=");
                            var uidIndex = url.indexOf("&userId=");

                            var docId = url.substring(index + 7, uidIndex);
                            var uid = url.substring(uidIndex + 8);

                           hub.server.registerChatGroup(docId, uid);

                           

                           tinyMCE.activeEditor.getContent();
                           tinyMCE.activeEditor.getContent({ format: 'raw' });
                           hub.server.typeText(tinyMCE.get('<%=myText.ClientID%>').getContent(), docId);
                           
                        });
                    });
                }
            });

            
        });
        
        
      

    </script>
     

</asp:Content>
