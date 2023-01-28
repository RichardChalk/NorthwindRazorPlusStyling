using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NorthwindRazorPlusStyling.Models;
using static NorthwindRazorPlusStyling.Pages.IndexModel;

namespace NorthwindRazorPlusStyling.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly NorthwindContext _dbContext;
        public string CategoryName { get; set; }

        public CategoryModel(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductViewModel> Products { get; set; }
        public void OnGet(int catId)
        {
            CategoryName = _dbContext.Categories.First(c => c.CategoryId == catId).CategoryName;

            Products = _dbContext.Products
                .Include(p => p.Category) /*Kan exkluderas!*/
                .Where(p=>p.Category.CategoryId == catId)
                .Select(p => new ProductViewModel
            {
                Id = p.ProductId,
                Name = p.ProductName,
                CategoryName = p.Category.CategoryName,
                UnitPrice = p.UnitPrice.Value
            }).ToList();
        }
    }
}
