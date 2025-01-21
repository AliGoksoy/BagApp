using AutoMapper;
using BagApp.Data.Dtos;
using BagApp.Data.Entities;
using BagApp.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml.Linq;

namespace BagApp.Controllers
{

    public class HomeController : Controller
    {
        IUow _uow;
        IMapper _mapper;
        private readonly IHtmlLocalizer<HomeController> _localizer;


        public HomeController(IUow uow, IMapper mapper, IHtmlLocalizer<HomeController> localizer)
        {
            _uow = uow;
            _mapper = mapper;
            _localizer = localizer;
        }


        public async Task ThemeVersion()
        {
            string userLanguage = CultureInfo.CurrentUICulture.ToString();
            var theme = await _uow.GetRepository<ThemeSetting>().FindAsync(1);
            var dto = _mapper.Map<ThemeDto>(theme);

            if (userLanguage == "tr-TR")
            {
                TempData["About"] = dto.About;
                TempData["ShortAbout"] = dto.ShortAbout;
            }

            else if (userLanguage == "en-US")
            {
                TempData["About"] = dto.AboutEn;
                TempData["ShortAbout"] = dto.ShortAboutEn;
            }
            else if (userLanguage == "ar-SA")
            {
                TempData["About"] = dto.AboutAr;
                TempData["ShortAbout"] = dto.ShortAboutAr;
            }
            TempData["CompanyName"] = dto.CompanyName;
            TempData["MetaKeyword"] = dto.MetaKeyword;
            TempData["MetaDesc"] = dto.MetaDesc;
            TempData["GoogleVerify"] = dto.GoogleVerify;
            TempData["LogoUrl"] = dto.LogoUrl;
            TempData["FooterLogoUrl"] = dto.FooterLogoUrl;
            TempData["Favicon"] = dto.Favicon;
            TempData["Gsm"] = dto.Gsm;
            TempData["Phone"] = dto.Phone;
            TempData["Email"] = dto.Email;
            TempData["Email2"] = dto.Email2;
            TempData["Password"] = dto.Password;
            TempData["Adress"] = dto.Address;
            TempData["Facebook"] = dto.Facebook;
            TempData["Twitter"] = dto.Twitter;
            TempData["Youtube"] = dto.Youtube;
            TempData["Smpt"] = dto.Smpt;
            TempData["SiteUrl"] = dto.SiteUrl;
        }


        [HttpPost]

        public IActionResult CultureManagement(string culture, string returnUrl)
        {

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions { Expires = DateTime.Now.AddDays(30) });



            //return RedirectToAction(nameof(Index));
            return LocalRedirect(returnUrl);

        }
         //[Route("~/anasayfa")]
        public async Task<IActionResult> Index()
        {
            ViewData["view"] = _localizer.GetString("view");
            ViewData["featured"] = _localizer.GetString("featured");
            await ThemeVersion();

            string userLanguage = CultureInfo.CurrentUICulture.ToString();
            ViewData["lang"] = userLanguage;




            ViewData["message"] = _localizer.GetString("message");
            var data = await _uow.GetRepository<Banner>().GetAllAsync();
            ViewBag.Banner = _mapper.Map<List<BannerDto>>(data);
            //category

            #region category
            var queryCategory = _uow.GetRepository<Category>().GetQueryable();
            var queryProduct = _uow.GetRepository<Product>().GetQueryable();
            var dataCategory = new List<CategoryDto>();
            var dataProduct = new List<ProductDto>();

            switch (userLanguage)
            {
                case "tr-TR":
                    dataCategory = await queryCategory.Where(x => x.Stat == true).Select(x => new CategoryDto { Id = x.Id, Name = x.Name, Image = x.Image }).ToListAsync();
                    dataProduct = await queryProduct.Where(x => x.Stat == true && x.Home == true).OrderBy(x => x.Name).Include(x => x.Category).Include(x => x.Subcategory).Select( x=> new ProductDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image = x.Image,
                        Category = new CategoryDto { Name=x.Name},
                        Subcategory = new SubcategoryDto { Name = x.Subcategory.Name },
                        StockNo = x.StockNo,
                        SeoUrl = x.SeoUrl

                    }).ToListAsync();
                    break;

                case "en-US":
                    dataCategory = await queryCategory.Where(x => x.Stat == true).Select(x => new CategoryDto { Id = x.Id, Name = x.English, Image = x.Image }).ToListAsync();
                    dataProduct = await queryProduct.Where(x => x.Stat == true && x.Home == true).OrderBy(x => x.Name).Include(x => x.Category).Include(x => x.Subcategory).Select(x => new ProductDto
                    {
                        Id = x.Id,
                        Name = x.English,
                        Image = x.Image,
                        Category = new CategoryDto { Name = x.English },
                        Subcategory = new SubcategoryDto { Name = x.Subcategory.English },
                        StockNo = x.StockNo,
                        SeoUrl = x.SeoUrl
                    }).ToListAsync();
                    break;
                case "ar-SA":
                    dataCategory = await queryCategory.Where(x => x.Stat == true).Select(x => new CategoryDto { Id = x.Id, Name = x.Arabic, Image = x.Image }).ToListAsync();

                    dataProduct = await queryProduct.Where(x => x.Stat == true && x.Home == true).OrderBy(x => x.Name).Include(x => x.Category).Include(x => x.Subcategory).Select(x => new ProductDto
                    {
                        Id = x.Id,
                        Name = x.English,
                        Image = x.Image,
                        Category = new CategoryDto { Name = x.Arabic },
                        Subcategory = new SubcategoryDto { Name = x.Subcategory.Arabic },
                        StockNo = x.StockNo,
                        SeoUrl = x.SeoUrl
                    }).ToListAsync();
                    break;
                default:
                    break;
            }
            ViewBag.Category = _mapper.Map<List<CategoryDto>>(dataCategory);
            ViewBag.Product = _mapper.Map<List<ProductDto>>(dataProduct);
            #endregion



            //product





            var reference = await _uow.GetRepository<Reference>().GetAllAsync();
            ViewBag.Reference = _mapper.Map<List<ReferenceDto>>(reference);

            return View();
        }


        public async Task<IActionResult> Search(string keywords)
        {
            await ThemeVersion();

            var query = _uow.GetRepository<Product>().GetQueryable();
            var data = await query.Where(x => x.Name.Contains(keywords) && x.Stat == true || x.StockNo == keywords).Include(x => x.Category).ToListAsync();
            var dto = _mapper.Map<List<ProductDto>>(data);
            if (data.Count > 0)
            {
                return View(dto);
            }
            else
            {
                return View("NotFoundResult");
            }



        }

        public async Task<IActionResult> NotFoundResult()
        {
            await ThemeVersion();
            return View();
        }



        [HttpGet("~/category")]
        public async Task<IActionResult> Category()

        {

            string userLanguage = CultureInfo.CurrentUICulture.ToString();
            ViewData["lang"] = userLanguage;
            ViewData["view"] = _localizer.GetString("view");
            ViewData["product"] = _localizer.GetString("product");
            await ThemeVersion();

            var queryCategory = _uow.GetRepository<Category>().GetQueryable();
            var dataCategory = await queryCategory.Where(x => x.Stat == true).ToListAsync();
            ViewBag.Category = _mapper.Map<List<CategoryDto>>(dataCategory);


            return View();
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> GetCategoryId(string category)
        {
            //category = @Url.FriendlyUrl(category);
            await ThemeVersion();
            var query = _uow.GetRepository<Product>().GetQueryable();
            var data = await query.Where(x => x.Category.SeoUrl == category).Include(x => x.Category).Include(x => x.Subcategory).ToListAsync();
            var dto = _mapper.Map<List<ProductDto>>(data);
            var dataCategory = await _uow.GetRepository<Category>().GetByIdAsync(x => x.SeoUrl == category);
            string userLanguage = CultureInfo.CurrentUICulture.ToString();
            ViewData["lang"] = userLanguage;

            if (data.Count > 0)
            {
                if (userLanguage == "tr-TR")
                {
                    ViewBag.CategoryName = dataCategory.Name;
                }
                else if (userLanguage == "en-US")
                {
                    ViewBag.CategoryName = dataCategory.English;
                }
                else if (userLanguage == "ar-SA")
                {
                    ViewBag.CategoryName = dataCategory.Arabic;
                }


                return View(dto);
            }
            else
            {
                return View("NotFoundResult");
            }


        }







        [HttpGet("{category}/{product}", Order = 1)]

        public async Task<IActionResult> ProductDetail(string category, string product)

        {

            string userLanguage = CultureInfo.CurrentUICulture.ToString();
            ViewData["lang"] = userLanguage;

            await ThemeVersion();
            var query = _uow.GetRepository<Product>().GetQueryable();
            var data = await query.Where(x => x.SeoUrl == product).Include(x => x.ProductMedias).Include(x => x.Category).SingleOrDefaultAsync();
            var dto = _mapper.Map<ProductDto>(data);
            return View(dto);
        }

        //[HttpGet("{category}/{subcategory}")]
        //public async Task<IActionResult> GetSubcategoryId(string category, string subcategory)
        //{

        //    await ThemeVersion();
        //    var query = _uow.GetRepository<Product>().GetQueryable();
        //    var data = await query.Where(x => x.Subcategory.SeoUrl == subcategory).Include(x => x.Category).Include(x => x.Subcategory).ToListAsync();
        //    var dto = _mapper.Map<List<ProductDto>>(data);
        //    var dataCategory = await _uow.GetRepository<Subcategory>().GetByIdAsync(x => x.SeoUrl == subcategory);
        //    string userLanguage = CultureInfo.CurrentUICulture.ToString();
        //    ViewData["lang"] = userLanguage;

        //    if (data.Count > 0)
        //    {
        //        if (userLanguage == "tr-TR")
        //        {
        //            ViewBag.CategoryName = dataCategory.Name;
        //        }
        //        else if (userLanguage == "en-US")
        //        {
        //            ViewBag.CategoryName = dataCategory.English;
        //        }
        //        else if (userLanguage == "ar-SA")
        //        {
        //            ViewBag.CategoryName = dataCategory.Arabic;
        //        }


        //        return View(dto);
        //    }
        //    else
        //    {
        //        return View("NotFoundResult");
        //    }


        //}
        [HttpGet("~/kurumsal")]
        public async Task<IActionResult> About()
        {
            string userLanguage = CultureInfo.CurrentUICulture.ToString();
            ViewData["lang"] = userLanguage;
            await ThemeVersion();
            return View();
        }
        [HttpGet("~/iletisim")]
        public async Task<IActionResult> Contact()
        {
            string userLanguage = CultureInfo.CurrentUICulture.ToString();
            ViewData["lang"] = userLanguage;
            await ThemeVersion();
            return View();
        }

        [HttpGet("~/sss")]
        public async Task<IActionResult> Question()
        {
            await ThemeVersion();
            string userLanguage = CultureInfo.CurrentUICulture.ToString();
            ViewData["lang"] = userLanguage;
            var ask = await _uow.GetRepository<Question>().GetAllAsync();
            var dto = _mapper.Map<List<QuestionDto>>(ask);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> PostContact(Contact contact)
        {
            await ThemeVersion();

            string GetHtmlStat()
            {
                var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/contact.html";
                using (StreamReader reader = new StreamReader(path))
                {
                    var body = reader.ReadToEnd();
                    body = body.Replace("#Name", contact.Name);
                    body = body.Replace("#Email", contact.Email);
                    body = body.Replace("#Phone", contact.Phone);
                    body = body.Replace("#Subject", contact.Subject);

                    return body;
                }
            }

            MailMessage mail = new MailMessage();
            string from = TempData["Email"].ToString();
            mail.From = new MailAddress(from, (string)TempData["CompanyName"]);
            string toEmail = TempData["Email"].ToString();
            mail.To.Add(toEmail);
            mail.Subject = contact.Subject;
            mail.Body = GetHtmlStat();
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = (string)TempData["Smpt"];// 
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential networkCredential = new NetworkCredential(from, TempData["Password"].ToString());
            smtp.Credentials = networkCredential;
            smtp.Send(mail);



            ViewBag.Message = "Mail Başarıyla iletildi";

            return View("Contact");
        }

    }
}
