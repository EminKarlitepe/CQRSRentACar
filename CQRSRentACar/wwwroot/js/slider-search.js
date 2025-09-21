/**
 * Slider Search Form Handler
 * Handles airport search, form validation, and distance calculation for car rental search
 */
class SliderSearchHandler {
    constructor() {
        this.pickUpTimeout = null;
        this.dropOffTimeout = null;
        this.init();
    }

    init() {
        this.bindEvents();
    }

    bindEvents() {
        const pickUpCityInput = document.getElementById('pickUpCity');
        const dropOffCityInput = document.getElementById('dropOffCity');
        const searchForm = document.getElementById('searchForm');

        if (pickUpCityInput) {
            pickUpCityInput.addEventListener('input', (e) => this.handleCityInput(e, 'pickUp'));
        }

        if (dropOffCityInput) {
            dropOffCityInput.addEventListener('input', (e) => this.handleCityInput(e, 'dropOff'));
        }

        if (searchForm) {
            searchForm.addEventListener('submit', (e) => this.handleFormSubmit(e));
        }
    }

    handleCityInput(event, type) {
        const city = event.target.value.trim();
        clearTimeout(type === 'pickUp' ? this.pickUpTimeout : this.dropOffTimeout);
        
        if (city.length >= 2) {
            const timeout = setTimeout(() => {
                this.loadAirportsByCity(city, type);
            }, 500);
            
            if (type === 'pickUp') {
                this.pickUpTimeout = timeout;
            } else {
                this.dropOffTimeout = timeout;
            }
        } else {
            this.hideAirportList(type);
        }
    }

    async loadAirportsByCity(city, type) {
        try {
            const response = await fetch(`/Airport/GetAirportsByCity?city=${encodeURIComponent(city)}`);
            const data = await response.json();
            
            if (data && data.length > 0) {
                this.showAirportList(data, type);
            } else {
                this.showNoAirportMessage(type);
            }
        } catch (error) {
            console.error('Error loading airports:', error);
            this.showNoAirportMessage(type);
        }
    }

    showAirportList(airports, type) {
        const listContainer = document.getElementById(`${type}AirportList`);
        const selectElement = document.getElementById(`${type}AirportSelect`);
        
        if (!listContainer || !selectElement) return;

        listContainer.style.display = 'block';
        listContainer.innerHTML = '';
        selectElement.style.display = 'none';
        
        airports.forEach(airport => {
            const airportItem = this.createAirportItem(airport, type);
            listContainer.appendChild(airportItem);
        });
    }

    createAirportItem(airport, type) {
        const airportItem = document.createElement('div');
        airportItem.className = 'airport-item p-2 mb-1 bg-light rounded border cursor-pointer';
        airportItem.style.cursor = 'pointer';
        airportItem.style.color = '#333';
        airportItem.innerHTML = `
            <div class="fw-bold text-dark">${airport.name}</div>
            <small class="text-secondary">${airport.iata} - ${airport.city}, ${airport.country}</small>
        `;
        
        airportItem.addEventListener('click', () => this.selectAirport(airport, type));
        
        return airportItem;
    }

    showNoAirportMessage(type) {
        const listContainer = document.getElementById(`${type}AirportList`);
        if (!listContainer) return;

        listContainer.style.display = 'block';
        listContainer.innerHTML = `
            <div class="p-2 text-center text-muted">
                <i class="fas fa-exclamation-triangle"></i>
                Bu şehir için havalimanı bulunamadı.
            </div>
        `;
    }

    hideAirportList(type) {
        const listContainer = document.getElementById(`${type}AirportList`);
        const selectElement = document.getElementById(`${type}AirportSelect`);
        
        if (listContainer) listContainer.style.display = 'none';
        if (selectElement) selectElement.style.display = 'none';
    }

    selectAirport(airport, type) {
        const selectElement = document.getElementById(`${type}AirportSelect`);
        const listContainer = document.getElementById(`${type}AirportList`);
        const cityInput = document.getElementById(`${type}City`);
        
        if (!selectElement || !listContainer || !cityInput) return;

        selectElement.innerHTML = `<option value="${airport.airportId}" data-iata="${airport.iata}">${airport.name}</option>`;
        selectElement.value = airport.airportId;
        selectElement.style.display = 'block';
        
        listContainer.style.display = 'none';
        cityInput.value = airport.city;
        
        this.calculateDistance();
    }

    handleFormSubmit(event) {
        const pickUpSelect = document.getElementById('pickUpAirportSelect');
        const dropOffSelect = document.getElementById('dropOffAirportSelect');
        
        if (!this.validateAirportSelection(pickUpSelect, dropOffSelect)) {
            event.preventDefault();
            return false;
        }
        
        if (!this.validateDates()) {
            event.preventDefault();
            return false;
        }
        
        return true;
    }

    validateAirportSelection(pickUpSelect, dropOffSelect) {
        if (!pickUpSelect || pickUpSelect.style.display === 'none' || !pickUpSelect.value) {
            alert('Lütfen alış lokasyonu seçiniz.');
            return false;
        }
        
        if (!dropOffSelect || dropOffSelect.style.display === 'none' || !dropOffSelect.value) {
            alert('Lütfen teslim lokasyonu seçiniz.');
            return false;
        }
        
        return true;
    }

    validateDates() {
        const pickUpDate = document.querySelector('input[name="pickUpDate"]')?.value;
        const dropOffDate = document.querySelector('input[name="dropOffDate"]')?.value;
        
        if (!pickUpDate || !dropOffDate) {
            alert('Lütfen alış ve teslim tarihlerini seçiniz.');
            return false;
        }
        
        if (new Date(pickUpDate) >= new Date(dropOffDate)) {
            alert('Teslim tarihi alış tarihinden sonra olmalıdır.');
            return false;
        }
        
        return true;
    }

    async calculateDistance() {
        const pickUpSelect = document.getElementById('pickUpAirportSelect');
        const dropOffSelect = document.getElementById('dropOffAirportSelect');
        
        if (!pickUpSelect || !dropOffSelect) return;
        
        if (pickUpSelect.selectedOptions.length > 0 && dropOffSelect.selectedOptions.length > 0) {
            const pickUpIata = pickUpSelect.selectedOptions[0].getAttribute('data-iata');
            const dropOffIata = dropOffSelect.selectedOptions[0].getAttribute('data-iata');
            
            if (pickUpIata && dropOffIata && pickUpIata !== dropOffIata) {
                try {
                    const response = await fetch(`/Airport/GetDistanceBetweenAirports?iata1=${pickUpIata}&iata2=${dropOffIata}`);
                    const data = await response.json();
                    
                    if (data.error) {
                        console.error('Mesafe hesaplama hatası:', data.error);
                    } else {
                        this.showDistanceInfo(data);
                    }
                } catch (error) {
                    console.error('Mesafe hesaplama hatası:', error);
                }
            }
        }
    }

    showDistanceInfo(distanceData) {
        let distanceContainer = document.getElementById('distanceInfo');
        
        if (!distanceContainer) {
            distanceContainer = document.createElement('div');
            distanceContainer.id = 'distanceInfo';
            distanceContainer.className = 'alert alert-info mt-3';
            
            const formContainer = document.querySelector('form');
            if (formContainer) {
                formContainer.appendChild(distanceContainer);
            }
        }
        
        distanceContainer.innerHTML = `
            <div class="row">
                <div class="col-md-6">
                    <h6 class="mb-2"><i class="fas fa-route"></i> Mesafe Bilgisi</h6>
                    <p class="mb-1"><strong>${distanceData.airport1.name}</strong> → <strong>${distanceData.airport2.name}</strong></p>
                    <p class="mb-0"><span class="badge bg-primary">${distanceData.distanceKm} km</span> <span class="badge bg-secondary">${distanceData.distanceMiles} mil</span></p>
                </div>
                <div class="col-md-6">
                    <small class="text-muted">
                        <i class="fas fa-clock"></i> Tahmini süre: ${Math.round(distanceData.distanceKm / 80)} saat<br>
                        <i class="fas fa-car"></i> Yakıt maliyeti: ~${Math.round(distanceData.distanceKm * 0.8)} TL
                    </small>
                </div>
            </div>
        `;
        distanceContainer.style.display = 'block';
    }
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    new SliderSearchHandler();
});
