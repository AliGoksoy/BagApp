  // Tıklama ile animasyonlu geçiş
  document.getElementById('scroll-down-link').addEventListener('click', (event) => {
    event.preventDefault(); // Default anchor behaviorını engelle
    const kategoriler = document.getElementById('kategoriler');
    kategoriler.scrollIntoView({ behavior: 'smooth' }); // Yumuşak kaydırma
  });

     // Scroll to Top Button
     const scrollTopBtn = document.getElementById("scrollTop");
     window.addEventListener("scroll", () => {
       if (window.scrollY > 200) {
         scrollTopBtn.classList.remove("hidden");
       } else {
         scrollTopBtn.classList.add("hidden");
       }
     });
     scrollTopBtn.addEventListener("click", () => {
       window.scrollTo({ top: 0, behavior: "smooth" });
     });


    
// Kampanya Modal Açma Fonksiyonu
function openCampaignModal(campaignId) {
  const modalContainer = document.getElementById("modal-container");
  const modalTitle = document.getElementById("modal-title");
  const modalImage = document.getElementById("modal-image");
  const modalDescription = document.getElementById("modal-description");
  const modalDetails = document.getElementById("modal-details");
  const modalAction = document.getElementById("modal-action");

  if (campaignId === "kampanya1") {
    modalTitle.textContent = "%30 İndirim Kampanyası";
    modalImage.src = "assets/images/bg/k1.png";
    modalDescription.textContent =
      "Seçili ürünlerde büyük indirim fırsatını kaçırmayın! Bu kampanya yalnızca belirli ürünlerde geçerli olup, 1 hafta süresince devam edecektir.";
    modalDetails.innerHTML = `
      <li>Kampanya yalnızca seçili ürünlerde geçerlidir.</li>
      <li>Stoklarla sınırlıdır.</li>
      <li>Detaylar için iletişime geçin.</li>
    `;
    modalAction.textContent = "Kampanyayı İncele";
    modalAction.onclick = () => window.location.href = "https://www.trendyol.com/magaza/looks-kids-m-248359?sst=0";
  } else if (campaignId === "kampanya2") {
    modalTitle.textContent = "1 Alana 1 Bedava Kampanyası";
    modalImage.src = "assets/images/bg/paketleme.png";
    modalDescription.textContent =
      "Tüm sezon ürünlerinde geçerli bu fırsatı kaçırmayın! Alınan ürünlerin aynı fiyat aralığında olması gerekmektedir.";
    modalDetails.innerHTML = `
      <li>Alınan ürünlerin aynı fiyatta olması gerekir.</li>
      <li>Kampanya stoklarla sınırlıdır.</li>
      <li>Detaylar için iletişime geçin.</li>
    `;
    modalAction.textContent = "Kampanyayı Gör";
    modalAction.onclick = () => window.location.href = "https://www.trendyol.com/magaza/looks-kids-m-248359?sst=0";
  } else if (campaignId === "kampanya3") {
    modalTitle.textContent = "Ücretsiz Kargo Kampanyası";
    modalImage.src = "assets/images/bg/lojıstık.png";
    modalDescription.textContent =
      "Tüm alışverişlerinizde geçerli ücretsiz kargo fırsatı! Yurtiçi teslimatlarda geçerlidir.";
    modalDetails.innerHTML = `
      <li>Yurtiçi teslimatlar için geçerlidir.</li>
      <li>Kargo süreleri farklılık gösterebilir.</li>
      <li>Detaylar için iletişime geçin.</li>
    `;
    modalAction.textContent = "Kargo Detayları";
    modalAction.onclick = () => window.location.href = "https://www.trendyol.com/magaza/looks-kids-m-248359?sst=0";
  }

  modalContainer.classList.remove("hidden");
}

// Modal Kapatma Fonksiyonu
function closeModal() {
  const modalContainer = document.getElementById("modal-container");
  modalContainer.classList.add("hidden");
}

// Modal Action Handler
function handleModalAction() {
  alert("Daha fazla bilgi almak için kampanya sayfasını ziyaret edin.");
}





// Yeni eklenenler Modal Açma Fonksiyonu
function openProductModal(product) {
  const modalContainer = document.getElementById("product-modal");
  const modalTitle = document.getElementById("product-modal-title");
  const modalImage = document.getElementById("product-modal-image");
  const modalDescription = document.getElementById("product-modal-description");
  const modalDetails = document.getElementById("product-modal-details");
  const modalPrice = document.getElementById("product-modal-price");
  const modalAction = document.getElementById("product-modal-action");

  // Dinamik içerik ekleme
  modalTitle.textContent = product.title || "Ürün Detayları";
  modalImage.src = product.image || "assets/images/default.jpg";
  modalDescription.textContent = product.description || "Bu ürün hakkında detaylı bilgiye buradan ulaşabilirsiniz.";
  modalDetails.innerHTML = product.features
    ? product.features.map((feature) => `<li>${feature}</li>`).join("")
    : `<li>Detaylı bilgi yok</li>`;
  modalPrice.textContent = product.price ? `₺${product.price}` : "₺0.00";
  modalAction.onclick = () => alert(`${product.title} sepete eklendi!`);

  // Modalı göster
  modalContainer.classList.remove("hidden");
}

// Modal Kapatma Fonksiyonu
function closeProductModal() {
  const modalContainer = document.getElementById("product-modal");
  modalContainer.classList.add("hidden");
}

// Varyasyon Görüntü Güncelleme
function updateModalImage(variantId) {
  const selectedVariant = document.getElementById(variantId);
  const modalImage = document.getElementById("product-modal-image");
  modalImage.src = selectedVariant.src;
}

// Ürün İşlem Fonksiyonu
function handleProductModalAction() {
  alert("Ürün sepete eklendi.");
}



 