using Dinner.Models;
using Dinner.Security;
using Dinner.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Dinner.Properties.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly IObserveRepository _observeRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDataProtector _protector;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICommentRepository _commentRepository;
        private readonly IReportRepository _reportRepository;
        public HomeController(IRecipeRepository recipeRepository,
                              IWebHostEnvironment hostingEnvironment,
                              IDataProtectionProvider dataProtectionProvider,
                              DataProtectionPurposeString dataProtectionPurposeString,
                               SignInManager<ApplicationUser> signInManager,
                               IRateRepository rateRepository,
                               IObserveRepository observeRepository,
                               IFavouriteRepository favouriteRepository,
                               ICommentRepository commentRepository,
                               IReportRepository reportRepository)
        {
            _recipeRepository = recipeRepository;
            _hostingEnvironment = hostingEnvironment;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeString.EmployeeIdRouteValue);
            _signInManager = signInManager;
            _rateRepository = rateRepository;
            _observeRepository = observeRepository;
            _favouriteRepository = favouriteRepository;
            _commentRepository = commentRepository;
            _reportRepository = reportRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AdvancedSearch(string? name, List<string> ingridients, List<string> tags, int? rating, int? timeMin, int? timeMax, string? authorName, int? difficultLevel, int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 9;

            ViewData["Name"] = name;
            ViewData["Ingridients"] = ingridients;
            ViewData["Tags"] = tags;
            ViewData["MinTime"] = timeMin;
            ViewData["MaxTime"] = timeMax;
            ViewData["Rating"] = rating;
            ViewData["AuthorName"] = authorName;
            ViewData["DifficultLevel"] = difficultLevel;

            AdvancedSearchViewModel model = new AdvancedSearchViewModel()
            {
                Name = name,
                Ingridients = ingridients,
                Tags = tags,
                TimeMin = timeMin,
                TimeMax = timeMax,
                Rating = rating,
                AuthorName = authorName,
                DifficultLevel = difficultLevel
            };

            if (ModelState.IsValid)
            {
                model.ResultRecipes = _recipeRepository.GetAdvancedSearchRecipes(model).ToList().ToPagedList(pageNumber, pageSize)
                    .Select(e =>
                    {
                        e.EncryptedId = _protector.Protect(e.Id.ToString());
                        return e;
                    });
            }

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Collections(string querry, int? page)
        {
            var recipes = _recipeRepository.GetAllRecipesForCollections(querry)
                .Select(e =>
                {
                    e.EncryptedId = _protector.Protect(e.Id.ToString());
                    return e;
                });
            ViewBag.CollectionsTitle = querry;

            var pageNumber = page ?? 1;
            int pageSize = 24;
            var onepageOfRecipes = recipes.ToList().ToPagedList(pageNumber, pageSize);

            CollectionsViewModel model = new CollectionsViewModel
            {
                Recipes = onepageOfRecipes,
                Query = querry
            };

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search(string query, int? page)
        {
            ViewData["MyQuery"] = query;

            var recipes = _recipeRepository.GetAllRecipesWithQuery(query)
                .Select(e =>
                {
                    e.EncryptedId = _protector.Protect(e.Id.ToString());
                    return e;
                });

            var pageNumber = page ?? 1;
            int pageSize = 24;
            var onepageOfRecipes = recipes.ToList().ToPagedList(pageNumber, pageSize);

            SearchViewModel model = new SearchViewModel
            {
                Recipes = onepageOfRecipes,
                Query = query
            };

            return View(model);
        }



        [Route("~/Home")]
        [Route("~/")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel
            {
                LastRecipes = _recipeRepository.GetNewRecipes()
                .Select(e =>
                {
                    e.EncryptedId = _protector.Protect(e.Id.ToString());
                    return e;
                }),

                LastBestRecipes = _recipeRepository.GetBestRecipesFromLastDays()
                .Select(e =>
                {
                    e.EncryptedId = _protector.Protect(e.Id.ToString());
                    return e;
                })



            };
            return View(model);
        }

        [Route("{id?}")]
        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            int recId = Convert.ToInt32(_protector.Unprotect(id));

            Recipe recipe = _recipeRepository.GetRecipe(recId);
            if (recipe == null)
            {
                Response.StatusCode = 404;
                return View("RecipeNotFound", recId);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Recipe = recipe,
                PageTitle = "page title from homecontroller",
                Ingridients = recipe.Ingridients.DeserializeStringToList(),
                Tags = recipe.Tags.DeserializeStringToList(),
                Time = recipe.Time,
                ThreeRandomOtherRecipe = _recipeRepository.GetThreeRandomRecipes().ToList(),
                ThreeRandomUserRecipe = _recipeRepository.GetThreeRandomUserRecipes(recipe.AuthorId).ToList(),
                Comments = _commentRepository.GetAllCommentsOfRecipe(recipe.Id)

            };

            foreach (var rec in homeDetailsViewModel.ThreeRandomOtherRecipe)
            {
                rec.EncryptedId = _protector.Protect(rec.Id.ToString());
            }
            foreach (var rec in homeDetailsViewModel.ThreeRandomUserRecipe)
            {
                rec.EncryptedId = _protector.Protect(rec.Id.ToString());
            }

            homeDetailsViewModel.Recipe.EncryptedId = _protector.Protect(homeDetailsViewModel.Recipe.Id.ToString());
            return View(homeDetailsViewModel);

        }
        [Route("~/UserPanelRecipes")]

        public async Task<IActionResult> UserPanelRecipes(int? page)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var model = _recipeRepository.GetAllRecipesOfUser(user.Id)
               .Select(e =>
               {
                   e.EncryptedId = _protector.Protect(e.Id.ToString());
                   return e;
               });
            var pageNumber = page ?? 1;
            int pageSize = 9;
            var onepageOfRecipes = model.ToList().ToPagedList(pageNumber, pageSize);
            return View(onepageOfRecipes);
        }
        public async Task<IActionResult> UserPanelChefs(int? page)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var model = _observeRepository.GetObservedChefsByUserId(user.Id);
            var pageNumber = page ?? 1;
            int pageSize = 48;
            var onepageOfRecipes = model.ToList().ToPagedList(pageNumber, pageSize);
            return View(onepageOfRecipes);
        }
        public async Task<IActionResult> UserPanelFavourites(int? page)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
            var model = _favouriteRepository.GetAllWithId(user.Id);

            var pageNumber = page ?? 1;
            int pageSize = 9;
            var onepageOfRecipes = model.ToList().ToPagedList(pageNumber, pageSize);
            return View(onepageOfRecipes);
        }

        [HttpGet]
        public IActionResult CreateRecipe()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRecipe(RecipeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model, "images");

                Recipe newRecipe = new Recipe
                {
                    Name = model.Name,
                    Description = model.Description,
                    PhotoPatch = uniqueFileName,
                    AuthorId = _signInManager.UserManager.GetUserId(User),
                    Ingridients = model.Ingridients.SerializeListToString(),
                    Tags = model.Tags.SerializeListToString(),
                    Time = model.Time,
                    Hint = model.Hint,
                    AddedTime = DateTime.Today,
                    DifficultLevel = model.DifficultLevel

                };

                _recipeRepository.Add(newRecipe);

                newRecipe.EncryptedId = _protector.Protect(newRecipe.Id.ToString());


                return RedirectToAction("details", new { id = newRecipe.EncryptedId });
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            int recipeId = Convert.ToInt32(_protector.Unprotect(id));
            Recipe recipe = _recipeRepository.GetRecipe(recipeId);
            RecipeEditViewModel recipeEditViewModel = new RecipeEditViewModel
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                ExistingPhotoPath = recipe.PhotoPatch,
                EncryptedId = _protector.Protect(recipe.Id.ToString()),
                AuthorId = recipe.AuthorId,
                Hint = recipe.Hint,
                Ingridients = recipe.Ingridients.DeserializeStringToList(),
                Tags = recipe.Tags.DeserializeStringToList(),
                Time = recipe.Time

            };

            return View(recipeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(RecipeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                int recipeId = Convert.ToInt32(_protector.Unprotect(model.EncryptedId));
                Recipe recipe = _recipeRepository.GetRecipe(recipeId);
                recipe.Name = model.Name;
                recipe.Description = model.Description;
                recipe.AuthorId = model.AuthorId;
                recipe.AddedTime = model.AddedTime;
                recipe.Hint = model.Hint;
                recipe.Ingridients = model.Ingridients.SerializeListToString();
                recipe.Tags = model.Tags.SerializeListToString();
                recipe.Time = model.Time;

                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images",
                            model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    recipe.PhotoPatch = ProcessUploadedFile(model, "images");
                }

                _recipeRepository.Update(recipe);

                recipe.EncryptedId = _protector.Protect(recipe.Id.ToString());

                return RedirectToAction("details", new { id = recipe.EncryptedId });
            }
            return View();
        }

        private string ProcessUploadedFile(RecipeViewModel model, string folderDirection)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, folderDirection);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        public IActionResult RateIt([FromBody] Rate _rate)
        {
            if (ModelState.IsValid)
            {
                _rate.Date = DateTime.Today;

                _rateRepository.Add(_rate);
                _rate.TempOveralRating = _rateRepository.GetOveralRating(_rate.RecipeId);

                Recipe recipe = _recipeRepository.GetRecipe(_rate.RecipeId);
                recipe.Rating = _rate.TempOveralRating;
                recipe.AmountOfRates = _rateRepository.GetAllWithId(_rate.RecipeId).Count();

                _rate.TempAmountOfRates = recipe.AmountOfRates;

                _recipeRepository.Update(recipe);

                return new JsonResult(_rate);
            }
            return new JsonResult(_rate);
        }

        public IActionResult FollowHim([FromBody] Observe observe)
        {
            if (ModelState.IsValid)
            {
                _observeRepository.Add(observe);
            }
            return new JsonResult(observe);
        }
        public IActionResult AddToFavourite([FromBody] Favourite favourite)
        {
            if (ModelState.IsValid)
            {
                _favouriteRepository.Add(favourite);
            }
            return new JsonResult(favourite);
        }
        [HttpPost]
        public IActionResult AddComment(string newComment, int recipeId, string userId, string recipeEncryptedId)
        {
            Comment comment = new Comment
            {
                CommentText = newComment,
                RecipeId = recipeId,
                UserId = userId,
                TimeAdded = DateTime.Today
            };

            _commentRepository.Add(comment);
            return RedirectToAction("Details", "Home", new { id = recipeEncryptedId });
        }
        [HttpPost]
        public IActionResult SendReport(string newReport, int recipeId, string userId, string recipeEncryptedId)
        {
            Report report= new Report
            {
                ReportText= newReport,
                RecipeId = recipeId,
                UserId = userId,
                TimeAdded = DateTime.Today
            };

            _reportRepository.Add(report);
            return RedirectToAction("SendReportConfirmation", "Home");
        }
        [HttpGet]
        public IActionResult SendReportConfirmation()
        {
            return View();
        }

        public IActionResult RandomRecipe()
        {
            Recipe recipe = _recipeRepository.GetRandomRecipe();
            if (recipe == null)
            {
                Response.StatusCode = 404;
                return View("RecipeNotFound", 0);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Recipe = recipe,
                PageTitle = "page title from homecontroller",
                Ingridients = recipe.Ingridients.DeserializeStringToList(),
                Tags = recipe.Tags.DeserializeStringToList(),
                Time = recipe.Time,
                ThreeRandomOtherRecipe = _recipeRepository.GetThreeRandomRecipes().ToList(),
                ThreeRandomUserRecipe = _recipeRepository.GetThreeRandomUserRecipes(recipe.AuthorId).ToList(),
                Comments = _commentRepository.GetAllCommentsOfRecipe(recipe.Id)

            };

            foreach (var rec in homeDetailsViewModel.ThreeRandomOtherRecipe)
            {
                rec.EncryptedId = _protector.Protect(rec.Id.ToString());
            }
            foreach (var rec in homeDetailsViewModel.ThreeRandomUserRecipe)
            {
                rec.EncryptedId = _protector.Protect(rec.Id.ToString());
            }

            homeDetailsViewModel.Recipe.EncryptedId = _protector.Protect(homeDetailsViewModel.Recipe.Id.ToString());
            return RedirectToAction("details", new { id = recipe.EncryptedId });

        }
    }
}

