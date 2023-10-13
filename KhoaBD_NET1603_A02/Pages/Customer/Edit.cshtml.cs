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
using Application.Repository;
using Domain.Dto;
using Application.Common;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace RazorPage.Pages.Customers
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [BindProperty]
        public CustomerDto Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var querHelper = new QueryHelper<Customer, CustomerDto>()
            {
                Filter = t => t.CustomerId == id
            };


            var customer = await _unitOfWork.Customer.GetFirstOrDefault(querHelper);
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
                var updateResult = await _unitOfWork.Customer.UpdateAsync(_mapper.Map<Customer>(Customer), true);
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
            var querHelper = new QueryHelper<Customer>()
            {
                Filter = e => e.CustomerId == id
            };

            return (await _unitOfWork.Customer.GetFirstOrDefault(querHelper)) != null;
        }
    }
}
