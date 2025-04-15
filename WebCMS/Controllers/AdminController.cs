using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using WebCMS.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace WebCMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View();
        }


        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<UserWithRoleViewModel>();

            foreach (var user in users)
            {
                var thisViewModel = new UserWithRoleViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.Role = string.Join(",", await _userManager.GetRolesAsync(user));
                userRolesViewModel.Add(thisViewModel);
            }

            return View(userRolesViewModel);
        }


        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string fullName, string email, string password, string role)
        {
            var user = new ApplicationUser { FullName = fullName, Email = email, UserName = email, Role = role };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);

                if (role == "Doctor")
                {
                    var doctor = new Models.Doctor
                    {
                        FullName = fullName,
                        Email = email,
                        UserId = user.Id
                    };
                    _context.Doctors.Add(doctor);
                }
                else if (role == "Patient")
                {
                    var patient = new Patient
                    {
                        FullName = fullName,
                        Email = email,
                        UserId = user.Id
                    };
                    _context.Patients.Add(patient);
                }
                else if (role == "LabWorker")
                {
                    var labWorker = new LabWorker
                    {
                        FullName = fullName,
                        Email = email,

                    };
                    _context.LabWorkers.Add(labWorker);
                }


                await _context.SaveChangesAsync();

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        public IActionResult Roles()
        {
            var roles = _roleManager.Roles.ToList();
            Console.WriteLine(roles);

            var rolesViewModel = new List<RolesViewModel>();
            foreach (var role in roles)
            {
                var thisViewModel = new RolesViewModel();
                thisViewModel.RoleId = role.Id;
                thisViewModel.RoleName = role.Name;
                rolesViewModel.Add(thisViewModel);
            }

            return View(rolesViewModel);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);

            if (roleExists)
            {
                ModelState.AddModelError("", "Role already exists.");
                return View(model);
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));

            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

    }
}
