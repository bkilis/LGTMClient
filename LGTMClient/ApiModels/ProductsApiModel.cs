using System.ComponentModel.DataAnnotations;

namespace LGTMClient.ApiModels
{
    public class ProductsApiModel
    {
        [Display(Name = "Product ID")]
        public int productId { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; } = null!;

        [Display(Name = "Description")]
        public string description { get; set; } = null!;

        [Display(Name = "Quantity")]
        public int quantity { get; set; }

        [Display(Name = "Price")]
        public decimal price { get; set; }

        [Display(Name = "Average Rating Value")]
        public decimal? averageRatingValue { get; set; }

        [Display(Name = "Product Category ID")]
        public int productCategoryId { get; set; }

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
