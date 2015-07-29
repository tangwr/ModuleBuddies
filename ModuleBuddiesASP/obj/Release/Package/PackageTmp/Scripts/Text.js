/// <reference path="jquery-1.8.3.js" />
/// <reference path="jquery-ui-1.9.2.js" />
/// <reference path="jquery.signalR-1.0.0-rc1.js" />

$(function () {

   
    //CKEDITOR.replace('myText');
    // Create the hub
    var hub = $.connection.documentHub;

    //var instance = CKEDITOR.instances['myText'];
    //alert(instance.id);
    //alert('ID: ' + instance.id + '\nName: ' + instance.name + '\n' + instance.getData());

    //var html_content = CKEDITOR.instances['myText'].document.$.getElementById('cke_1');
    //alert(document.getElementById('cke_1').value);
  
   


    // Create a function that the hub can call to draw a text
    hub.client.textTyped = function (cid, text) {
        
        //if (cid == hub.connection.id)
           // return;

        //document.getElementById("myText").value = text;
        document.getElementById('<%=CKEditor1.ClientID%>').value = text;
        //CKEDITOR.instances.myText.setData(text);

        // Create an element based on a native DOM element.
        //var element = new CKEDITOR.dom.element(document.getElementById('myText'));
        //element.value = text;
        //instance.setData(text);
       
    };


    // Start the connection
    $.connection.hub.start().done(function () {

        // Register the key event so we can allow users to type 
        //$(document).keyup(function (evt) {
        $(document).on('keyup',function (evt) {
            // Notify the server
            //hub.server.typeText($.connection.hub.id, $('#myText').val());
            hub.server.typeText($.connection.hub.id, $('#<%=CKEditor1.ClientID %>').val());
            //hub.server.typeText(instance.getData());
           
        });
       
    });
});

