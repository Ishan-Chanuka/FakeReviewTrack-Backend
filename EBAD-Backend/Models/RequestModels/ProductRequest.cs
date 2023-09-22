namespace EBAD_Backend.Models.RequestModels
{
    public class ProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public string ProductPrice { get; set; } = string.Empty;
    }
}
