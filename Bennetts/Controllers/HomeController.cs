using Bennetts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bennetts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new UserViewModel();
            await vm.PopulateUserVM();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await vm.SaveUser();
                    vm.Message = $"User saved successfully: {vm.NewUser.FullName}";
                    vm.NewUser = new BennettsDataModels.UserDM();
                }
                catch (Exception ex)
                {
                    vm.IsError = true;
                    vm.Message = ex.Message;
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                vm.IsError = true;
                vm.Message = "<ul>";
                errors.ForEach(x => vm.Message += $"<li>{x}</li>");
                vm.Message += "</ul>";
            }

            await vm.PopulateUserVM();

            return View("Index", vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var vm = new UserViewModel();
            vm.DeleteUser(id);

            return new EmptyResult();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}