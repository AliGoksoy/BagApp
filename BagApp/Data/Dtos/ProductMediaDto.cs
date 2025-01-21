namespace BagApp.Data.Dtos
{
    public class ProductMediaDto : IDto
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        public string Image { get; set; }
    }
}