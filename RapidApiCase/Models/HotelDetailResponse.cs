using Newtonsoft.Json;
using System.Collections.Generic;

namespace RapidApiCase.Models
{
    public class HotelDetailResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        [JsonProperty("data")]
        public HotelDetail Data { get; set; }

        
        public string? Checkout { get; set; }
        public string? Checkin { get; set; }
    }

    public class HotelDetail
    {
        [JsonProperty("hotel_id")]
        public int HotelId { get; set; }

        [JsonProperty("hotel_name")]
        public string HotelName { get; set; }

        public string Url { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }

        [JsonProperty("review_nr")]
        public int ReviewCount { get; set; }

        

        // Latitude ve Longitude'un API yanıtında doğrudan Data objesi içinde geldiğini varsayarak ekliyoruz.
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }

        // Otel genel açıklaması için bir alan ekliyoruz, API yanıtınızda bu bilginin nerede geldiğini kontrol edin.
        // Örneğin, 'hotel_text' gibi bir alan olabilir veya 'description' gibi.
        [JsonProperty("hotel_text")]
        public HotelTextObject? HotelTextContent { get; set; }

        // Odaları temsil eden ana yapı
        [JsonProperty("rooms")] // Odaların geldiği yer
        public Dictionary<string, RoomDetail>? Rooms { get; set; }

        // Otel genel fotoğrafları için RawData objesini geri getiriyoruz
        [JsonProperty("rawData")]
        public RawData? RawData { get; set; }

        // Tesis blokları ve top_ufi_benefits gibi genel olanakları geri getiriyoruz
        [JsonProperty("facilities_block")]
        public FacilitiesBlock? Facilities_Block { get; set; }

        [JsonProperty("top_ufi_benefits")]
        public List<TopUfiBenefit>? Top_Ufi_Benefits { get; set; }

        [JsonProperty("spoken_languages")]
        public List<string> SpokenLanguages { get; set; }

       
    }

    public class HotelTextObject
    {
        [JsonProperty("en")] // İngilizce açıklama
        public string? EnglishDescription { get; set; }

        [JsonProperty("tr")] // Türkçe açıklama
        public string? TurkishDescription { get; set; }

        // Diğer dil kodları için esneklik sağlar
        [JsonExtensionData]
        public Dictionary<string, object>? AdditionalLanguages { get; set; }
    }

    public class RoomDetail
    {
        public string Description { get; set; }

        [JsonProperty("photos")]
        public List<RoomPhoto> Photos { get; set; }
        public List<RoomFacility> Facilities { get; set; }
        public List<BedConfiguration> BedConfigurations { get; set; }

        [JsonProperty("room_name")] // Örnek alan adı, API'nize göre değişebilir
        public string? Name { get; set; }

        [JsonProperty("max_occupancy")] // Örnek alan adı, API'nize göre değişebilir
        public int? MaxOccupancy { get; set; }

        // Fiyat detayları için CompositePriceBreakdown gibi bir yapı
        [JsonProperty("composite_price_breakdown")]
        public CompositePriceBreakdown CompositePriceBreakdown { get; set; }
    }

    public class RoomPhoto
    {
        [JsonProperty("url_original")]
        public string OriginalUrl { get; set; }

        [JsonProperty("url_max750")]
        public string UrlMax750 { get; set; }

        [JsonProperty("url_max300")]
        public string UrlMax300 { get; set; }
    }

    public class RoomFacility
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class BedConfiguration
    {
        [JsonProperty("bed_types")]
        public List<BedType> BedTypes { get; set; }
    }

    public class BedType
    {
        public string Name { get; set; }

        [JsonProperty("name_with_count")]
        public string NameWithCount { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }

    // Fiyat detayları için yeni sınıflar (API'deki karmaşık fiyat yapılarına göre)
    public class CompositePriceBreakdown
    {
        [JsonProperty("gross_amount")]
        public GrossAmount GrossAmount { get; set; }
    }

    public class GrossAmount
    {
        [JsonProperty("value")]
        public float Value { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    // Otel genel fotoğrafları için RawData
    public class RawData
    {
        [JsonProperty("reviewScore")]
        public float ReviewScore { get; set; }

        [JsonProperty("reviewScoreWord")]
        public string ReviewScoreWord { get; set; }

        [JsonProperty("photoUrls")]
        public List<string> PhotoUrls { get; set; }
    }

    // Tesis blokları için (geri getirildi)
    public class FacilitiesBlock
    {
        public List<Facility> Facilities { get; set; }
    }

    public class Facility
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    // Top UFI Benefits için (geri getirildi)
    public class TopUfiBenefit
    {
        public string Translated_Name { get; set; }
        public string Icon_Name { get; set; }
    }
}