namespace LGTMClient.ApiModels
{
    public class ProductsApiModel
    {
        public int productId { get; set; }
        public string name { get; set; } = null!;
        public string description { get; set; } = null!;
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal? averageRatingValue { get; set; }

        public string? Validate()
        {
            if (description.Length > 4000)
                return "Product description cannot exceed 4000 characters.";
            if (price < 0)
                return "Product price cannot be less than 0.";
            if (quantity < 0)
                return "Quantity cannot be less than 0";
            if (averageRatingValue.HasValue && (averageRatingValue < 0 || averageRatingValue > 5))
                return "Incorrect Average Rating Value";

            return null;
        }
    }
}
