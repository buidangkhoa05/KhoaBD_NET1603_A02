using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KhoaBDRazorPage.Pages
{
    [Authorize(Roles ="customer")]
    public class RentHistoryModel : PageModel
    {
        public readonly IUnitOfWork _unitOfWork;

        public RentHistoryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<RentingDetail> RentingDetail { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var rentTrans = await _unitOfWork.RentingTrans.Get(new QueryHelper<RentingTransaction>()
            {
                Filter = t => t.Customer.Email == User.Identity.Name,
                Includes = new System.Linq.Expressions.Expression<Func<RentingTransaction, object>>[]
                {
                    t => t.Customer,
                    t => t.RentingDetails
                }
            });

            var rentTransIds = rentTrans.Select(t => t.RentingTransationId);

            var rentDetails = await _unitOfWork.RentingDetail.Get(new QueryHelper<RentingDetail>()
            {
                Filter = t => rentTransIds.Contains(t.RentingTransactionId),
                Includes = new System.Linq.Expressions.Expression<Func<RentingDetail, object>>[]
                {
                    t => t.Car,
                    t => t.RentingTransaction
                }
            });

            RentingDetail = rentDetails.ToList();
        }
    }
}
