
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using PRN221.Domain.Models;

using Application.Repository;
using Domain.Dto;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace RazorPage.Pages.Customers
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CustomerDto Customer { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = _mapper.Map<Customer>(Customer);

            var result = _unitOfWork.Customer.CreateAsync(customer, true);

            return RedirectToPage("./Index");
        }
    }
}
