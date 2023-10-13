using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace KhoaBDRazorPage.Pages
{
    [Authorize(Roles = "customer")]
    public class RentModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RentModel(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public RentingTransactionDto RentingTransaction { get; set; } = default!;

        [BindProperty]
        public List<PickCarDto> PickCarDtos { get; set; }


        public async Task<IActionResult> OnGet(string? errorMessage = null)
        {
            //var selectListItems = await _unitOfWork.Customer.Get(new QueryHelper<Customer, CustomerDto>());

            //ViewData["CustomerId"] = new SelectList(selectListItems, "CustomerId", "Email");

            var carList = await _unitOfWork.CarInformation.Get(new QueryHelper<CarInformation, CarInformationDto>());

            var pickCarList = carList.Select(t => new PickCarDto()
            {
                CardId = t.CarId,
                CarName = t.CarName,
                CarRentingPricePerDay = t.CarRentingPricePerDay,
                FuelType = t.FuelType,
                NumberOfDoors = t.NumberOfDoors,
                SeatingCapacity = t.SeatingCapacity,
            });

            PickCarDtos = pickCarList.ToList();

            ErrorMessage = errorMessage;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var cstomer = await _unitOfWork.Customer.GetFirstOrDefault(new QueryHelper<Customer>()
            {
                Filter = t => t.Email == User.Identity.Name
            });

            var rentingTrans = _mapper.Map<RentingTransaction>(RentingTransaction);

            rentingTrans.CustomerId = cstomer.CustomerId;
            rentingTrans.RentingStatus = 1;

            var carPicked = PickCarDtos.Where(t => t.IsPicked)?.ToList();

            if (carPicked == null || !carPicked.Any())
            {
                return RedirectToPage("/SelfRent", new { errorMessage = "Must pick car" });
            }

            foreach (var item in carPicked)
            {
                var isValid = await ValidateDateForRent(item.StartDate, item.EndDate, item.CardId);

                if (!isValid)
                {
                    return RedirectToPage("/SelfRent", new { errorMessage = "Date is not valid" });
                }
            }

            var rentingDetails = carPicked.Select(t => new RentingDetail()
            {
                CarId = t.CardId,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Price = t.CarRentingPricePerDay,
            }).ToList();

            var maxId = (await _unitOfWork.RentingTrans.Get(new QueryHelper<RentingTransaction>())).MaxBy(t => t.RentingTransationId);


            rentingTrans.RentingDetails = rentingDetails;
            rentingTrans.TotalPrice = rentingDetails.Sum(t => t.Price);
            rentingTrans.RentingDate = DateTime.Now;
            rentingTrans.RentingStatus = 1;
            rentingTrans.RentingTransationId = maxId.RentingTransationId + 1;

            try
            {
                var result = await _unitOfWork.RentingTrans.CreateAsync(rentingTrans, true);
            }
            catch (Exception)
            {

            }

            return RedirectToPage("./Index");
        }

        public async Task<bool> ValidateDateForRent(DateTime nStart, DateTime nEnd, int carId)
        {
            if (nStart > nEnd)
            {
                return false;
            }

            var cars = await _unitOfWork.CarInformation.Get(new QueryHelper<CarInformation>()
            {
                Includes = new System.Linq.Expressions.Expression<Func<CarInformation, object>>[]
                {
                    t => t.RentingDetails
                },
                Filter = t => t.RentingDetails.Any(t => t.CarId == carId && !(nEnd < t.StartDate || nStart > t.EndDate))
            });

            if (cars.Any())
            {
                return false;
            }

            return true;
        }
    }
}
