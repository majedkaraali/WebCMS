

namespace WebCMS.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using WebCMS.Models;

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;


        public AccountController(
         UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         ApplicationDbContext context) 


        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                Console.WriteLine(user);
                Console.WriteLine(user.Role);
                Console.WriteLine("---------------------------------------------");
                return user.Role switch
                {
                    "Patient" => RedirectToAction("Index", "Patient"),
                    "Doctor" => RedirectToAction("Index", "Doctor"),
                    "Admin" => RedirectToAction("Index", "Admin"),
                    "Lab man" => RedirectToAction("Index", "Lab man"),
                    _ => RedirectToAction("Login"),
                };
            }
            else
            {
                Console.WriteLine("Error");
            }
            return View();
        }

        public IActionResult Register() => View();

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


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); 
            return RedirectToAction("Login", "Account");
        }

    }

}
