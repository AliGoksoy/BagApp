document.addEventListener("DOMContentLoaded", () => {
    let currentIndex = 0; // Başlangıç indeksi
    const slides = document.querySelector(".slider-container"); // Slider konteyneri
    const slidesArray = Array.from(slides.children); // Orijinal slide elemanlarını diziye çevir
    const totalSlides = slidesArray.length; // Orijinal toplam görsel sayısı
  
    // Klonlama: İlk slide'ı en sona ekle (sonsuz sağ hareket için)
    const firstSlideClone = slidesArray[0].cloneNode(true);
    slides.appendChild(firstSlideClone);
  
    // Her 8 saniyede bir sağa doğru geçiş yap
    setInterval(() => {
      currentIndex++; // Sıradaki görsele geç
      slides.style.transform = `translateX(-${currentIndex * 100}%)`; // X ekseninde kaydır
      slides.style.transition = "transform 2s ease-in-out"; // Geçiş efekti
  
      // Eğer son görsel (klon) görüntüleniyorsa, hemen başa döndür
      if (currentIndex === totalSlides) {
        setTimeout(() => {
          slides.style.transition = "none"; // Geçişi kaldır
          currentIndex = 0; // İlk orijinal görsele dön
          slides.style.transform = `translateX(-${currentIndex * 100}%)`; // X eksenini sıfırla
        }, 2000); // Geçiş süresi kadar bekle (2s)
      }
    }, 6000); // 8 saniyede bir döngü
  });
  

// Galeri bölümü

const slider = document.getElementById('gallery-slider');
const slides = slider.children;
const totalSlides = slides.length;
let currentIndex = 0;

// Clone first slide to the end for infinite effect
const firstSlide = slides[0].cloneNode(true);
slider.appendChild(firstSlide);

function slideNext() {
  currentIndex++;
  slider.style.transition = "transform 0.7s ease-in-out";
  slider.style.transform = `translateX(-${currentIndex * 100}%)`;

  // Reset to the first slide without animation after the last slide
  if (currentIndex === totalSlides) {
    setTimeout(() => {
      slider.style.transition = "none";
      slider.style.transform = "translateX(0)";
      currentIndex = 0;
    }, 700); // Match the duration of the transition
  }
}

function slidePrev() {
  if (currentIndex === 0) {
    currentIndex = totalSlides - 1;
    slider.style.transition = "none";
    slider.style.transform = `translateX(-${currentIndex * 100}%)`;

    // Allow time for the transition reset before animating
    setTimeout(() => {
      slider.style.transition = "transform 0.7s ease-in-out";
      currentIndex--;
      slider.style.transform = `translateX(-${currentIndex * 100}%)`;
    }, 20);
  } else {
    currentIndex--;
    slider.style.transition = "transform 0.7s ease-in-out";
    slider.style.transform = `translateX(-${currentIndex * 100}%)`;
  }
}

// Auto-slide every 5 seconds
const autoSlideInterval = setInterval(slideNext, 5000);

// Navigation Buttons
document.getElementById('next-slide').addEventListener('click', () => {
  clearInterval(autoSlideInterval); // Clear the interval to avoid conflicts
  slideNext();
});

document.getElementById('prev-slide').addEventListener('click', () => {
  clearInterval(autoSlideInterval); // Clear the interval to avoid conflicts
  slidePrev();
});

