using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Web.ViewModels.Category;
using HobbyHubSystem.Web.ViewModels.Hobby;
using Microsoft.AspNetCore.Mvc;

namespace HobbyHub.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IHobbyService hobbyService;
        private readonly IImageService imageService;

        public CategoryController(ICategoryService _categoryService,
            IHobbyService _hobbyService,
            IImageService _imageService)
        {
            this.categoryService = _categoryService;
            this.hobbyService = _hobbyService;
            this.imageService = _imageService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            var categoryViewModels = categories.Select(c => new CategoryViewModel
            {
                CategoryId = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl
            }).ToList();

            var viewModel = new AllCategoriesViewModel
            {
                Categories = categoryViewModels
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> OpenCategory(int id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var hobbies = await hobbyService.GetHobbiesByCategoryId(id);

            var hobbiesViewModel = hobbies.Select(h => new HobbiesInCategoryViewModel
            {
                HobbyId = h.Id,
                Name = h.Name,
                ImageUrl = h.ImageUrl               
            }).ToList();

            var categoryViewModel = new DisplayCategoryViewModel
            {
                CategoryId = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl,
                Hobbies = hobbiesViewModel
            };

            return View("OpenCategory", categoryViewModel);
        }
    

    [HttpGet]
    public IActionResult AddCategory()
    {
        if (!(User.IsInRole("Administrator") || User.IsInRole("Moderator")))
        {
            return Forbid(); 
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(AddCategoryViewModel categoryViewModel)
    {
        if (!(User.IsInRole("Administrator") || User.IsInRole("Moderator")))
        {
            return Forbid(); 
        }

        if (ModelState.IsValid)
        {
            await categoryService.AddCategoryAsync(categoryViewModel);
            return RedirectToAction("All");
        }

        return View(categoryViewModel);
    }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int Id)
        {
            var category = await categoryService.GetCategoryByIdAsync(Id);

            if (category == null)
            {
                return NotFound();
            }
            if (category.IsActive == false)
            {
                return NotFound();
            }

            var categoryViewModel = new EditCategoryViewModel
            {
                Id = Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };

            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int Id, EditCategoryViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {                   
                    await categoryService.EditCategory(Id, model);
                    return RedirectToAction("All");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var category = await categoryService.GetCategoryByIdAsync(Id);
            if (category == null)
            {
                return NotFound();
            }
            if (category.IsActive == false)
            {
                return NotFound();
            }
            var categoryViewModel = new DeleteCategoryViewModel
            {
                Id = Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl
            };

            return View(categoryViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            try
            {
                await categoryService.DeleteCategory(Id);
                return RedirectToAction("All");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return RedirectToAction("All", "Category");
        }

    }
}
