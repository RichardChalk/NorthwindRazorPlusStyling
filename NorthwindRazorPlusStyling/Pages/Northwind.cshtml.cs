using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindRazorPlusStyling.Models;

namespace NorthwindRazorPlusStyling.Pages
{
    public class NorthwindModel : PageModel
    {
        private readonly NorthwindContext _dbContext;

        public NorthwindModel(NorthwindContext dbContext)
        {
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

        public void OnGet()
        {
            Categories = _dbContext.Categories.Select(c => new CategoryViewModel
            {
                Id = c.CategoryId,
                Name = c.CategoryName,
            }).ToList();
        }
    }
}
