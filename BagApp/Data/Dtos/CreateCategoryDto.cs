using System.Collections.Generic;

namespace BagApp.Data.Dtos
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
        public string Image { get; set; }
        public string SeoUrl { get; set; }
        public string SeoDescription { get; set; }
        public string Keywords { get; set; }
        public bool Stat { get; set; }
    }

    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
        public string Image { get; set; }
        public string SeoUrl { get; set; }
        public string SeoDescription { get; set; }
        public string Keywords { get; set; }
        public bool Stat { get; set; }
    }
}
