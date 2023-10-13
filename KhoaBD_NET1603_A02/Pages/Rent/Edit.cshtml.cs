using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace KhoaBDRazorPage.Pages.Rent
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;


        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public RentingTransaction RentingTransaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentingtransaction = await _unitOfWork.RentingTrans.GetFirstOrDefault(new QueryHelper<RentingTransaction>() { Filter = t => t.RentingTransationId == id });
            if (rentingtransaction == null)
            {
                return NotFound();
            }
            RentingTransaction = rentingtransaction;

            var customerDto = await _unitOfWork.Customer.Get(new QueryHelper<Customer, CustomerDto>());

            ViewData["CustomerId"] = new SelectList(customerDto, "CustomerId", "Email");

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
                await _unitOfWork.RentingTrans.UpdateAsync(RentingTransaction, true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RentingTransactionExists(RentingTransaction.RentingTransationId))
                {
                    return NotFound();
                }
                else
                {
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> RentingTransactionExists(int id)
        {
            var entity = await _unitOfWork.RentingTrans.GetFirstOrDefault(new QueryHelper<RentingTransaction>() { Filter = t => t.RentingTransationId == id });

            return entity != null;
        }
    }
}
