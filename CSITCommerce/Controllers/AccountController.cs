using CSITCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CSITCommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser iUser = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(iUser, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(iUser, "Admin");
                    return Redirect("/");

                }

                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleModel model)
        {
            IdentityRole role = new IdentityRole { Name = model.Name };
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return Redirect("/");
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser iuser = await _userManager.FindByNameAsync(model.UserName);
                if (iuser != null)
                {
                    bool result = await _userManager.CheckPasswordAsync(iuser, model.Password);

                    await _signInManager.SignInAsync(iuser, true);
                    return RedirectToAction("/");
                }
            }
            return View(model);
        }
    }
}
