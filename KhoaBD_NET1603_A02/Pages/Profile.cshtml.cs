using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity.Infrastructure;

namespace KhoaBDRazorPage.Pages
{
    [Authorize(Roles = "customer")]
    public class ProfileModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var customer = await _unitOfWork.Customer.GetFirstOrDefault(new QueryHelper<Customer>() { Filter = t => t.Email == User.Identity.Name });

            if (customer == null)
            {
                return NotFound();
            }

            Customer = customer;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var customer = await _unitOfWork.Customer.GetFirstOrDefault(new QueryHelper<Customer>() { Filter = t => t.Email == User.Identity.Name });

                if (customer.CustomerId != Customer.CustomerId)
                {
                    return NotFound();
                }

                await _unitOfWork.Customer.UpdateAsync(Customer, true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CustomerExists(Customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> CustomerExists(int id)
        {
            return (await _unitOfWork.Customer.GetFirstOrDefault(new QueryHelper<Customer>() { Filter = t => t.CustomerId == id })) != null;
        }
    }
}
