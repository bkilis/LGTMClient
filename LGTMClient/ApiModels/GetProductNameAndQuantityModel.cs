using System.ComponentModel.DataAnnotations;

namespace LGTMClient.ApiModels
{
    public class GetProductNameAndQuantityModel
    {
        [Display(Name = "Product Name")]
        public string name { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }
    }
}
