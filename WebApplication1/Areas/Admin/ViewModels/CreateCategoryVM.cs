using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.ViewModels
{
    public class CreateCategoryVM
    {
        public CreateCategoryVM()
        {
            List<Product> products = new List<Product>();
        }
        [Required]
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public List<Product>? Products { get; set; }
    }
}
