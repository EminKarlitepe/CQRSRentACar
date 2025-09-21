(() => {
    const messageInput = document.getElementById('message-input');
    const sendButton = document.getElementById('send-button');
    const chatMessages = document.getElementById('chat-messages');
    const statusBadge = document.getElementById('status-badge');
    const quickActionButtons = document.querySelectorAll('.quick-action-btn');

    let isTyping = false;

    async function sendMessage(message) {
        if (!message?.trim() || isTyping) return;

        addMessageToChat(message, 'user');
        
        if (messageInput) {
            messageInput.value = '';
        }
        
        showTypingIndicator();
        setInputState(false);

        try {
            const response = await fetch('/Chatbot/SendMessage', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    message: message,
                    userEmail: ''
                })
            });

            const data = await response.json();
            
            hideTypingIndicator();
            
            if (data.success) {
                addMessageToChat(data.response, 'bot', data.messageType);
            } else {
                addMessageToChat('Üzgünüm, bir hata oluştu. Lütfen tekrar deneyin.', 'bot', 'error');
            }
        } catch (error) {
            console.error('Error:', error);
            hideTypingIndicator();
            addMessageToChat('Bağlantı hatası. Lütfen internet bağlantınızı kontrol edin.', 'bot', 'error');
        } finally {
            setInputState(true);
        }
    }

    function addMessageToChat(message, sender, messageType = '') {
        if (!chatMessages) return;

        const messageDiv = document.createElement('div');
        messageDiv.className = `message ${sender}-message`;
        
        const now = new Date();
        const timeString = now.toLocaleTimeString('tr-TR', { 
            hour: '2-digit', 
            minute: '2-digit' 
        });

        let messageClass = '';
        if (messageType === 'CarRecommendation') {
            messageClass = 'car-recommendation';
        } else if (messageType === 'RealTimeSupport') {
            messageClass = 'real-time-support';
        }

        messageDiv.innerHTML = `
            <div class="message-content ${messageClass}">
                <div class="message-header">
                    <strong>${sender === 'user' ? 'Siz' : 'CQRS Rent A Car Asistanı'}</strong>
                    <small class="text-muted">${timeString}</small>
                </div>
                <div class="message-text">${formatMessage(message)}</div>
            </div>
        `;

        chatMessages.appendChild(messageDiv);
        chatMessages.scrollTop = chatMessages.scrollHeight;
    }

    function formatMessage(message) {
        return message.replace(/\n/g, '<br>');
    }

    function showTypingIndicator() {
        if (!chatMessages) return;

        const typingDiv = document.createElement('div');
        typingDiv.className = 'message bot-message typing-indicator show';
        typingDiv.innerHTML = `
            <div class="message-content">
                <div class="message-text">
                    <span class="typing-dots">Yazıyor</span>
                </div>
            </div>
        `;
        
        chatMessages.appendChild(typingDiv);
        chatMessages.scrollTop = chatMessages.scrollHeight;
        isTyping = true;
    }

    function hideTypingIndicator() {
        const typingIndicator = document.querySelector('.typing-indicator');
        if (typingIndicator) {
            typingIndicator.remove();
        }
        isTyping = false;
    }

    function setInputState(enabled) {
        if (messageInput) {
            messageInput.disabled = !enabled;
            if (enabled) {
                messageInput.focus();
            }
        }
        
        if (sendButton) {
            sendButton.disabled = !enabled;
        }
    }

    function initializeEventListeners() {
        if (sendButton) {
            sendButton.addEventListener('click', function() {
                const message = messageInput?.value?.trim();
                if (message) {
                    sendMessage(message);
                }
            });
        }

        if (messageInput) {
            messageInput.addEventListener('keypress', function(e) {
                if (e.key === 'Enter' && !e.shiftKey) {
                    e.preventDefault();
                    const message = this.value.trim();
                    if (message) {
                        sendMessage(message);
                    }
                }
            });
        }

        quickActionButtons.forEach(button => {
            button.addEventListener('click', function() {
                const message = this.getAttribute('data-message');
                if (message) {
                    sendMessage(message);
                }
            });
        });
    }

    function initializeStatusIndicator() {
        if (!statusBadge) return;

        setInterval(() => {
            const isOnline = navigator.onLine;
            statusBadge.textContent = isOnline ? 'Çevrimiçi' : 'Çevrimdışı';
            statusBadge.className = `badge ${isOnline ? 'badge-success' : 'badge-danger'}`;
        }, 1000);
    }

    function initializePage() {
        initializeEventListeners();
        initializeStatusIndicator();
        
        if (messageInput) {
            messageInput.focus();
        }
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initializePage);
    } else {
        initializePage();
    }
})();
