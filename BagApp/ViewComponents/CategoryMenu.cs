using AutoMapper;
using BagApp.Data.Dtos;
using BagApp.Data.Entities;
using BagApp.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BagApp.ViewComponents
{
    public class CategoryMenu : ViewComponent
    {
        IUow _uow;
        IMapper _mapper;

        public CategoryMenu(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = _uow.GetRepository<Category>().GetQueryable();
            var data = await query.Where(x => x.Stat == true).Include(x => x.Subcategories).ToListAsync();
            var dto = _mapper.Map<List<CategoryDto>>(data);

            string userLanguage = CultureInfo.CurrentUICulture.ToString();
            ViewData["lang"] = userLanguage;

            return View(dto);
        }
    }
}
