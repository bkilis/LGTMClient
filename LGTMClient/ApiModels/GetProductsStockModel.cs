using System.ComponentModel.DataAnnotations;

namespace LGTMClient.ApiModels
{
    public class GetProductsStockResponse
    {
        public GetProductsStockResponse()
        {
            stocks = new List<ProductStock>();
        }

        [Display(Name = "Stocks")]
        public List<ProductStock> stocks { get; set; }
    }

    public class ProductStock
    {
        [Display(Name = "Product ID")]
        public int productId { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }
    }
}
