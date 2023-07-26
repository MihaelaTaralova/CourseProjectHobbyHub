using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHubSystem.Web.ViewModels.Hobby;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HobbyHub.Controllers
{
    public class HobbyController : Controller
    {
        private readonly HobbyHubDbContext dbContext;
        private readonly IHobbyService hobbyService;
        private readonly ICategoryService categoryService;
        private readonly IImageService imageService;
        private readonly IHubService hubService;

        public HobbyController(HobbyHubDbContext _dbContext,
            IHobbyService _hobbyService,
            ICategoryService _categoryService,
            IImageService _imageService,
            IHubService _hubService)
        {
            this.dbContext = _dbContext;
            this.hobbyService = _hobbyService;
            this.categoryService = _categoryService;
            this.imageService = _imageService;
            this.hubService = _hubService;

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

        [HttpPost]
        public async Task<IActionResult> AddHobby(AddHobbyViewModel hobbyViewModel)
        {
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
        public async Task<IActionResult> EditHobby(int Id)
        {
            var hobby = await hobbyService.GetHobbyByIdAsync(Id);

            if (hobby == null)
            {
                return NotFound();
            }

            if (hobby.IsActive == false)
            {
                return NotFound();
            }

            var hobbyViewModel = new EditHobbyViewModel
            {
                Id = Id,
                Name = hobby.Name,
                Description = hobby.Description,
                ImageUrl = hobby.ImageUrl
        };

            return View(hobbyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHobby(int Id, EditHobbyViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await hobbyService.EditHobbyAsync(Id, model);
                    return RedirectToAction("OpenHobby", "Hobby", new { id = model.Id, name = model.Name }, fragment: null);
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteHobby(int Id)
        {
            var hobby = await hobbyService.GetHobbyByIdAsync(Id);

            if (hobby == null)
            {
                return NotFound();
            }

            if (hobby.IsActive == false)
            {
                return NotFound();
            }

            var hobbyViewModel = new DeleteHobbyViewModel
            {
                Id = hobby.Id,
                Name = hobby.Name,
                ImageUrl = hobby.ImageUrl,
            };

            return View(hobbyViewModel);
        }

        [HttpPost, ActionName("DeleteHobby")]
        public async Task<IActionResult> DeleteHobbyConfirmed(int Id)
        {
            try
            {
                await hobbyService.DeleteHobbyAsync(Id);
                return RedirectToAction("OpenCategory", "Category");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> OpenHobby(int Id)
        {
            var hobby = await dbContext.Hobbies.FindAsync(Id);
            if (hobby == null)
            {
                return NotFound();
            }

            //var hub = await dbContext.Hubs.FirstOrDefaultAsync(h => h.HobbyId == Id);

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                return BadRequest();
            }

            bool isJoinedHub = await hubService.IsUserJoinedHub(hobby.HubId, userId);

            var hobbyViewModel = new HobbyViewModel
            {
                HobbyId = Id,
                Name = hobby.Name,
                Description = hobby.Description,
                ImageUrl = hobby.ImageUrl,
                HubId = hobby.HubId,
                IsJoinedHub = isJoinedHub
            };

            return View("OpenHobby", hobbyViewModel);
        }
    }
}
