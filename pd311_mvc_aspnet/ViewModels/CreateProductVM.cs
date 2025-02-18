using Microsoft.AspNetCore.Mvc.Rendering;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.ViewModels
{
    public class CreateProductVM
    {
        public Product Product { get; set; } = new();
        public IEnumerable<SelectListItem> Categories { get; set; } = [];
        public IFormFile? File { get; set; }
    }
}
