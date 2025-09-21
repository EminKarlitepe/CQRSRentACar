(() => {
    const pickUpDateInput = document.querySelector('input[name="PickUpDate"]');
    const dropOffDateInput = document.querySelector('input[name="DropOffDate"]');
    const dailyPriceElement = document.getElementById('dailyPrice');
    const distanceCostElement = document.getElementById('distanceCostAmount');
    const countrySelect = document.getElementById('countrySelect');
    const submitButton = document.getElementById('submitButton');
    const rentalForm = document.getElementById('rentalForm');
    
    const dailyPrice = dailyPriceElement ? parseFloat(dailyPriceElement.textContent.replace(/[^\d]/g, '')) : 0;
    const distanceCost = distanceCostElement ? parseFloat(distanceCostElement.textContent.replace(/[^\d]/g, '')) : 0;

    function calculatePrice() {
        if (!pickUpDateInput?.value || !dropOffDateInput?.value) {
            document.getElementById('rentalDays').textContent = '-';
            document.getElementById('totalPrice').textContent = '-';
            return;
        }
        
        const pickUpDate = new Date(pickUpDateInput.value);
        const dropOffDate = new Date(dropOffDateInput.value);
        
        if (dropOffDate > pickUpDate) {
            const timeDiff = dropOffDate.getTime() - pickUpDate.getTime();
            const daysDiff = Math.max(1, Math.ceil(timeDiff / (1000 * 3600 * 24)));
            const rentalCost = daysDiff * dailyPrice;
            const totalPrice = rentalCost + distanceCost;
            
            document.getElementById('rentalDays').textContent = daysDiff;
            document.getElementById('totalPrice').textContent = totalPrice.toLocaleString('tr-TR');
            
            if (distanceCost > 0 && distanceCostElement) {
                distanceCostElement.textContent = distanceCost.toLocaleString('tr-TR');
            }
        } else {
            document.getElementById('rentalDays').textContent = '-';
            document.getElementById('totalPrice').textContent = '-';
        }
    }
    
    function updateFuelPrice() {
        if (!countrySelect?.value) return;
        
        const currentUrl = new URL(window.location);
        currentUrl.searchParams.set('selectedCountry', countrySelect.value);
        window.location.href = currentUrl.toString();
    }

    function submitRental() {
        if (!rentalForm) return;
        
        const formData = new FormData(rentalForm);
        
        fetch(rentalForm.action, {
            method: 'POST',
            body: formData
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                const modal = new bootstrap.Modal(document.getElementById('successModal'));
                modal.show();
                
                setTimeout(() => {
                    window.location.href = '/Default/Index';
                }, 3000);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Bir hata oluştu. Lütfen tekrar deneyin.');
        });
    }

    function initializeEventListeners() {
        if (pickUpDateInput) {
            pickUpDateInput.addEventListener('change', function() {
                if (dropOffDateInput) {
                    const minDropOffDate = new Date(this.value);
                    minDropOffDate.setDate(minDropOffDate.getDate() + 1);
                    dropOffDateInput.min = minDropOffDate.toISOString().split('T')[0];
                }
                calculatePrice();
            });
        }
        
        if (dropOffDateInput) {
            dropOffDateInput.addEventListener('change', calculatePrice);
        }
        
        if (countrySelect) {
            countrySelect.addEventListener('change', updateFuelPrice);
        }
        
        if (submitButton) {
            submitButton.addEventListener('click', submitRental);
        }
    }

    function initializePage() {
        initializeEventListeners();
        
        if (pickUpDateInput?.value && dropOffDateInput?.value) {
            calculatePrice();
        }
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initializePage);
    } else {
        initializePage();
    }
})();
