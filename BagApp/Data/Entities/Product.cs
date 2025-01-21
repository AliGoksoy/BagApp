using System.Collections.Generic;

namespace BagApp.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
        public string Description { get; set; }
        public string StockNo { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public string Image { get; set; }

        public bool Home { get; set; }
        public bool Stat { get; set; }
        public List<ProductMedia> ProductMedias { get; set; }

        public string SeoUrl { get; set; }
        public string SeoDescription { get; set; }
        public string Keywords { get; set; }


    }
}
