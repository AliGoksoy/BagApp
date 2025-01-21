using System.Collections.Generic;

namespace BagApp.Data.Dtos
{
    public class SubcategoryDto : IDto
    {
        public string Name { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
        public bool Stat { get; set; }
        public int CategoryID { get; set; }
        public CategoryDto Category { get; set; }
        public string SeoUrl { get; set; }
        public string SeoDescription { get; set; }
        public string Keywords { get; set; }

        public string Image { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
