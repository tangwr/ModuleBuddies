function btnLogin_Click(groupName) {
    //window.location.replace("maingroupchat.aspx?gid=" + groupName);
    window.location.href = "PublicChat.aspx?gid=" + groupName;
}


function addFriend(id, name) {
    window.location.href = "PublicChat.aspx?id=" + id + "&name=" + name;

}


function publicChatGroup(groupName, uid) {
    window.location.href = "PublicChat.aspx?gid=" + groupName + "&uid=" + uid;

}

function privateChatGroup(groupName, uid) {
    window.location.href = "PrivateChatGroup.aspx?gid=" + groupName + "&uid=" + uid;
}

function individualChatGroup(uid, fid) {
    window.location.href = "MainChat.aspx?uid=" + uid + "&fid=" + fid;
}

function deleteChat(type, uniqueID) {
    var dc = confirm("Are you sure you want to delete?");
    if (dc == true) {
        var url = window.location.href;
        window.location.href = "Delete.aspx?type=" + type + "&uid=" + uniqueID + "&url=" + url;
    }
}

function joinPublicChat(uniqueID, groupName) {
    var url = window.location.href;
    window.location.href = "JoinPublicChat.aspx?groupName=" + groupName + "&uid=" + uniqueID + "&url=" + url;
}