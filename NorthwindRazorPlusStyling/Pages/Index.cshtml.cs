using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NorthwindRazorPlusStyling.Models;

namespace NorthwindRazorPlusStyling.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NorthwindContext _dbContext;

        public IndexModel(ILogger<IndexModel> logger, NorthwindContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        // Obs! Jag behöver inte new:a fram denna list om jag inte vill det...
        // Eftersom jag fyller den och kör ToList() i OnGet() metoden
        public List<CategoryViewModel> Categories { get; set; }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CategoryName { get; set; }
            public decimal UnitPrice { get; set; }
        }

        public List<ProductViewModel> Products { get; set; }


        public void OnGet()
        {
            Categories = _dbContext.Categories.Take(6).Select(c => new CategoryViewModel
            {
                Id = c.CategoryId,
                Name = c.CategoryName,
            }).ToList();

            Products = _dbContext.Products
                .Include(c=>c.Category) /*Kan exkluderas!*/
                .Take(10) /*Vi begränsar listan till 5st. */
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