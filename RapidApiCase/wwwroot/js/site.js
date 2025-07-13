// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Hotel Search Form Enhancement
document.addEventListener('DOMContentLoaded', function() {
    // Set minimum date to today for check-in
    const checkinInput = document.querySelector('input[name="checkin"]');
    const checkoutInput = document.querySelector('input[name="checkout"]');
    
    if (checkinInput && checkoutInput) {
        const today = new Date().toISOString().split('T')[0];
        checkinInput.min = today;
        
        // Set default dates if empty
        if (!checkinInput.value) {
            checkinInput.value = today;
        }
        
        if (!checkoutInput.value) {
            const tomorrow = new Date();
            tomorrow.setDate(tomorrow.getDate() + 1);
            checkoutInput.value = tomorrow.toISOString().split('T')[0];
        }
        
        // Update checkout minimum date when checkin changes
        checkinInput.addEventListener('change', function() {
            const checkinDate = new Date(this.value);
            const nextDay = new Date(checkinDate);
            nextDay.setDate(nextDay.getDate() + 1);
            
            checkoutInput.min = nextDay.toISOString().split('T')[0];
            
            // If checkout is before or same as checkin, update it
            if (checkoutInput.value <= this.value) {
                checkoutInput.value = nextDay.toISOString().split('T')[0];
            }
        });
    }
    
    // Form validation
    const searchForm = document.querySelector('.book_now');
    if (searchForm) {
        searchForm.addEventListener('submit', function(e) {
            const city = document.querySelector('input[name="city"]');
            if (city && !city.value.trim()) {
                e.preventDefault();
                alert('Lütfen şehir adı girin.');
                city.focus();
                return false;
            }
        });
    }
    
    // Add loading animation to search button
    const searchBtns = document.querySelectorAll('.book_btn');
    searchBtns.forEach(btn => {
        btn.addEventListener('click', function() {
            if (this.type === 'submit') {
                this.innerHTML = '<i class="fa fa-spinner fa-spin"></i> Aranıyor...';
                this.disabled = true;
            }
        });
    });
});
