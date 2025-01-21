using AutoMapper;
using BagApp.Data.Dtos;
using BagApp.Data.Entities;
using BagApp.Data.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BagApp.Areas.Authorize.Controllers
{
    [Area("Authorize")]
    public class Account : Controller
    {
        IUow _uow;
        IMapper _mapper;
        IValidator<UserDto> _validator;

        public Account(IUow uow, IMapper mapper, IValidator<UserDto> validator)
        {
            _uow = uow;
            _mapper = mapper;
            _validator = validator;
        }

        public IActionResult SignIn()
        {
            return View(new UserDto());
        }
        [HttpPost]

        public async Task<IActionResult> SignIn(UserDto user)
        {
            var valid = _validator.Validate(user);

            if (valid.IsValid)
            {
                var result = await _uow.GetRepository<User>().GetByIdAsync(x => x.UserName == user.UserName && x.Password == user.Password);
                if (result != null)
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, result.UserName),
                            new Claim(ClaimTypes.Role, result.RoleName),
                        };


                    var claimsIdentity = new ClaimsIdentity(
                       claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = user.Remember,
                    };
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    HttpContext.Session.SetString("Id", result.Id.ToString());
                    HttpContext.Session.SetString("UserName", result.UserName);
                    HttpContext.Session.SetString("Role", result.RoleName);


                    return RedirectToAction("Index", "Dashboard", new { Area = "Admin" });
                }

                ViewBag.Error = "Kullanıcı adı boş geçilemez !";
                return View(user);
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("SignIn");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
