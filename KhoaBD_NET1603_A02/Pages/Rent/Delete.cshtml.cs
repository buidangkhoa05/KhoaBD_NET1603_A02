using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace KhoaBDRazorPage.Pages.Rent
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
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
            else
            {
                RentingTransaction = rentingtransaction;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _unitOfWork.RentingTrans.DeleteAsync(t => t.RentingTransationId == id);

            return RedirectToPage("./Index");
        }
    }
}
