document.addEventListener("DOMContentLoaded", () => {
    const cards = document.querySelectorAll(".group");
  
    // AOS Animasyonları
    cards.forEach((card, index) => {
      card.setAttribute("data-aos", index % 2 === 0 ? "fade-right" : "fade-left");
      card.setAttribute("data-aos-delay", index * 100);
      card.setAttribute("data-aos-duration", "800");
    });
  
    // AOS Init
    AOS.init({
      offset: 120, // Animasyon tetiklenme mesafesi
      duration: 600, // Animasyon süresi
      easing: 'ease-in-out', // Geçiş tipi
      once: false, // Sadece bir kere tetiklenmesi için
    });
  });
  
  // Kampanya bölümü için AOS animasyon ayarı
AOS.init({
    duration: 1000, // Animasyon süresi
    once: false, // Her scroll'da çalışsın
    easing: 'ease-in-out', // Animasyon tipi
  });
  

  
  // Geri sayımı tüm kartlara uygula
document.addEventListener("DOMContentLoaded", () => {
    const countdowns = [
      { id: "countdown-1", deadline: "2025-01-15T23:59:59" },
      { id: "countdown-2", deadline: "2025-01-10T23:59:59" },
      { id: "countdown-3", deadline: "2025-01-20T23:59:59" },
    ];
  
    const initializeCountdown = (elementId, deadline) => {
      const countdownElement = document.getElementById(elementId);
      const deadlineDate = new Date(deadline).getTime();
  
      const updateCountdown = () => {
        const now = new Date().getTime();
        const timeLeft = deadlineDate - now;
  
        if (timeLeft <= 0) {
          clearInterval(interval);
          countdownElement.textContent = "Süre Doldu!";
        } else {
          const days = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
          const hours = Math.floor((timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
          const minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
          const seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);
  
          countdownElement.textContent = `${days} Gün ${hours
            .toString()
            .padStart(2, "0")}:${minutes.toString().padStart(2, "0")}:${seconds
            .toString()
            .padStart(2, "0")}`;
        }
      };
  
      updateCountdown();
      const interval = setInterval(updateCountdown, 1000);
    };
  
    countdowns.forEach((countdown) => {
      initializeCountdown(countdown.id, countdown.deadline);
    });
  });

  
//   Yorumlar kısmı 
document.addEventListener("DOMContentLoaded", () => {
    const slider = document.querySelector("#reviews-slider");
    const cloneSlider = () => {
      const clone = slider.innerHTML;
      slider.innerHTML += clone;
    };
    cloneSlider();
  });


// Sidebar

