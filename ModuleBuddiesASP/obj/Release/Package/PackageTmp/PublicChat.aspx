<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="PublicChat.aspx.cs" Inherits="ModuleBuddiesASP.PublicChat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- DIRECT CHAT -->
    <div class="box box-primary direct-chat direct-chat-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
                <asp:Label ID="publicChatLabel" runat="server" Text="Label"></asp:Label>
            </h3>    
        </div><!-- /.box-header -->

        <div class="box-body">
            <!-- Conversations are loaded here -->
            <div id="chatBody" class="direct-chat-messages">
               
            </div><!-- /.direct-chat-messages -->
        </div><!-- /.box-body -->

        <div class="box-footer">
            <form action="#" method="post">
                <div class="input-group">
                    <input id="msgTB" type="text" name="message" placeholder="Type Message ..." class="form-control" />
                    <span class="input-group-btn">
                        <button id="sendBtn" type="button" class="btn btn-primary btn-flat">Send</button>
                    </span>
                </div>
            </form>
        </div><!-- /.box-footer-->
    </div><!--/.direct-chat -->


    <!-- JavaScript -->

    <!-- jQuery 2.1.4 -->
    <script src="AdminLTE/plugins/jQuery/jQuery-2.1.4.min.js" type="text/javascript"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="AdminLTE/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src="AdminLTE/plugins/fastclick/fastclick.min.js" type="text/javascript"></script>
 
    <!-- Signal R-->
    <script src="Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="signalr/hubs"></script>

     <script type="text/javascript">
         $(function () {
             /* FUNCTION DEFINITIONS */
             //Function to get query vars example: getQueryVariable("fid")
             function getQueryVariable(variable) {
                 var query = window.location.search.substring(1);
                 var vars = query.split("&");
                 for (var i = 0; i < vars.length; i++) {
                     var pair = vars[i].split("=");
                     if (pair[0] == variable) { return pair[1]; }
                 }
                 return (false);
             }

             //Function to get Date.Now
             function getDateTime() {
                 var currentdate = new Date();
                 var datetime = currentdate.getDate() + "/"
                                 + (currentdate.getMonth() + 1) + "/"
                                 + currentdate.getFullYear() + " @ "
                                 + currentdate.getHours() + ":"
                                 + currentdate.getMinutes() + ":"
                                 + currentdate.getSeconds();

                 return datetime;
             }
             /* PROXY TO SIGNALR HUB */
             var hubProxy = $.connection.publicChatHub;

             /* CLIENT EVENT REGISTRATIONS */

             //UPDATE CHAT FUNCTION
             hubProxy.client.updateChat = function (msg) {
                 //update CHAT
                 $("#chatBody").append(msg);

                 //Auto scroll to bottom
                 $('#chatBody').scrollTop($('#chatBody')[0].scrollHeight - $('#chatBody').height());
             };

            <% 
            ModuleBuddiesASP.HelperClasses.IvleUserInfo ivle = new ModuleBuddiesASP.HelperClasses.IvleUserInfo();
            var userID = ivle.getUserID();
            var userName = ivle.getUserName();
            %>
            var url = window.location.href;
            var gidIndex = url.indexOf("?gid=");
            var uidIndex = url.indexOf("&uid=");

            var gid = url.substring(gidIndex + 5, uidIndex);
            var uid = url.substring(uidIndex + 5);
            var userInfo = '<%=userName + ", " + userID%>';

            /* START SIGNALR */
            $.connection.hub.start().done(function () {

                $("#msgTB").val("");

                /* Retrieve Chat History */
                hubProxy.server.getChatHistory(gid, uid, userInfo);

                /* REGISTER NEW CHAT GROUP */
                hubProxy.server.registerChatGroup(uid);

                /* REGISTER BUTTON CLICK EVENT */
                $("#sendBtn").click(function () {

                    /* SERVER CALLS */
                    hubProxy.server.send($("#msgTB").val(), gid, uid, getDateTime(), userInfo);

                    /* CLEAR msgTB */
                    $("#msgTB").focus().val("");
                });

                /* MAP ENTER KEY TO SENDBUTTON */
                $(document).keypress(function (a) {
                    if (a.which == 13) {
                        a.preventDefault();
                        $("#sendBtn").click();
                    }
                });
            });



        });
    </script>
</asp:Content>
