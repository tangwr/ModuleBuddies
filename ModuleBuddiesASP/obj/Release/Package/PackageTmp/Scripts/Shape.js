/// <reference path="jquery-1.8.3.js" />
/// <reference path="jquery-ui-1.9.2.js" />
/// <reference path="jquery.signalR-1.0.0-rc1.js" />

$(function () {
    // Create the hub
    var hub = $.connection.shape;
    var myUserColor = '';

    // Create a function that the hub can call to draw a line
    hub.client.lineDrawn = function (cid, fromX, fromY, toX, toY, color) {
        if (cid == hub.connection.id)
            return;

        // Enclose the drawing with begin and close path so the colors are mainatied per draw!!
        context.beginPath();
        context.strokeStyle = color;
        context.moveTo(fromX, fromY);
        context.lineTo(toX, toY);
        context.stroke();
        context.closePath();
    };

    // Create a function that the hub can call to draw a text
    hub.client.textTyped = function (cid, x, y, text, color) {
        if (cid == hub.connection.id)
            return;

        context.fillStyle = color;
        context.fillText(text, x, y);
    };

    // Create a function that the hub can call to clear the canvas
    hub.client.canvasCleared = function (cid) {
        if (cid == hub.connection.id)
            return;

        context.beginPath();
        // The canvas height and width are hard-coded - please see Index.html
        context.clearRect(0, 0, 700, 500);
        context.closePath();
    };

    // Create a function that the hub can call to update connections count
    hub.client.connectionsCountUpdated = function (c) {
        $('#count').html("<b>There are " + c + " connections!!</b>");
    };

    // Create a function that the hub can call to announce that the users got updated
    hub.client.usersUpdated = function (users) {
        $('#shapes').empty();
        
        // The users list comes from the server as userName-Color becuase the server cannot send Objects
        for (var i = 0; i < users.length; i++) {
            var userParts = users[i].split("-");
            $('#shapes').append('<li><div class="shape-' + i + '" id="' + userParts[0] + '"><p>' + userParts[0] + '</p></li>');
            $(".shape-" + i).css({ backgroundColor: userParts[1] });
        }
        
        // TODO: We do not need this code anymore ...I kept it here as a reference only
        // Select all the elements with an id that starts with 'shape-'
        // Only my shape should be allowed to run the drag function
        //var shapes = $('[id^=shape-]');
        //$.each(shapes, function (key, shape) {
        //    $('#' + thisUserShape).draggable({
        //        drag: function() {
        //            hub.server.moveShape($('#userName').val(), this.offsetLeft, this.offsetTop || 0);
        //        }
        //    });
        //});
    };
    
    // setup global variables - can be made configurable
    var startX = 0, startY = 0;
    var draw = false;
    var canvas = $('canvas')[0];
    var context = canvas.getContext("2d");
    // Can be made configurable!!!
    context.font = "20px Verdana";
    context.lineWidth = 1;

    // Start the connection
    $.connection.hub.start().done(function () {
        // Get the user name and store it 
        $('#userName').val(prompt('Enter your name please (no spaces): ', ''));

        var encodedUserName = $('#userName').val();//$('#userName').val().html();
        myUserColor = '#' + (0x1000000 + (Math.random()) * 0xffffff).toString(16).substr(1, 6);
        hub.server.registerUser($.connection.hub.id, encodedUserName, myUserColor);

        // Register the key event so we can allow users to type in the canvas
        $(document).keyup(function (evt) {
            context.fillStyle = myUserColor;
            context.fillText(String.fromCharCode(evt.keyCode), startX, startY);
            startX += context.measureText(String.fromCharCode(evt.keyCode)).width;
            
            // Notify the server
            hub.server.typeText($.connection.hub.id, startX, startY, String.fromCharCode(evt.keyCode), myUserColor);
        });

        // Register the canvas mouse events to draw
        $('canvas').mousedown(function (evt) {
            var o = $(evt.delegateTarget).offset();
            startX = evt.clientX - o.left;
            startY = evt.clientY - o.top;
            console.log(evt);
            draw = true;
            context.beginPath();
        }).mousemove(function (evt) {
            if (!draw) return;
            context.strokeStyle = myUserColor;

            var fromX = startX;
            var fromY = startY;
            context.moveTo(startX, startY);
            var o = $(evt.delegateTarget).offset();
            startX = evt.clientX - o.left;
            startY = evt.clientY - o.top;
            context.lineTo(startX, startY);
            context.stroke();

            var toX = startX;
            var toY = startY;

            // Notify the server
            hub.server.drawLine($.connection.hub.id, toX, toY, fromX, fromY, myUserColor);
        }).mouseup(function () {
            draw = false;
            context.closePath();
        });

        $('#clearButton').click(function() {
            context.beginPath();
            // The canvas height and width are hard-coded - please see Index.html
            context.clearRect(0, 0, 700, 500);
            context.closePath();

            // Notify the server
            hub.server.clearCanvas($.connection.hub.id);
        });
    });
});

