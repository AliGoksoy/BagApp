using System.Collections.Generic;

namespace BagApp.Data.Dtos
{
    public class CategoryDto : IDto
    {
        public string Name { get; set; }
        public List<ProductDto> Products { get; set; }
        public string Image { get; set; }
        public bool Stat { get; set; }

        public string SeoUrl { get; set; }
        public string SeoDescription { get; set; }
        public string Keywords { get; set; }
        public List<SubcategoryDto> Subcategories { get; set; }
    }
}
