"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

//New message
connection.on("ReceiveMessage", function (user, message) {
    var div = document.createElement("div");
    div.className = "bloccontainer";

    var pUser = document.createElement("div");
    pUser.textContent = user.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    pUser.className = "usercontainer";

    var pMessage = document.createElement("p");
    pMessage.textContent =message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    pMessage.className = "messagecontainer";

    var date = new Date();
    var pDate = document.createElement("div");
    var min;
    if (date.getMinutes() < 10)
        min = "0" + date.getMinutes();
    else
        min = date.getMinutes();
    pDate.textContent = date.getHours() + ":" + min;
    pDate.className = "datecontainer";

    div.appendChild(pUser);
    div.appendChild(pDate); 
    div.appendChild(pMessage);    

    document.getElementById("messagesList").appendChild(div);
    var elem = document.getElementById('messagesList');
    elem.scrollTop = elem.scrollHeight;
});

//Send Button reactivated when connection is on
connection.start().then(function () {
    var pMessage = document.createElement("div");
    pMessage.textContent = "Welcome in Global Chat";
    pMessage.className = "titlecontainer";
    document.getElementById("messagesList").appendChild(pMessage);
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//Sending the message on button click
document.getElementById("sendButton").addEventListener("click", function (event) { 
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});