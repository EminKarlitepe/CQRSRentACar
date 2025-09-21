/**
 * Contact Form Handler
 * Handles form submission, validation, and user feedback for contact forms
 */
class ContactFormHandler {
    constructor() {
        this.form = document.getElementById('contactForm');
        this.submitBtn = document.getElementById('submitBtn');
        this.submitText = document.getElementById('submitText');
        this.loadingSpinner = document.getElementById('loadingSpinner');
        
        this.init();
    }

    init() {
        if (this.form) {
            this.form.addEventListener('submit', (e) => this.handleSubmit(e));
        }
    }

    async handleSubmit(e) {
        e.preventDefault();
        
        if (!this.form.checkValidity()) {
            e.stopPropagation();
            this.form.classList.add('was-validated');
            return;
        }

        this.setLoadingState(true);

        try {
            const formData = this.getFormData();
            
            if (!this.validateFormData(formData)) {
                this.showToast('Hata!', 'Lütfen tüm alanları doldurun.', 'error');
                return;
            }

            const response = await this.submitForm(formData);
            const result = await response.json();

            if (result.success) {
                this.resetForm();
                this.showToast('Başarılı!', result.message, 'success');
            } else {
                this.showToast('Hata!', result.message, 'error');
            }
        } catch (error) {
            console.error('Form submission error:', error);
            this.showToast('Hata!', 'Bir hata oluştu. Lütfen tekrar deneyin.', 'error');
        } finally {
            this.setLoadingState(false);
        }
    }

    getFormData() {
        return {
            Name: document.getElementById('name').value.trim(),
            Email: document.getElementById('email').value.trim(),
            Subject: document.getElementById('subject').value.trim(),
            Message: document.getElementById('message').value.trim()
        };
    }

    validateFormData(formData) {
        return formData.Name && formData.Email && formData.Subject && formData.Message;
    }

    async submitForm(formData) {
        return await fetch('/Contact/SendMessage', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });
    }

    setLoadingState(isLoading) {
        this.submitBtn.disabled = isLoading;
        this.submitText.textContent = isLoading ? 'Gönderiliyor...' : 'Mesaj Gönder';
        this.loadingSpinner.style.display = isLoading ? 'inline-block' : 'none';
    }

    resetForm() {
        this.form.reset();
        this.form.classList.remove('was-validated');
    }

    showToast(title, message, type) {
        const toast = document.createElement('div');
        toast.className = `alert alert-${type === 'success' ? 'success' : 'danger'} alert-dismissible fade show position-fixed`;
        toast.style.cssText = 'top: 20px; right: 20px; z-index: 9999; min-width: 300px;';
        toast.innerHTML = `
            <strong>${title}</strong> ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;
        
        document.body.appendChild(toast);
        
        setTimeout(() => {
            if (toast.parentNode) {
                toast.parentNode.removeChild(toast);
            }
        }, 5000);
    }
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    new ContactFormHandler();
});
