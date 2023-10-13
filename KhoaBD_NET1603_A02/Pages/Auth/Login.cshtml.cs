using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace KhoaBDRazorPage.Pages.Auth
{
    public class LoginModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public LoginDto Account { get; set; } = default!;

        public LoginModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Account.Email == AppConfig.Admin.Email && Account.Password == AppConfig.Admin.Email)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,  AppConfig.Admin.Email),
                    new Claim(ClaimTypes.Role, "admin"),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                return RedirectToPage("/Index");
            }
            else
            {
                var customer = await _unitOfWork.Customer.GetFirstOrDefault(new QueryHelper<Customer>()
                { Filter = t => t.Email == Account.Email && t.Password == Account.Password });

                if (customer == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return Page();
                }


                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,  customer.Email),
                    new Claim(ClaimTypes.Role, "customer"),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                return RedirectToPage("/Index");
            }
        }
    }
}
