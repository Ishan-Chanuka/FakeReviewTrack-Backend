namespace EBAD_Backend.DataAccess
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ProductsCollection { get; set; } = string.Empty;
        public string PurchasesCollection { get; set; } = string.Empty;
        public string ReviewsCollection { get; set; } = string.Empty;
    }
}
