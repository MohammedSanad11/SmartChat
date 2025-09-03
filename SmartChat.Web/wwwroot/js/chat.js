const conversationId = '@Model.Conversation.Id';
const senderId = '@Model.CurrentUserId';
const chatBox = document.getElementById("chatBox");

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub?conversationId=" + conversationId)
    .build();

connection.on("ReceiveMessage", function (msg) {
    const userId = msg.userId || msg.SenderId;
    const text = msg.text || msg.Text;
    const createdAt = new Date(msg.createdAt || msg.CreatedAt);

    const div = document.createElement("div");
    div.className = "mb-2 text-" + (userId === senderId ? "end" : "start");

    const span = document.createElement("span");
    span.className = "badge rounded-pill " + (userId === senderId ? "bg-success" : "bg-secondary") + " text-white";
    span.innerText = text;

    const timeDiv = document.createElement("div");
    timeDiv.className = "text-muted";
    timeDiv.style.fontSize = "0.8em";
    timeDiv.innerText = createdAt.toLocaleTimeString();

    div.appendChild(span);
    div.appendChild(timeDiv);
    chatBox.appendChild(div);
    chatBox.scrollTop = chatBox.scrollHeight;
});

connection.start()
    .then(() => {
        console.log("Connection started");
        connection.invoke("JoinConversation", senderId, conversationId)
            .catch(err => console.error("JoinConversation error:", err.toString()));

        document.getElementById("sendBtn").addEventListener("click", () => {
            const text = document.getElementById("messageInput").value;
            if (text.trim() !== "") {
                connection.invoke("SendMessage", senderId, conversationId, text)
                    .catch(err => console.error("SendMessage error:", err.toString()));
                document.getElementById("messageInput").value = "";
            }
        });
    })
    .catch(err => console.error("Connection failed: ", err.toString()));