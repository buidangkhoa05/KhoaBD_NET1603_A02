﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;

namespace KhoaBDRazorPage.Pages.Rent
{
    public class DeleteModel : PageModel
    {
        private readonly Domain.Models.FucarRentingManagementContext _context;

        public DeleteModel(Domain.Models.FucarRentingManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
      public RentingTransaction RentingTransaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RentingTransactions == null)
            {
                return NotFound();
            }

            var rentingtransaction = await _context.RentingTransactions.FirstOrDefaultAsync(m => m.RentingTransationId == id);

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
            if (id == null || _context.RentingTransactions == null)
            {
                return NotFound();
            }
            var rentingtransaction = await _context.RentingTransactions.FindAsync(id);

            if (rentingtransaction != null)
            {
                RentingTransaction = rentingtransaction;
                _context.RentingTransactions.Remove(RentingTransaction);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}