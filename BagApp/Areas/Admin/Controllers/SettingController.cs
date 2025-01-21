using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using BagApp.Data.Dtos;
using BagApp.Data.Entities;
using BagApp.Data.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BagApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Super")]
    public class SettingController : Controller
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public SettingController(IUow uow, IMapper mapper, INotyfService notyf)
        {
            _uow = uow;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> ThemeUpdate()
        {
            var theme = await _uow.GetRepository<ThemeSetting>().FindAsync(1);
            var map = _mapper.Map<ThemeDto>(theme);
            return View(map);
        }

        [HttpPost]
        public async Task<IActionResult> ThemeUpdate(ThemeDto dto, IFormFile LogoUrl, IFormFile FooterImage, IFormFile FaviconImage)
        {
            var theme = await _uow.GetRepository<ThemeSetting>().FindAsync(1);
            if (LogoUrl != null)
            {
                var ext = Path.GetExtension(LogoUrl.FileName);
                var GuidId = Guid.NewGuid();
                var path = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidId + ext;
                FileStream stream = new FileStream(path, FileMode.Create);
                LogoUrl.CopyTo(stream);
                dto.LogoUrl = "/uploads/" + GuidId + ext;
            }
            if (FooterImage != null)
            {
                //footer
                var extFooter = Path.GetExtension(FooterImage.FileName);
                var GuidIdFooter = Guid.NewGuid();
                var pathFooter = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidIdFooter + extFooter;
                FileStream streamFooter = new FileStream(pathFooter, FileMode.Create);
                FooterImage.CopyTo(streamFooter);
                dto.FooterLogoUrl = "/uploads/" + GuidIdFooter + extFooter;
            }
            if (FaviconImage != null)
            {
                //favicon
                var extFavicon = Path.GetExtension(FaviconImage.FileName);
                var GuidIdFavicon = Guid.NewGuid();
                var pathFavicon = Directory.GetCurrentDirectory() + "/wwwroot" + "/uploads/" + GuidIdFavicon + extFavicon;
                FileStream streamFavicon = new FileStream(pathFavicon, FileMode.Create);
                FaviconImage.CopyTo(streamFavicon);
                dto.Favicon = "/uploads/" + GuidIdFavicon + extFavicon;
            }







            //dto.Favicon = theme.Favicon;
            //dto.LogoUrl = theme.LogoUrl;
            //dto.FooterLogoUrl = theme.FooterLogoUrl;
            var map = _mapper.Map<ThemeSetting>(dto);
            _uow.GetRepository<ThemeSetting>().Update(map);
            await _uow.SaveChangesAsync();
            _notyf.Success("Tema ayarları kayıt edildi", 3);
            return RedirectToAction("Index", "Dashboard");
        }

    }
}
