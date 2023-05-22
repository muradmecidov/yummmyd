using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Areas.Admin.ViewModels
{
    public class ProductVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
