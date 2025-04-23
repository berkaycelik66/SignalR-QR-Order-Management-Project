var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7202/signalrhub").build();
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var currentTime = new Date();
    var currentHour = currentTime.getHours();
    var currentMinute = currentTime.getMinutes();

    var li = document.createElement("li");
    var span = document.createElement("span");
    span.style.fontWeight = "bold";
    if (user == document.getElementById("userInput").value) {
        li.className = "d-flex justify-content-end";  // Sağ hizalama ve ortalama
    } else {
        li.className = "d-flex justify-content-start";  // Sola hizalama ve ortalama
    }
    span.textContent = user;
    li.appendChild(span);
    li.innerHTML += `: ${message} - ${currentHour}:${currentMinute}`;
    document.getElementById("messageList").appendChild(li);
});

connection.on("ReceiveClientCount", (value) => {
    $("#clientCount").text(value);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch((error) => { return error.toString() });

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", user, message).catch(error => { return error.toString() });

    document.getElementById("userInput").disabled = true;
    document.getElementById("messageInput").value = '';

    event.preventDefault();
});