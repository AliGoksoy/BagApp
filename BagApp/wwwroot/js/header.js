// Header.js


// Function to toggle language dropdown
function toggleLanguageDropdown() {
    const languageToggle = document.getElementById('language-toggle');
    const languageDropdown = document.getElementById('language-dropdown');
    const selectedLanguage = document.getElementById('selected-language');
    const currentFlag = document.getElementById('current-flag');

    languageToggle.addEventListener('click', () => {
        languageDropdown.classList.toggle('hidden');
    });

    document.addEventListener('click', (e) => {
        if (!languageDropdown.contains(e.target) && e.target !== languageToggle && !languageToggle.contains(e.target)) {
            languageDropdown.classList.add('hidden');
        }
    });

    languageDropdown.addEventListener('click', (e) => {
        const targetLang = e.target.closest('[data-lang]');
        if (targetLang) {
            const selectedLangName = targetLang.getAttribute('data-lang');
            const selectedLangFlag = targetLang.getAttribute('data-flag');
            selectedLanguage.textContent = selectedLangName;
            currentFlag.src = selectedLangFlag;
            languageDropdown.classList.add('hidden');
            alert(`${selectedLangName} seçildi.`);
        }
    });
}

// Function to set active nav link
function setActiveNav() {
    const currentPage = document.location.pathname.split('/').pop();
    const navLinks = document.querySelectorAll('.nav-link');
    navLinks.forEach(link => {
        if (link.href.includes(currentPage)) {
            link.querySelector('i').classList.add('text-black');
            link.querySelector('span').classList.add('text-black', 'font-bold');
        } else {
            link.querySelector('i').classList.remove('text-black');
            link.querySelector('span').classList.remove('text-black', 'font-bold');
        }
    });
}

// Function to create sticky effect
function stickyHeader() {
    window.addEventListener('scroll', () => {
        const headerEl = document.querySelector('.fixed');
        if (window.scrollY > 50) {
            headerEl.classList.add('shadow-lg', 'py-1', 'bg-opacity-90');
            headerEl.classList.remove('py-2');
        } else {
            headerEl.classList.remove('shadow-lg', 'py-1', 'bg-opacity-90');
            headerEl.classList.add('py-2');
        }
    });
}



// Function to Get Social Media Colors
function getSocialColor(platform) {
    switch (platform) {
        case 'Facebook': return '#3b5998';
        case 'Instagram': return '#E1306C';
        case 'Telegram': return '#0088CC';
        case 'Trendyol': return '#FF7F00';
        default: return '#333333'; // Default color
    }
}


// Initialize header
createHeader();
stickyHeader();
toggleLanguageDropdown();
setActiveNav();

