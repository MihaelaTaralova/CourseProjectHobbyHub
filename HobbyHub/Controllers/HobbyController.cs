using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Web.ViewModels.Category;
using HobbyHubSystem.Web.ViewModels.Hobby;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace HobbyHub.Controllers
{
    public class HobbyController : Controller
    {
        private readonly HobbyHubDbContext dbContext;
        private readonly IHobbyService hobbyService;
        private readonly ICategoryService categoryService;
        private readonly IImageService imageService;

        public HobbyController(HobbyHubDbContext _dbContext,
            IHobbyService _hobbyService,
            ICategoryService _categoryService,
            IImageService _imageService)
        {
            this.dbContext = _dbContext;
            this.hobbyService = _hobbyService;
            this.categoryService = _categoryService;
            this.imageService = _imageService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var hobbies = await hobbyService.GetAllHobbiesAsync();
            var hobbiesInCategoriesViewModel = hobbies.Select(h => new HobbiesInCategoryViewModel
            {
                HobbyId = h.Id,
                Name = h.Name,
                ImageUrl = h.ImageUrl
            }).ToList();

            var model = new AllHobbiesViewModel
            {
                Hobbies = hobbiesInCategoriesViewModel
            };

            return View(model);
        }

        [HttpGet]
        public  IActionResult AddHobby(int id)
        {
            AddHobbyViewModel model = new()
            {
                CategoryId = id
            };
            return View(model);
        }

        public async Task<IActionResult> AddHobby(AddHobbyViewModel hobbyViewModel)
        {
            //    if (hobbyViewModel.ImageUrl == null || hobbyViewModel.ImageUrl.Length == 0)
            //    {
            //        ModelState.AddModelError("ImageUrl", "Please select a valid image file.");
            //    }
            //    else if (!imageService.IsValidImage(hobbyViewModel.ImageUrl))
            //    {
            //        ModelState.AddModelError("ImageUrl", "Invalid image format. Only JPG, JPEG, PNG, and GIF are supported.");
            //    }

            //    if (User.IsInRole("Administrator") || User.IsInRole("Moderator"))
            //    {
            //        hobbyViewModel.IsApproved = true;
            //    }
            //    else
            //    {
            //        hobbyViewModel.IsApproved = false;
            //    }

            //    hobbyViewModel.CreatorId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            //    if (ModelState.IsValid)
            //    {
            //        // save the image and get the url
            //        var imageurl = await imageService.SaveImage(hobbyViewModel.ImageUrl);

            //        // set the imageurl property of the viewmodel to the saved url
            //        hobbyViewModel.SavedImageUrl = imageurl;

            //        // Get the category from the database using the category name
            //        var category = await categoryService.GetCategoryByNameAsync(hobbyViewModel.Name);

            //        if (category != null)
            //        {
            //            // Set the CategoryId property of the ViewModel to the category id
            //            hobbyViewModel.CategoryId = category.Id;

            //            // Now you can add the hobby to the database
            //            await hobbyService.AddHobbyAsync(hobbyViewModel);
            //            return RedirectToAction("All");
            //        }
            //        else
            //        {
            //            // Handle the case when the category does not exist
            //            ModelState.AddModelError("", "The specified category does not exist.");
            //        }
            //    }

            //    return View(hobbyViewModel);

            if (!(User.IsInRole("Administrator") || User.IsInRole("Moderator")))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                await hobbyService.AddHobbyAsync(hobbyViewModel, new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                return RedirectToAction("OpenCategory", "Category", new { id =  hobbyViewModel.CategoryId });
            }

            return View(hobbyViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditHobbyAsync(int hobbyId)
        {
            var hobby = await dbContext.Hobbies.FindAsync(hobbyId);
            if (hobby == null)
            {
                return NotFound();
            }

            var hobbyViewModel = new HobbyViewModel
            {
                HobbyId = hobbyId,
                Name = hobby.Name,
                Description = hobby.Description,
                ImageUrl = hobby.ImageUrl,
                CategoryId = hobby.CategoryId,
                HubId = hobby.HubId
            };

            return View(hobbyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditHobbyAsync(int hobbyId, AddHobbyViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await hobbyService.EditHobbyAsync(hobbyId, model);
                    return RedirectToAction("All");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteHobbyAsync(int hobbyId)
        {
            try
            {
                await hobbyService.DeleteHobbyAsync(hobbyId);
                return RedirectToAction("All");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> OpenHobby(int hobbyId)
        {
            var hobby = await dbContext.Hobbies.FindAsync(hobbyId);
            if (hobby == null)
            {
                return NotFound();
            }

            var hobbyViewModel = new HobbyViewModel
            {
                HobbyId = hobbyId,
                Name = hobby.Name,
                Description = hobby.Description,
                ImageUrl = hobby.ImageUrl,
                HubId = hobby.HubId
            };

            return View("OpenHobby", hobbyViewModel);
        }
    }
}
