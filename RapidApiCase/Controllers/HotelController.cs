using Microsoft.AspNetCore.Mvc;
using RapidApiCase.Models;
using RapidApiCase.Services;

namespace RapidApiCase.Controllers
{
    public class HotelController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<HotelController> _logger;

        public HotelController(IBookingService bookingService, ILogger<HotelController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> SearchHotel(string city, string checkin, string checkout, int adults, int children, string childrenAges)
        {
            try
            {
                var searchResult = await _bookingService.SearchHotelsAsync(city, checkin, checkout, adults, children, childrenAges);
                return Json(searchResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during AJAX hotel search");
                return Json(new { error = true, message = "Arama sırasında hata oluştu." });
            }
        }


        public async Task<IActionResult> Detail(int hotelId, DateTime arrivalDate, DateTime departureDate, int adults, string childrenAges, int roomQty, string languageCode, string currencyCode,string accessibilityLabel,bool isPreferred,bool isPreferredPlus,bool isDiscounted)
        {
            var hotel = await _bookingService.GetHotelDetailAsync(hotelId,arrivalDate,departureDate,adults,childrenAges,roomQty,languageCode,currencyCode);
            ViewBag.access = accessibilityLabel;
            ViewBag.preffered = isPreferred;
            ViewBag.prefferedPlus = isPreferredPlus;
            ViewBag.discounted = isDiscounted;
            if (hotel == null)
                return NotFound();

            return View(hotel);
        }
    }
}
