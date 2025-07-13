using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RapidApiCase.Models;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RapidApiCase.Services
{
    public interface IBookingService
    {
        Task<List<HotelCardViewModel>> SearchHotelsAsync(string destination, string checkinDate, string checkoutDate, int adults = 1, int children = 0, string childrenAges = "");
        Task<HotelDetailViewModel?> GetHotelDetailAsync(int hotelId, DateTime arrivalDate, DateTime departureDate, int adults, string childrenAges, int roomQty, string languageCode, string currencyCode);
    }

    public class BookingService : IBookingService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private const string BaseUrl = "https://booking-com15.p.rapidapi.com/api/v1";

        public BookingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            // RapidAPI headers
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "booking-com15.p.rapidapi.com");
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", _configuration["RapidApi:ApiKey"] ?? "1f1d6ac16bmsh4593122d632d9d2p1b411fjsn666cf8f9e6fd");
        }

        public async Task<List<HotelCardViewModel>> SearchHotelsAsync(string destination, string checkinDate, string checkoutDate, int adults = 1, int children = 0, string childrenAges = "")
        {
            try
            {
                // Destination ID mapping - buraya daha fazla şehir eklenebilir
                var (destId, destType) = await GetDestinationIdAsync(destination);

                // Children ages formatı (örn: "0,17" veya "5,12,8")
                var childrenAgeParam = children > 0 && !string.IsNullOrEmpty(childrenAges) ? childrenAges : "";

                var url = $"{BaseUrl}/hotels/searchHotels" +
                          $"?dest_id={destId}" +
                          $"&search_type={destType.ToLower()}" + 
                          $"&arrival_date={checkinDate}" +
                          $"&departure_date={checkoutDate}" +
                          $"&adults={adults}" +
                          $"&children_age={Uri.EscapeDataString(childrenAgeParam)}" +
                          $"&room_qty=1" +
                          $"&page_number=1" +
                          $"&units=metric" +
                          $"&temperature_unit=c" +
                          $"&languagecode=en-us" +
                          $"&currency_code=USD" +
                          $"&location=US";

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url)
                };

                using (var response = await _httpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var root = JsonConvert.DeserializeObject<HotelSearchResponse>(json);

                        var hotels = root?.data?.hotels.ToList();

                        if (hotels == null)
                            return new();

                        List<HotelCardViewModel> values = hotels.Select(h => new HotelCardViewModel
                        {
                            HotelId = h.hotel_id,
                            Name = h.property?.name ?? "Bilinmeyen Otel",
                            ImageUrl = h.property?.photoUrls?.FirstOrDefault() ?? "",
                            PhotoUrls = h.property?.photoUrls?.ToList() ?? new List<string>(),

                            Price = h.property?.priceBreakdown?.grossPrice?.value ?? 0,
                            Currency = h.property?.priceBreakdown?.grossPrice?.currency ?? "USD",
                            OriginalPrice = h.property?.priceBreakdown?.strikethroughPrice?.value,

                            ReviewScore = h.property?.reviewScore ?? 0,
                            ReviewCount = h.property?.reviewCount ?? 0,
                            ReviewText = h.property?.reviewScoreWord ?? string.Empty,
                            Stars = (int)Math.Floor((double)(h.property?.reviewScore ?? 0) / 2),

                            // Tarihler ve saatler
                            CheckinDate = h.property?.checkinDate ?? "",
                            CheckoutDate = h.property?.checkoutDate ?? "",
                            CheckinTimeFrom = h.property?.checkin?.fromTime ?? "",
                            CheckinTimeUntil = h.property?.checkin?.untilTime ?? "",
                            CheckoutTimeFrom = h.property?.checkout?.fromTime ?? "",
                            CheckoutTimeUntil = h.property?.checkout?.untilTime ?? "",

                            // Konum bilgileri
                            CountryCode = h.property?.countryCode ?? "",
                            City = h.property?.wishlistName ?? "",
                            Latitude = (double)(h.property?.latitude ?? 0),
                            Longitude = (double)(h.property?.longitude ?? 0),

                            // Rozetler ve bayraklar
                            IsPreferred = h.property?.isPreferred ?? false,
                            IsPreferredPlus = h.property?.isPreferredPlus ?? false,
                            AccessibilityLabel = h.accessibilityLabel ?? "",
                            RankingPosition = h.property?.rankingPosition ?? 0,

                            // Badge'ler
                            BenefitBadges = h.property?.priceBreakdown?.benefitBadges?
                                .Where(b => !string.IsNullOrWhiteSpace(b?.text))
                                .Select(b => b.text)
                                .ToList() ?? new List<string>(),

                            // Özet bilgi olarak accessibility label'ı böl
                            RoomSummary = ExtractRoomSummary(h.accessibilityLabel)
                        }).ToList();

                        return values;

                    }

                    return new List<HotelCardViewModel>();
                }
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error searching hotels: {ex.Message}");
                return new List<HotelCardViewModel>();
            }
        }

        public static string ExtractRoomSummary(string accessibilityLabel)
        {
            if (string.IsNullOrWhiteSpace(accessibilityLabel))
                return string.Empty;

            // Cümleyi noktalama işaretine göre parçala
            var firstSentence = accessibilityLabel.Split('.', '!', '?')
                .FirstOrDefault(s => !string.IsNullOrWhiteSpace(s))?
                .Trim();

            return firstSentence ?? string.Empty;
        }


        private async Task<(string destId, string destType)> GetDestinationIdAsync(string destination)
        {
            var url = $"{BaseUrl}/hotels/searchDestination?query={Uri.EscapeDataString(destination)}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return ("-755070", "CITY"); // fallback: Istanbul

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DestinationResponse>(content);

            // İlk eşleşen uygun destinasyonu al
            var bestMatch = result?.Data?.FirstOrDefault(x =>
                x.DestType == "city" || x.SearchType == "city");

            if (bestMatch != null)
                return (bestMatch.DestId, bestMatch.DestType);

            return ("-755070", "CITY");
        }




        public async Task<HotelDetailViewModel?> GetHotelDetailAsync(int hotelId, DateTime arrivalDate, DateTime departureDate, int adults, string childrenAges, int roomQty, string languageCode, string currencyCode)
        {
            try
            {
                var query = new Dictionary<string, string>
                {
                    ["hotel_id"] = hotelId.ToString(),
                    ["arrival_date"] = arrivalDate.ToString("yyyy-MM-dd"),
                    ["departure_date"] = departureDate.ToString("yyyy-MM-dd"),
                    ["adults"] = adults.ToString(),
                    ["children_age"] = childrenAges ?? "",
                    ["room_qty"] = roomQty.ToString(),
                    ["units"] = "metric",
                    ["temperature_unit"] = "c",
                    ["languagecode"] = languageCode,
                    ["currency_code"] = currencyCode
                };

                string url = QueryHelpers.AddQueryString($"{BaseUrl}/hotels/getHotelDetails", query);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonContent);
                    // Deserialize ettiğimiz model RapidApiCase.Models.HotelDetailResponse olacak, Rootobject değil
                    var apiResponse = JsonConvert.DeserializeObject<HotelDetailResponse>(jsonContent);

                    if (apiResponse == null || !apiResponse.Status || apiResponse.Data == null)
                    {
                        Console.WriteLine($"API yanıtı başarısız veya veri boş: {apiResponse?.Message}");
                        return null;
                    }

                    var data = apiResponse.Data;

                    var viewModel = new HotelDetailViewModel
                    {
                        HotelId = data.HotelId,
                        Name = data.HotelName,
                        Address = data.Address,
                        City = data.City,
                        CountryCode = data.CountryCode,
                        Url = data.Url,
                        ReviewCount = data.ReviewCount,
                        ReviewScore = data.RawData.ReviewScore,
                        ReviewScoreWord = data.RawData.ReviewScoreWord,
                        Latitude = data.Latitude,
                        Longitude = data.Longitude,
                        Description = data.HotelTextContent?.TurkishDescription ?? data.HotelTextContent?.EnglishDescription ?? null,
                        PhotoUrls = data.RawData?.PhotoUrls?.Where(url => !string.IsNullOrEmpty(url)).ToList() ?? new List<string>(),
                        Rooms = new List<RoomViewModel>(),
                        Amenities = new List<string>(),
                        SpokenLanguages = data.SpokenLanguages
                    };

                  
                        foreach (var roomEntry in data.Rooms)
                        {
                            var roomTypeDetail = roomEntry.Value;
                            var roomViewModel = new RoomViewModel
                            {
                                RoomTypeId = roomEntry.Key,
                                Name = roomTypeDetail.Name,
                                Description = roomTypeDetail.Description,
                                MaxOccupancy = roomTypeDetail.MaxOccupancy,
                                RoomPhotoUrls = roomTypeDetail.Photos?
                                    .Where(p => !string.IsNullOrEmpty(p.UrlMax300))
                                    .Select(p => p.UrlMax300)
                                    .ToList() ?? new List<string>(),
                                Facilities = roomTypeDetail.Facilities?.Select(f => f.Name).ToList() ?? new List<string>(),
                                BedConfigurations = roomTypeDetail.BedConfigurations?
                                                        .SelectMany(bc => bc.BedTypes)
                                                        .Select(bt => bt.NameWithCount ?? bt.Name)
                                                        .ToList() ?? new List<string>(),
                                // *** Düzeltme burada: Price ve Currency, RoomTypeDetail içinden eşlendi ***
                                Price = roomTypeDetail.CompositePriceBreakdown?.GrossAmount?.Value,
                                Currency = roomTypeDetail.CompositePriceBreakdown?.GrossAmount?.Currency
                            };
                            viewModel.Rooms.Add(roomViewModel);
                        }
                    

                    // Otel olanaklarını eşleme
                    if (data.Facilities_Block?.Facilities != null)
                    {
                        viewModel.Amenities.AddRange(data.Facilities_Block.Facilities.Select(f => f.Name));
                    }
                    if (data.Top_Ufi_Benefits != null)
                    {
                        viewModel.Amenities.AddRange(data.Top_Ufi_Benefits.Select(b => b.Translated_Name));
                    }
                    viewModel.Amenities = viewModel.Amenities.Distinct().ToList();


                    return viewModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting hotel detail: {ex.Message}");
                return null;
            }
        }
    }
}
