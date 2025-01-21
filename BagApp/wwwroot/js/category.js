let allProducts = []; // Ürünler burada olacak
let currentPage = 1;
const productsPerPage = 12;

// Ürünleri render et
function renderProducts() {
  const productList = document.getElementById('product-list');
  productList.innerHTML = ''; // Listeyi sıfırla

  const startIndex = (currentPage - 1) * productsPerPage;
  const endIndex = startIndex + productsPerPage;
  const currentProducts = allProducts.slice(startIndex, endIndex);

  // Eğer ürün yoksa, kullanıcıyı bilgilendirmek için kontrol
  if (currentProducts.length === 0) {
    productList.innerHTML = `<p class="text-center text-red-500 font-bold">Bu sayfada görüntülenecek ürün yok.</p>`;
    return;
  }

  // Ürün kartlarını oluştur ve ekle
  currentProducts.forEach((product) => {
    const productCard = document.createElement('div');
    productCard.className = 'product-card p-4 rounded-lg shadow-lg';
    productCard.innerHTML = `
      <img src="${product.image}" alt="${product.title}" class="w-full h-40 object-cover rounded-lg mb-4">
      <h3 class="text-lg font-bold">${product.title}</h3>
      <p class="text-sm text-gray-600 mb-2">${product.description}</p>
      <span class="text-sm font-semibold ${
        product.stock === 'Stokta Var' ? 'text-green-500' : 'text-red-500'
      }">${product.stock}</span>
      <button class="mt-4 bg-blue-500 text-white px-4 py-2 rounded-lg shadow hover:bg-blue-600" 
              onclick="openModal(${product.id})">
        Detay
      </button>
    `;
    productList.appendChild(productCard);
  });

  // Sayfa bilgisini güncelle
  const totalPages = Math.ceil(allProducts.length / productsPerPage);
  document.getElementById('page-info').textContent = `Sayfa ${currentPage} / ${totalPages}`;
}

// Modal açma fonksiyonu
function openModal(productId) {
  const product = allProducts.find((p) => p.id === productId);
  if (!product) return;

  document.getElementById('modal-title').textContent = product.title;
  document.getElementById('modal-description').textContent = product.description;
  document.getElementById('modal-stock').textContent = product.stock;
  document.getElementById('modal-main-image').src = product.image;

  // Variant Images
  document.getElementById('variant1').src = product.variants[0];
  document.getElementById('variant2').src = product.variants[1];
  document.getElementById('variant3').src = product.variants[2];

  // Modal göster
  document.getElementById('modal').classList.remove('hidden');
}

// Modal kapatma fonksiyonu
function closeModal() {
  document.getElementById('modal').classList.add('hidden');
}

// Variant görüntü güncelleme fonksiyonu
function updateModalImage(variantId) {
  const image = document.getElementById(variantId).src;
  document.getElementById('modal-main-image').src = image;
}

// Sayfa değiştirme
document.getElementById('prev-button').addEventListener('click', () => {
  if (currentPage > 1) {
    currentPage--;
    renderProducts();
  }
});

document.getElementById('next-button').addEventListener('click', () => {
  const totalPages = Math.ceil(allProducts.length / productsPerPage);
  if (currentPage < totalPages) {
    currentPage++;
    renderProducts();
  }
});

// Ürün listesi örneği (Mock)
allProducts = [
  // 16 ürün burada olacak, örnek:
  { id: 1, title: "Ürün 1", description: "Açıklama 1", stock: "Stokta Var", image: "image1.jpg", variants: ["image1.jpg", "image2.jpg", "image3.jpg"] },
  { id: 2, title: "Ürün 2", description: "Açıklama 2", stock: "Stokta Var", image: "image2.jpg", variants: ["image1.jpg", "image2.jpg", "image3.jpg"] },
  // Devam eden ürünler
];

// İlk render
renderProducts();
