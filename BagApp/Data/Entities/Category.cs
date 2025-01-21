using System.Collections.Generic;

namespace BagApp.Data.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string English { get; set; }
        public string Arabic { get; set; }
        public List<Product> Products { get; set; }
        public string Image { get; set; }
        public bool Stat { get; set; }
 
        public List<Subcategory> Subcategories { get; set; }


        public string SeoUrl { get; set; }
        public string SeoDescription { get; set; }
        public string Keywords { get; set; }
    }
}