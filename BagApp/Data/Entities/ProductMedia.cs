namespace BagApp.Data.Entities
{
    public class ProductMedia : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Image { get; set; }
    }
}
