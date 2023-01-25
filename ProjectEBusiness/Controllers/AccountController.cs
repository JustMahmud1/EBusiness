using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectEBusiness.DAL;
using ProjectEBusiness.DTOs.AccountDTOs;
using ProjectEBusiness.Models;

namespace ProjectEBusiness.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(AppDbContext context, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser appUser = new AppUser()
            {
                FullName = registerDto.FullName,
                UserName = registerDto.Username,
                Email = registerDto.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(appUser,registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(appUser, "Admin");
            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login (LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.Username);
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginDto.Password,true,true);
            if (!result.Succeeded)
            {
                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Your account is locked out for 5 minutes");
                    }
                    ModelState.AddModelError("", "Username or Password is incorrect");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        

        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    return Json("Ok");
        //}


    }
}
