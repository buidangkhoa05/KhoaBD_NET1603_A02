using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KhoaBDRazorPage.Pages.Auth
{
    public class SignupModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SignupModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CreateAccountrDto Account { get; set; } = null!;

        //[BindProperty]
        //public string? CustomerName { get; set; }

        //[BindProperty]
        //public string? Telephone { get; set; }

        //[BindProperty]
        //public string Email { get; set; } = null!;

        //[BindProperty]
        //public DateTime? CustomerBirthday { get; set; }

        //[BindProperty]
        //public byte? CustomerStatus { get; set; }

        //[BindProperty]
        //public string? Password { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {



                var result = await _unitOfWork.Customer.CreateAsync(_mapper.Map<Customer>(Account), true);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToPage("/Auth/Login");
        }
    }
}
