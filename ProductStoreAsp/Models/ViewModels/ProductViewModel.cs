namespace ProductStoreAsp.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Price { get; set; }

        public IFormFile Image { get; set; } = null!;

        public int CategoryId { get; set; }

    }
}
