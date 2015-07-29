<%@ Page Title="Buddy Chat" Language="C#" MasterPageFile="~/AdminLTE.Master" AutoEventWireup="true" CodeBehind="MainChat.aspx.cs" Inherits="ModuleBuddiesASP.MainChat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <!-- DIRECT CHAT -->
    <div class="box box-primary direct-chat direct-chat-primary">
        <div class="box-header with-border">
            <h3 class="box-title">
                <asp:Label ID="chatTitleLabel" runat="server" Text="Label"></asp:Label>
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


    <!-- MY CODES-->

    
   
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
            var hubProxy = $.connection.chatHub;

            /* CLIENT EVENT REGISTRATIONS */

            //UPDATE CHAT FUNCTION
            hubProxy.client.updateChat = function (msg) {
                //update CHAT
                $("#chatBody").append(msg);

                //Auto scroll to bottom
                $('#chatBody').scrollTop($('#chatBody')[0].scrollHeight - $('#chatBody').height());
            };

            /* START SIGNALR */
            $.connection.hub.start().done(function () {
                
                /* GET UID + FID */
                var uid = getQueryVariable("uid");
                var fid = getQueryVariable("fid");
                
                /* Retrieve Chat History */
                hubProxy.server.getChatHistory(uid, fid);

                /* REGISTER NEW CHAT GROUP */
                hubProxy.server.registerChatGroup(uid, fid);
                /* REGISTER BUTTON CLICK EVENT */
                $("#sendBtn").click(function () {
                    
                    /* SERVER CALLS */
                    hubProxy.server.send($("#msgTB").val(), uid, fid, getDateTime());

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
            //End of SignalR
        });

    </script>

    <!-- END OF MY CODES-->
</asp:Content>
