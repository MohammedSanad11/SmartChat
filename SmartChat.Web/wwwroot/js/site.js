document.addEventListener("DOMContentLoaded", function () {
    const pageSize = 5;
    let currentPage = 1;
    const users = window.dashboardUsers || [];
    const totalPages = Math.ceil(users.length / pageSize);
    const currentUserId = window.currentUserId || null; // تأكد أن هذا معرف بالـ HTML أو JS

    // ---- Profile toggle ----
    const avatarBtn = document.getElementById("avatar-btn");
    const profileSection = document.getElementById("profile-section");
    const closeProfileBtn = document.getElementById("close-profile-btn");

    avatarBtn?.addEventListener("click", () => {
        document.querySelectorAll('.section').forEach(s => s.classList.add("d-none"));
        profileSection?.classList.remove("d-none");
    });

    closeProfileBtn?.addEventListener("click", () => {
        profileSection?.classList.add("d-none");
    });

    document.querySelectorAll(".close-profile-btn").forEach(btn => {
        btn.addEventListener("click", () => {
            btn.closest('.section')?.classList.add("d-none");
        });
    });

    // ---- Sidebar: My Conversations toggle ----
    const myConvosToggle = document.getElementById("my-conversations-toggle");
    const myConvosList = document.getElementById("conversations-item");

    myConvosToggle?.addEventListener("click", (e) => {
        e.preventDefault();
        myConvosList?.classList.toggle("d-none");
    });

    // ---- Sidebar: Start New Chat toggle ----
    const startToggle = document.getElementById('start-new-chat-toggle');
    const startList = document.getElementById('start-new-chat-list');
    const pagination = document.getElementById('start-new-chat-pagination');

    function renderPage(page) {
        startList.innerHTML = '';
        const start = (page - 1) * pageSize;
        const end = start + pageSize;
        const pageUsers = users.slice(start, end);

        pageUsers.forEach(u => {
            const li = document.createElement('li');
            li.className = 'mb-2 p-2 rounded bg-dark-gray text-white d-flex justify-content-between align-items-center';
            li.innerHTML = `
        <span>${u.UserName}</span>
        <button class="btn btn-success btn-sm start-chat-btn">
            <i class="bi bi-chat-dots me-1"></i> Chat
        </button>`;

            const btn = li.querySelector(".start-chat-btn");
            btn.addEventListener("click", async (e) => {
                e.preventDefault(); // يمنع أي submit أو سلوك افتراضي
                btn.disabled = true; // يمنع الضغط المتكرر أثناء الطلب

                try {
                    const response = await fetch(`/User/StartConversation?agentId=${u.Id}`, {
                        method: 'POST',
                        headers: { "X-Requested-With": "XMLHttpRequest" }
                    });

                    if (!response.ok) throw new Error("Failed to create conversation");

                    const newConversation = await response.json();
                    openConversation(newConversation.id, `Chat with ${u.UserName}`, currentUserId);

                } catch (err) {
                    console.error("Error starting new conversation:", err);
                } finally {
                    btn.disabled = false;
                }
            });

            startList.appendChild(li);
        });

        // تحديث صفحة الـ pagination إذا موجودة
        document.getElementById('page-info').textContent = `Page ${currentPage} of ${totalPages}`;
        pagination?.classList.toggle('d-none', totalPages <= 1);


        document.getElementById('page-info').textContent = `Page ${currentPage} of ${totalPages}`;
        pagination?.classList.toggle('d-none', totalPages <= 1);
    }

    startToggle?.addEventListener('click', function (e) {
        e.preventDefault();
        startList?.classList.toggle('d-none');
        pagination?.classList.toggle('d-none');
        renderPage(currentPage);
    });

    document.getElementById('prev-page')?.addEventListener('click', () => {
        if (currentPage > 1) currentPage--;
        renderPage(currentPage);
    });

    document.getElementById('next-page')?.addEventListener('click', () => {
        if (currentPage < totalPages) currentPage++;
        renderPage(currentPage);
    });

    // ---- Open Conversation from Sidebar or anywhere ----
    window.openConversation = function (conversationId, title, currentUserId) {
        if (!conversationId || !currentUserId) return console.error("conversationId or CurrentUserId missing!");

        console.log("Opening conversation:", conversationId, currentUserId);


        // أخفي كل الـ sections
        document.querySelectorAll('.section').forEach(s => s.classList.add("d-none"));

        // اجلب الـ Partial View
        fetch(`/Chat/Index?conversationId=${conversationId}`, {
            headers: { "X-Requested-With": "XMLHttpRequest" }
        })
            .then(res => res.text())
            .then(html => {
                const chatSection = document.getElementById("chat-section");
                chatSection.innerHTML = html;
                chatSection.classList.remove("d-none");

                // أضف hidden inputs للقيم اللي الدالة تحتاجها
                let convInput = document.createElement('input');
                convInput.type = 'hidden';
                convInput.id = 'conversation-id';
                convInput.value = conversationId;
                chatSection.appendChild(convInput);

                let userInput = document.createElement('input');
                userInput.type = 'hidden';
                userInput.id = 'current-user-id';
                userInput.value = currentUserId;
                chatSection.appendChild(userInput);

                // حدث عنوان الصفحة
                document.getElementById("page-title").textContent = title;

                // نداء دالة SignalR بعد التأكد أن العناصر موجودة
                initializeChatEvents();

                // تحديث رابط الـ URL
                window.history.pushState({}, "", `?conversationId=${conversationId}`);
            })
            .catch(err => console.error("Failed to load chat: ", err));
    }

    // ---- SignalR Global ----
    function initializeChatEvents() {
        const chatBox = document.getElementById("chatBox");
        const messageInput = document.getElementById("messageInput");
        const sendBtn = document.getElementById("sendBtn");

        if (!chatBox || !messageInput || !sendBtn) return console.error("Some chat elements are missing!");

        const conversationId = document.getElementById("conversation-id")?.value;
        const senderId = document.getElementById("current-user-id")?.value;

        if (!conversationId || !senderId) return console.error("ConversationId or SenderId missing!");

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("/chatHub")
            .build();

        connection.on("ReceiveMessage", (msg) => {
            const isMine = msg.UserId == senderId;

            const divWrapper = document.createElement("div");
            divWrapper.className = "d-flex mb-2 " + (isMine ? "justify-content-end" : "justify-content-start");

            const divContent = document.createElement("div");
            divContent.className = "d-flex flex-column align-items-" + (isMine ? "end" : "start");

            const nameBadge = document.createElement("span");
            nameBadge.className = "badge rounded-pill " + (isMine ? "bg-success" : "bg-secondary") + " text-white mb-1";
            nameBadge.innerText = isMine ? "You" : "Sender";

            const textDiv = document.createElement("div");
            textDiv.className = "p-2 rounded " + (isMine ? "bg-success text-white" : "bg-secondary text-white");
            textDiv.style.maxWidth = "250px";
            textDiv.style.wordWrap = "break-word";
            textDiv.innerText = msg.Text;

            divContent.appendChild(nameBadge);
            divContent.appendChild(textDiv);
            divWrapper.appendChild(divContent);
            chatBox.appendChild(divWrapper);
            chatBox.scrollTop = chatBox.scrollHeight;
        });

        connection.start().then(() => {
            connection.invoke("JoinConversation", senderId, conversationId)
                .catch(err => console.error(err.toString()));

            sendBtn.addEventListener("click", () => {
                const text = messageInput.value.trim();
                if (!text) return;
                connection.invoke("SendMessage", senderId, conversationId, text)
                    .catch(err => console.error(err.toString()));
                messageInput.value = "";
            });
        }).catch(err => console.error("Connection failed:", err.toString()));
    }

    // ---- Auto open conversation if conversationId in URL ----
    const urlParams = new URLSearchParams(window.location.search);
    const conversationIdFromUrl = urlParams.get('conversationId');
    if (conversationIdFromUrl) {
        openConversation(conversationIdFromUrl, 'Chat', currentUserId);
    }
});