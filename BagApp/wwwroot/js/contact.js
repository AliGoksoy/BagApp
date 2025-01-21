document.addEventListener("DOMContentLoaded", () => {
    AOS.init({
      duration: 1000, // Animasyon süresi
      offset: 150,   // Başlama noktası
    });
  });
  
  document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector("form");
    const successMessage = document.createElement("div");
  
    // Form gönderim işlemi
    form.addEventListener("submit", (event) => {
      event.preventDefault(); // Sayfanın yenilenmesini engelle
  
      // Form verilerini al
      const formData = new FormData(form);
      const formObject = Object.fromEntries(formData.entries());
  
      // Konsola yazdır (sunucuya gönderim için kullanılabilir)
      console.log("Form Verileri:", formObject);
  
      // Başarı mesajı oluştur
      successMessage.className =
        "mt-4 p-4 bg-green-100 border border-green-500 text-green-700 rounded-lg text-center";
      successMessage.textContent =
        "Mesajınız başarıyla iletilmiştir. En kısa sürede sizinle iletişime geçilecektir.";
  
      // Formun altına başarı mesajını ekle
      form.parentNode.insertBefore(successMessage, form.nextSibling);
  
      // Formu sıfırla
      form.reset();
  
      // 5 saniye sonra başarı mesajını kaldır
      setTimeout(() => {
        successMessage.remove();
      }, 5000);
    });
  });
  