// Görsel Slider
document.addEventListener("DOMContentLoaded", () => {
    const currentSlide = document.querySelector("#current-slide");
    const hiddenImages = document.querySelectorAll("#hidden-images img");
    const slideImages = Array.from(hiddenImages).map(img => img.src);

    let currentIndex = 0;

    const changeSlide = () => {
        const tl = gsap.timeline();

        if (currentIndex % 2 === 0) {
            tl.to(currentSlide, { opacity: 0, y: -50, duration: 1, ease: "power2.inOut" });
        } else {
            tl.to(currentSlide, { opacity: 0, scale: 0.8, x: -100, duration: 1.2, ease: "power2.inOut" });
        }

        tl.add(() => {
            currentSlide.src = slideImages[currentIndex];
        });

        tl.to(currentSlide, { opacity: 1, y: 0, scale: 1, x: 0, duration: 1.2, ease: "power2.out" });

        currentIndex = (currentIndex + 1) % slideImages.length;
    };

    changeSlide();
    setInterval(changeSlide, 6000);
});



// Ürün Kartları
document.addEventListener("DOMContentLoaded", () => {
    const productCards = document.querySelectorAll(".product-card");

    // Kartların sırayla yüklenmesi
    const animateCards = () => {
        productCards.forEach((card, index) => {
            gsap.to(card, {
                opacity: 1,
                y: 0,
                duration: 0.8,
                delay: index * 0.4, // Her kart arasında gecikme
                ease: "power2.out",
                onStart: () => card.classList.add("loaded"),
            });
        });
    };

    // Animasyon ilk kez çalıştırılıyor
    animateCards();

    // 7 saniyede bir animasyonu tekrar çalıştır
    setInterval(() => {
        productCards.forEach((card) => {
            card.classList.remove("loaded");
            gsap.to(card, { opacity: 0, y: 20, duration: 0.5 });
        });

        setTimeout(() => {
            animateCards();
        }, 1000); // Önce kartları tekrar gizleyip sonra gösteriyoruz
    }, 7000);
});

// Yazı Slider (Ekranda Yazılıp Silinen Metin)
document.addEventListener("DOMContentLoaded", () => {
    const slideTexts = [
        "Modern ve Zarif Takım Elbiseler",
        "Düğün ve Özel Günler için Tasarımlar",
        "Şıklığın Adresi"
    ];
    const sliderText = document.querySelector("#slider-text");

    let currentIndex = 0;

    const typeText = (text, callback) => {
        let charIndex = 0;
        const typingInterval = setInterval(() => {
            if (charIndex < text.length) {
                sliderText.textContent += text[charIndex];
                charIndex++;
            } else {
                clearInterval(typingInterval);
                setTimeout(callback, 2000); // Yazı tamamlandıktan sonra 2 saniye bekle
            }
        }, 100); // Her harfi 100ms'de yaz
    };

    const deleteText = (callback) => {
        const deletingInterval = setInterval(() => {
            if (sliderText.textContent.length > 0) {
                sliderText.textContent = sliderText.textContent.slice(0, -1);
            } else {
                clearInterval(deletingInterval);
                setTimeout(callback, 500); // Yazı silindikten sonra 0.5 saniye bekle
            }
        }, 50); // Her harfi 50ms'de sil
    };

    const loopTexts = () => {
        setTimeout(() => {
            typeText(slideTexts[currentIndex], () => {
                deleteText(() => {
                    currentIndex = (currentIndex + 1) % slideTexts.length; // Sonraki metne geç
                    loopTexts();
                });
            });
        }, 1000); // Başlangıçta 1 saniye boş bekleme
    };

    // Başlangıçta metin animasyonunu başlat
    loopTexts();
});

document.addEventListener("DOMContentLoaded", () => {
    // GSAP Timeline
    const tl = gsap.timeline();

    // Title animasyonu
    tl.from("[data-anim='title']", {
        opacity: 0,
        y: -50, // Yukarıdan kayar
        duration: 1.2,
        ease: "power3.out",
    });

    // Description animasyonu
    tl.from("[data-anim='description']", {
        opacity: 0,
        y: 50, // Aşağıdan kayar
        duration: 1.2,
        ease: "power3.out",
    }, "-=0.8"); // Önceki animasyonun bitişine 0.8 saniye kala başlar
});

// -------------------------------------------------------------------------


document.addEventListener("DOMContentLoaded", () => {
    const discoverBtn = document.querySelector("#discover-btn");

    discoverBtn.addEventListener("click", (e) => {
        e.preventDefault(); // Varsayılan davranışı engelle

        // Hedef bölümü seç
        const targetSection = document.querySelector(discoverBtn.getAttribute("href"));

        // Sayfa kaydırma pozisyonunu hesapla
        const targetPosition = targetSection.offsetTop; // Hedef bölümün üstten uzaklığı
        const currentPosition = window.scrollY; // Şu anki kaydırma pozisyonu

        // GSAP ile kaydırma animasyonu
        gsap.to(window, {
            duration: 1.5, // Animasyon süresi (saniye)
            scrollTo: targetPosition, // Kaydırılacak hedef pozisyon
            ease: "power2.inOut" // Daha doğal bir geçiş için easing fonksiyonu
        });
    });
});
