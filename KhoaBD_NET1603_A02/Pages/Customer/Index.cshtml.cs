using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;

using Domain.Dto;
using Application.Common;
using Application.Repository;

namespace RazorPage.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<CustomerDto> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var querHelper = new QueryHelper<Customer, CustomerDto>();
            Customer = (await _unitOfWork.Customer.Get(querHelper)).ToList();
        }
    }
}
