using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using PRN221.Domain.Models;
using MapsterMapper;
using Team5.Domain.Common;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace KhoaBDRazorPage.Pages.Rent
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private
             readonly IMapper _mapper;

        public IndexModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public string SearchText { get; set; } = default!;
        public PagedList<RentingTransactionDto> RentingTransaction { get; set; } = default!;

        public async Task OnGetAsync(int? pageNumber, string? searchText = "")
        {
            var queryHelper = new QueryHelper<RentingTransaction, RentingTransactionDto>()
            {
                Selector = x => _mapper.Map<RentingTransactionDto>(x),
                PaginationParams = new PagingParameters(pageNumber, null),
                Includes = new System.Linq.Expressions.Expression<Func<RentingTransaction, object>>[]
                {
                    x => x.Customer
                },
                Filter = t => t.Customer.Email.Contains(searchText)
            };

            try
            {
                RentingTransaction = await _unitOfWork.RentingTrans.GetWithPagination(queryHelper);

                if (!string.IsNullOrEmpty(searchText))
                {
                    SearchText = searchText;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
