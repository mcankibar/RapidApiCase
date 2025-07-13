using System.Text.Json.Serialization;

namespace RapidApiCase.Models
{
    public class HotelCardViewModel
    {
        [JsonPropertyName("hotelId")]
        public int HotelId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("photoUrls")]
        public List<string> PhotoUrls { get; set; } = new();

        [JsonPropertyName("price")]
        public float Price { get; set; }

        [JsonPropertyName("originalPrice")]
        public float? OriginalPrice { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("reviewScore")]
        public float ReviewScore { get; set; }

        [JsonPropertyName("reviewCount")]
        public int ReviewCount { get; set; }

        [JsonPropertyName("reviewText")]
        public string ReviewText { get; set; }

        [JsonPropertyName("stars")]
        public int Stars { get; set; }

        [JsonPropertyName("checkinDate")]
        public string CheckinDate { get; set; }

        [JsonPropertyName("checkoutDate")]
        public string CheckoutDate { get; set; }

        [JsonPropertyName("checkinTimeFrom")]
        public string CheckinTimeFrom { get; set; }

        [JsonPropertyName("checkinTimeUntil")]
        public string CheckinTimeUntil { get; set; }

        [JsonPropertyName("checkoutTimeFrom")]
        public string CheckoutTimeFrom { get; set; }

        [JsonPropertyName("checkoutTimeUntil")]
        public string CheckoutTimeUntil { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("isPreferred")]
        public bool IsPreferred { get; set; }

        [JsonPropertyName("isPreferredPlus")]
        public bool IsPreferredPlus { get; set; }

        [JsonPropertyName("accessibilityLabel")]
        public string AccessibilityLabel { get; set; }

        [JsonPropertyName("roomSummary")]
        public string RoomSummary { get; set; }

        [JsonPropertyName("benefitBadges")]
        public List<string> BenefitBadges { get; set; } = new();

        [JsonPropertyName("rankingPosition")]
        public int RankingPosition { get; set; }
    }
}
