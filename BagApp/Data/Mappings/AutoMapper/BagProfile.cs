using AutoMapper;
using BagApp.Data.Dtos;
using BagApp.Data.Entities;

namespace BagApp.Data.Mappings.AutoMapper
{
    public class BagProfile : Profile
    {
        public BagProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<Subcategory, SubcategoryDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ProductMedia, ProductMediaDto>().ReverseMap();
            CreateMap<Banner, BannerDto>().ReverseMap();
            CreateMap<ThemeSetting, ThemeDto>().ReverseMap();
            CreateMap<User, PwdDto>().ReverseMap();
            CreateMap<Reference, ReferenceDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();


        }
    }
}
