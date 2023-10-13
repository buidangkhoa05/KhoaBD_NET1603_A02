using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;
using Application.Repository;
using MapsterMapper;
using Application.Common;
using Domain.Dto;

namespace RazorPage.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteModel(IUnitOfWork unitOfWork, IMapper mapper)
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

            var queryHelper = new QueryHelper<Customer, CustomerDto>()
            { Filter = t => t.CustomerId == id };

            var customer = await _unitOfWork.Customer.GetFirstOrDefault(queryHelper);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var queryHelper = new QueryHelper<Customer, CustomerDto>()
            { Filter = t => t.CustomerId == id };

            var customer = await _unitOfWork.Customer.GetFirstOrDefault(queryHelper);

            if (customer != null)
            {
                Customer = customer;
                await _unitOfWork.Customer.DeleteAsync(t => t.CustomerId == Customer.CustomerId);
            }

            return RedirectToPage("./Index");
        }
    }
}
