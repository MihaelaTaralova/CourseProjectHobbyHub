using HobbyHub.Data;
using HobbyHub.Web.Services.Interfaces;
using HobbyHub.Web.Services.Services;
using HobbyHubSystem.Web.ViewModels.Hobby;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HobbyHub.Controllers
{
    [Authorize]
    public class HobbyController : Controller
    {
        private readonly HobbyHubDbContext dbContext;
        private readonly IHobbyService hobbyService;
        private readonly IHubService hubService;
        private readonly IImageService imageService;

        public HobbyController(HobbyHubDbContext _dbContext,
            IHobbyService _hobbyService,
            IHubService _hubService,
            IImageService imageService)
        {
            this.dbContext = _dbContext;
            this.hobbyService = _hobbyService;
            this.hubService = _hubService;
            this.imageService = imageService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var hobbies = await hobbyService.GetAllHobbiesAsync();
            var hobbiesInCategoriesViewModel = hobbies.Select(h => new HobbiesInCategoryViewModel
            {
                HobbyId = h.Id,
                Name = h.Name,
                ImageUrl = h.ImageUrl,
                IsApproved = h.IsApproved
            }).ToList();

            var model = new AllHobbiesViewModel
            {
                Hobbies = hobbiesInCategoriesViewModel
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddHobby(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            AddHobbyViewModel model = new()
            {
                CategoryId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddHobby(AddHobbyViewModel hobbyViewModel)
        {
            var isAppoved = User.IsInRole("Administrator") || User.IsInRole("Moderator");

            if (ModelState.IsValid)
            {
                await hobbyService.AddHobbyAsync(hobbyViewModel, new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value), isAppoved);
                return RedirectToAction("OpenCategory", "Category", new { id = hobbyViewModel.CategoryId });
            }

            return View(hobbyViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditHobby(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

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
                CurrentImageUrl = hobby.ImageUrl
            };

            return View(hobbyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHobby(int Id, EditHobbyViewModel model, IFormFile imageFile)
        {
            if (Id <= 0)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.ImageFile != null)
                    {
                        var newImageUrl = await imageService.SaveImage(imageFile);
                        model.CurrentImageUrl = newImageUrl;
                    }

                    await hobbyService.EditHobbyAsync(Id, model, model.ImageFile);
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
            if (Id <= 0)
            {
                return BadRequest();
            }

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
            if (Id <= 0)
            {
                return BadRequest();
            }

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
            if (Id <= 0)
            {
                return BadRequest();
            }

            var hobby = await dbContext.Hobbies.FindAsync(Id);
            if (hobby == null)
            {
                return NotFound();
            }

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
