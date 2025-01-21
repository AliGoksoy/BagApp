using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using BagApp.Areas.Admin.Extensions;
using BagApp.Data.Dtos;
using BagApp.Data.Entities;
using BagApp.Data.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BagApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Super")]
    public class Dashboard : Controller
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryDto> _categoryvalidator;

        private readonly IValidator<CreateCategoryDto> _categorCreatevalidator;
        private readonly IValidator<UpdateCategoryDto> _categorUpdatevalidator;

        private readonly IValidator<SubcategoryDto> _subcategoryvalidator;
        private readonly IValidator<ProductDto> _productvalidator;
        private readonly IValidator<BannerDto> _bannervalidator;
        private readonly IValidator<ReferenceDto> _referenceValidator;
        private readonly IValidator<QuestionDto> _questionValidator;
        private readonly INotyfService _notyf;

        public Dashboard(IUow uow, IMapper mapper, IValidator<CategoryDto> categoryvalidator, IValidator<ProductDto> productvalidator, IValidator<BannerDto> bannervalidator, INotyfService notyf, IValidator<ReferenceDto> referenceValidator, IValidator<QuestionDto> questionValidator, IValidator<SubcategoryDto> subcategoryvalidator, IValidator<CreateCategoryDto> categorCreatevalidator, IValidator<UpdateCategoryDto> categorUpdatevalidator)
        {
            _uow = uow;
            _mapper = mapper;
            _categoryvalidator = categoryvalidator;
            _productvalidator = productvalidator;
            _bannervalidator = bannervalidator;
            _notyf = notyf;
            _referenceValidator = referenceValidator;
            _questionValidator = questionValidator;
            _subcategoryvalidator = subcategoryvalidator;
            _categorCreatevalidator = categorCreatevalidator;
            _categorUpdatevalidator = categorUpdatevalidator;
        }

        public async Task<IActionResult> Index()
        {
            var query = _uow.GetRepository<Category>().GetQueryable();
            var category = query.OrderBy(x => x.Id).Take(7).Include(x => x.Products).ToList();
            ViewBag.category = _mapper.Map<List<CategoryDto>>(category);



            var queryproduct = _uow.GetRepository<Product>().GetQueryable();
            var data = await queryproduct.Include(x => x.Category).Take(7).ToListAsync();
            var dto = _mapper.Map<List<ProductDto>>(data);
            ViewBag.product = dto;


            return View();
        }
        public IActionResult AddCategory()
        {
            return View(new CreateCategoryDto());
        }

        [HttpPost]

        public async Task<IActionResult> AddCategory(CreateCategoryDto category, IFormFile Image)
        {
            var valid = _categorCreatevalidator.Validate(category);
            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        category.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }
                }


                var dto = _mapper.Map<Category>(category);
                dto.SeoUrl = SeoExentsion.CreateSeoUrl(category.Name);
                await _uow.GetRepository<Category>().CreateAsync(dto);
                await _uow.SaveChangesAsync();
                _notyf.Success("Kategori Eklendi", 3);
                return RedirectToAction("GetCategory");
            }
            else
            {
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _notyf.Error(error.ErrorMessage, 3);
                }
                return View(category);
            }

        }
        public async Task<IActionResult> UpdateCategory(int Id)
        {
            var data = await _uow.GetRepository<Category>().GetByIdAsync(x => x.Id == Id);
            var dto = _mapper.Map<UpdateCategoryDto>(data);
            return View(dto);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto category, IFormFile Image)
        {

            var valid = _categorUpdatevalidator.Validate(category);
            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        category.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }

                }









                var dto = _mapper.Map<Category>(category);
                dto.SeoUrl = SeoExentsion.CreateSeoUrl(category.Name);
                _uow.GetRepository<Category>().Update(dto);
                await _uow.SaveChangesAsync();
                _notyf.Success("Kategori Güncellendi", 3);
                return RedirectToAction("GetCategory");
            }
            else
            {
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _notyf.Error(error.ErrorMessage, 3);
                }
                return View(category);
            }


        }
        public async Task<IActionResult> RemoveCategory(CategoryDto category)
        {

            var findCategory = await _uow.GetRepository<Category>().FindAsync(category.Id);

            var dto = _mapper.Map<Category>(findCategory);
            _uow.GetRepository<Category>().Remove(dto);
            await _uow.SaveChangesAsync();
            _notyf.Warning("Kategori Silindi", 3);
            return Json(new { Result = "OK" });




        }
        public async Task<IActionResult> RemoveProduct(ProductDto product)
        {

            var findCategory = await _uow.GetRepository<Product>().FindAsync(product.Id);

            var dto = _mapper.Map<Product>(findCategory);
            _uow.GetRepository<Product>().Remove(dto);
            await _uow.SaveChangesAsync();
            _notyf.Warning("Ürün Silindi", 3);
            return Json(new { Result = "OK" });




        }
        public async Task<IActionResult> GetCategory()
        {
            var query = _uow.GetRepository<Category>().GetQueryable();
            var category = await query.OrderBy(x => x.Id).Include(x => x.Products).ToListAsync();
            var dto = _mapper.Map<List<CategoryDto>>(category);
            return View(dto);
        }


        //Subcategory

        public async Task<IActionResult> AddSubcategory()
        {
            var cat = await _uow.GetRepository<Category>().GetAllAsync();
            ViewBag.category = new SelectList(cat, "Id", "Name");
            return View(new SubcategoryDto());
        }

        [HttpPost]

        public async Task<IActionResult> AddSubcategory(SubcategoryDto subcategory, IFormFile Image)
        {
            var valid = _subcategoryvalidator.Validate(subcategory);
            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        subcategory.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }
                }





                var dto = _mapper.Map<Subcategory>(subcategory);
                dto.SeoUrl = SeoExentsion.CreateSeoUrl(subcategory.Name);
                await _uow.GetRepository<Subcategory>().CreateAsync(dto);
                await _uow.SaveChangesAsync();
                _notyf.Success("Kategori Eklendi", 3);
                return RedirectToAction("GetSubcategory");
            }
            else
            {
                var cat = await _uow.GetRepository<Category>().GetAllAsync();
                ViewBag.category = new SelectList(cat, "Id", "Name");
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _notyf.Error(error.ErrorMessage, 3);
                }
                return View(subcategory);
            }

        }
        public async Task<IActionResult> UpdateSubcategory(int Id)
        {
            var cat = await _uow.GetRepository<Category>().GetAllAsync();
            ViewBag.category = new SelectList(cat, "Id", "Name");
            var data = await _uow.GetRepository<Subcategory>().GetByIdAsync(x => x.Id == Id);
            var dto = _mapper.Map<SubcategoryDto>(data);
            return View(dto);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateSubcategory(SubcategoryDto subcategory, IFormFile Image)
        {

            var valid = _subcategoryvalidator.Validate(subcategory);
            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        subcategory.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }

                }









                var dto = _mapper.Map<Subcategory>(subcategory);
                dto.SeoUrl = SeoExentsion.CreateSeoUrl(subcategory.Name);
                _uow.GetRepository<Subcategory>().Update(dto);
                await _uow.SaveChangesAsync();
                _notyf.Success("Kategori Güncellendi", 3);
                return RedirectToAction("GetSubcategory");
            }
            else
            {
                var cat = await _uow.GetRepository<Category>().GetAllAsync();
                ViewBag.category = new SelectList(cat, "Id", "Name");
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _notyf.Error(error.ErrorMessage, 3);
                }
                return View(subcategory);
            }


        }
        public async Task<IActionResult> RemoveSubcategory(SubcategoryDto subcategory)
        {

            var findCategory = await _uow.GetRepository<Subcategory>().FindAsync(subcategory.Id);

            var dto = _mapper.Map<Subcategory>(findCategory);
            _uow.GetRepository<Subcategory>().Remove(dto);
            await _uow.SaveChangesAsync();
            _notyf.Warning("Kategori Silindi", 3);
            return Json(new { Result = "OK" });




        }
        public async Task<IActionResult> GetSubcategory()
        {
            var query = _uow.GetRepository<Subcategory>().GetQueryable();
            var category = await query.OrderBy(x => x.Id).Include(x => x.Category).ToListAsync();
            var dto = _mapper.Map<List<SubcategoryDto>>(category);
            return View(dto);
        }


        [HttpGet]
        public async Task<IActionResult> GetSubCategoryId(int CategoryId)
        {

            List<Subcategory> productGroup = new();
            var query = _uow.GetRepository<Subcategory>().GetQueryable();
            productGroup = await query.Where(x => x.CategoryID == CategoryId).ToListAsync();
            productGroup.Insert(0, new Subcategory { Id = 0, Name = "Alt Kategori Seç" });
            var dto = _mapper.Map<List<SubcategoryDto>>(productGroup);
            return Json(new SelectList(dto, "Id", "Name"));

        }
        public async Task<IActionResult> AddProduct()
        {
            var cat = await _uow.GetRepository<Category>().GetAllAsync();
            ViewBag.category = new SelectList(cat, "Id", "Name");
            var scat = await _uow.GetRepository<Subcategory>().GetAllAsync();
            ViewBag.subcategory = new SelectList(scat, "Id", "Name");

            return View(new ProductDto());
        }
        [HttpPost]

        public async Task<IActionResult> AddProduct(ProductDto product, IFormFile Image)
        {
            product.SubcategoryId = 2;
            var valid = _productvalidator.Validate(product);
            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        product.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }



                }
                List<ProductMediaDto> fileDetails = new List<ProductMediaDto>();
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    var file = Request.Form.Files[i];
                    if (file != null && file.Length > 0)
                    {

                        var ext = Path.GetExtension(file.FileName);
                        var guidAdd = Guid.NewGuid();

                        var url = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + guidAdd + ext;
                        ProductMediaDto fileDetail = new ProductMediaDto()
                        {
                            Image = "~/Uploads/" + guidAdd + ext,


                        };
                        fileDetails.Add(fileDetail);
                        using (Stream fileStream = new FileStream(url, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);

                        }
                    }
                }
                product.ProductMedias = fileDetails;
                var dto = _mapper.Map<Product>(product);

                dto.SeoUrl = SeoExentsion.CreateSeoUrl(product.Name);
                await _uow.GetRepository<Product>().CreateAsync(dto);
                await _uow.SaveChangesAsync();
                _notyf.Success("Ürün Eklendi", 3);
                return RedirectToAction("GetProduct");
            }
            else
            {
                var cat = await _uow.GetRepository<Category>().GetAllAsync();
                ViewBag.category = new SelectList(cat, "Id", "Name");
                var scat = await _uow.GetRepository<Subcategory>().GetAllAsync();
                ViewBag.subcategory = new SelectList(scat, "Id", "Name");
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _notyf.Error(error.ErrorMessage, 3);
                }

                return View(product);
            }

        }
        public async Task<IActionResult> GetProduct()
        {
            var query = _uow.GetRepository<Product>().GetQueryable();
            var data = await query.Include(x => x.Category).Include(x => x.Subcategory).ToListAsync();
            var dto = _mapper.Map<List<ProductDto>>(data);
            return View(dto);
        }
        public async Task<IActionResult> UpdateProduct(int Id)
        {
            var cat = await _uow.GetRepository<Category>().GetAllAsync();
            ViewBag.category = new SelectList(cat, "Id", "Name");
            var scat = await _uow.GetRepository<Subcategory>().GetAllAsync();
            ViewBag.subcategory = new SelectList(cat, "Id", "Name");
            var data = await _uow.GetRepository<Product>().FindAsync(Id);
            var dto = _mapper.Map<ProductDto>(data);
            dto.SubcategoryId = 2;
            var dataMedia = _uow.GetRepository<ProductMedia>().GetQueryable();
            var mediaquery = await dataMedia.Where(x => x.ProductId == Id).ToListAsync();
            ViewBag.Media = _mapper.Map<List<ProductMediaDto>>(mediaquery);


            return View(dto);

        }

        [HttpPost]

        public async Task<IActionResult> UpdateProduct(ProductDto product, IFormFile Image)
        {
            product.SubcategoryId = 2;
            var valid = _productvalidator.Validate(product);

            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        product.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }



                }
                List<ProductMediaDto> fileDetails = new List<ProductMediaDto>();
                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    var file = Request.Form.Files[i];
                    if (file != null && file.Length > 0)
                    {

                        var ext = Path.GetExtension(file.FileName);
                        var guidAdd = Guid.NewGuid();

                        var url = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + guidAdd + ext;
                        ProductMediaDto fileDetail = new ProductMediaDto()
                        {
                            Image = "~/Uploads/" + guidAdd + ext,
                            ProductId = product.Id


                        };
                        fileDetails.Add(fileDetail);
                        using (Stream fileStream = new FileStream(url, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);

                            var dtoMedia = _mapper.Map<ProductMedia>(fileDetail);
                            await _uow.GetRepository<ProductMedia>().CreateAsync(dtoMedia);
                            await _uow.SaveChangesAsync();
                        }
                    }
                }

                product.ProductMedias = fileDetails;
                var dto = _mapper.Map<Product>(product);

                dto.SeoUrl = SeoExentsion.CreateSeoUrl(product.Name);
                _uow.GetRepository<Product>().Update(dto);
                await _uow.SaveChangesAsync();
                _notyf.Success("Ürün Güncellendi", 3);
                return RedirectToAction("GetProduct");
            }
            else
            {
                var cat = await _uow.GetRepository<Category>().GetAllAsync();
                ViewBag.category = new SelectList(cat, "Id", "Name");
                var scat = await _uow.GetRepository<Subcategory>().GetAllAsync();
                ViewBag.subcategory = new SelectList(scat, "Id", "Name");
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _notyf.Error(error.ErrorMessage, 3);
                }

                return View(product);
            }

        }

        public async Task<IActionResult> RemoveMedia(int Id)
        {
            var data = await _uow.GetRepository<ProductMedia>().FindAsync(Id);
            var dto = _mapper.Map<ProductMedia>(data);
            _uow.GetRepository<ProductMedia>().Remove(dto);
            await _uow.SaveChangesAsync();
            _notyf.Warning("Resim Silindi", 3);
            return Json(new { Result = "OK" });
        }


        // banner


        public IActionResult AddBanner()
        {
            return View(new BannerDto());
        }

        [HttpPost]

        public async Task<IActionResult> AddBanner(BannerDto banner, IFormFile Image)
        {
            var valid = _bannervalidator.Validate(banner);
            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        banner.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }
                }





                var dto = _mapper.Map<Banner>(banner);
                await _uow.GetRepository<Banner>().CreateAsync(dto);
                await _uow.SaveChangesAsync();
                return RedirectToAction("GetBanner");
            }
            else
            {
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(banner);
            }

        }
        public async Task<IActionResult> UpdateBanner(int Id)
        {
            var data = await _uow.GetRepository<Banner>().GetByIdAsync(x => x.Id == Id);
            var dto = _mapper.Map<BannerDto>(data);
            return View(dto);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateBanner(BannerDto banner, IFormFile Image)
        {

            var valid = _bannervalidator.Validate(banner);
            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        banner.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }

                }









                var dto = _mapper.Map<Banner>(banner);
                _uow.GetRepository<Banner>().Update(dto);
                await _uow.SaveChangesAsync();
                _notyf.Success("Banner Eklendi", 3);
                return RedirectToAction("GetBanner");
            }
            else
            {
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _notyf.Warning(error.ErrorMessage, 3);
                }
                return View(banner);
            }


        }
        public async Task<IActionResult> RemoveBanner(BannerDto banner)
        {

            var findCategory = await _uow.GetRepository<Banner>().FindAsync(banner.Id);

            var dto = _mapper.Map<Banner>(findCategory);
            _uow.GetRepository<Banner>().Remove(dto);
            await _uow.SaveChangesAsync();
            _notyf.Error("Banner Silindi", 3);
            return Json(new { Result = "OK" });




        }

        public async Task<IActionResult> GetBanner()
        {
            var data = await _uow.GetRepository<Banner>().GetAllAsync();
            var dto = _mapper.Map<List<BannerDto>>(data);
            return View(dto);
        }

        // reference
        public IActionResult AddReference()
        {
            return View(new ReferenceDto());
        }

        [HttpPost]

        public async Task<IActionResult> AddReference(ReferenceDto banner, IFormFile Image)
        {
            var valid = _referenceValidator.Validate(banner);
            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        banner.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }
                }





                var dto = _mapper.Map<Reference>(banner);
                await _uow.GetRepository<Reference>().CreateAsync(dto);
                await _uow.SaveChangesAsync();
                return RedirectToAction("GetReference");
            }
            else
            {
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(banner);
            }

        }
        public async Task<IActionResult> UpdateReference(int Id)
        {
            var data = await _uow.GetRepository<Reference>().GetByIdAsync(x => x.Id == Id);
            var dto = _mapper.Map<ReferenceDto>(data);
            return View(dto);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateReference(ReferenceDto banner, IFormFile Image)
        {

            var valid = _referenceValidator.Validate(banner);
            if (valid.IsValid)
            {
                if (Image != null)
                {
                    if (Image.ContentType == "image/jpeg")
                    {
                        var ext = Path.GetExtension(Image.FileName);
                        var GuidId = Guid.NewGuid();
                        var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                        FileStream stream = new FileStream(path, FileMode.Create);
                        Image.CopyTo(stream);
                        banner.Image = "~/uploads/" + GuidId + ext;
                    }
                    else
                    {
                        TempData["message"] = "Geçersiz dosya tipi";
                    }

                }









                var dto = _mapper.Map<Reference>(banner);
                _uow.GetRepository<Reference>().Update(dto);
                await _uow.SaveChangesAsync();
                _notyf.Success("Referans Eklendi", 3);
                return RedirectToAction("GetReference");
            }
            else
            {
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _notyf.Warning(error.ErrorMessage, 3);
                }
                return View(banner);
            }


        }
        public async Task<IActionResult> RemoveReference(ReferenceDto banner)
        {


            var findCategory = await _uow.GetRepository<Reference>().FindAsync(banner.Id);

            var dto = _mapper.Map<Reference>(findCategory);
            _uow.GetRepository<Reference>().Remove(dto);
            await _uow.SaveChangesAsync();
            _notyf.Error("Referans Silindi", 3);
            return Json(new { Result = "OK" });




        }

        public async Task<IActionResult> GetReference()
        {
            var data = await _uow.GetRepository<Reference>().GetAllAsync();
            var dto = _mapper.Map<List<ReferenceDto>>(data);
            return View(dto);
        }


        // password update
        public IActionResult PasswordUpdate()
        {
            return View(new PwdDto());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PasswordUpdate(PwdDto password)
        {
            var user = await _uow.GetRepository<User>().FindAsync(1);
            if (password.OldPassword == user.Password)
            {
                user.Password = password.NewPassword;
                var map = _mapper.Map<User>(user);
                _uow.GetRepository<User>().Update(map);
                await _uow.SaveChangesAsync();
                _notyf.Success("Şifreniz Değiştirildi", 5);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                _notyf.Error("Eski şifre yanlış", 5);
                return View(password);
            }
        }


        // Questions


        public IActionResult AddQuestion()
        {
            return View(new QuestionDto());
        }

        [HttpPost]

        public async Task<IActionResult> AddQuestion(QuestionDto banner)
        {
            var valid = _questionValidator.Validate(banner);
            if (valid.IsValid)
            {


                var dto = _mapper.Map<Question>(banner);
                await _uow.GetRepository<Question>().CreateAsync(dto);
                await _uow.SaveChangesAsync();
                return RedirectToAction("GetQuestion");
            }
            else
            {
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(banner);
            }

        }
        public async Task<IActionResult> UpdateQuestion(int Id)
        {
            var data = await _uow.GetRepository<Question>().GetByIdAsync(x => x.Id == Id);
            var dto = _mapper.Map<QuestionDto>(data);
            return View(dto);
        }

        [HttpPost]

        public async Task<IActionResult> UpdateQuestion(QuestionDto banner, IFormFile Image)
        {

            var valid = _questionValidator.Validate(banner);
            if (valid.IsValid)
            {


                var dto = _mapper.Map<Question>(banner);
                _uow.GetRepository<Question>().Update(dto);
                await _uow.SaveChangesAsync();
                _notyf.Success("Soru Eklendi", 3);
                return RedirectToAction("GetQuestion");
            }
            else
            {
                foreach (var error in valid.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    _notyf.Warning(error.ErrorMessage, 3);
                }
                return View(banner);
            }


        }
        public async Task<IActionResult> RemoveQuestion(QuestionDto banner)
        {

            var findCategory = await _uow.GetRepository<Question>().FindAsync(banner.Id);

            var dto = _mapper.Map<Question>(findCategory);
            _uow.GetRepository<Question>().Remove(dto);
            await _uow.SaveChangesAsync();
            _notyf.Error("Soru Silindi", 3);
            return Json(new { Result = "OK" });




        }

        public async Task<IActionResult> GetQuestion()
        {
            var data = await _uow.GetRepository<Question>().GetAllAsync();
            var dto = _mapper.Map<List<QuestionDto>>(data);
            return View(dto);
        }


    }


}

