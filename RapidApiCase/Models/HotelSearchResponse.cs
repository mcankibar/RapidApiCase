namespace RapidApiCase.Models
{
    public class HotelSearchResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public long timestamp { get; set; }
        public HotelSearchData data { get; set; }
    }

    public class HotelSearchData
    {
        public Hotel[] hotels { get; set; }
       
    }

    public class Hotel
    {
        public int hotel_id { get; set; }
        public string accessibilityLabel { get; set; }
        public Property1 property { get; set; }
    }

    public class Property1
    {
        public string wishlistName { get; set; }
        public int accuratePropertyClass { get; set; }
        public bool isPreferredPlus { get; set; }
        public string checkoutDate { get; set; }
        public Pricebreakdown priceBreakdown { get; set; }
        public float reviewScore { get; set; }
        public Checkin checkin { get; set; }
        public string currency { get; set; }
        public string[] blockIds { get; set; }
        public bool isPreferred { get; set; }
        public int optOutFromGalleryChanges { get; set; }
        public string checkinDate { get; set; }
        public string reviewScoreWord { get; set; }
        public int reviewCount { get; set; }
        public int position { get; set; }
        public int ufi { get; set; }
        public float latitude { get; set; }
        public int propertyClass { get; set; }
        public string countryCode { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public bool isFirstPage { get; set; }
        public int qualityClass { get; set; }
        public int mainPhotoId { get; set; }
        public int rankingPosition { get; set; }
        public Checkout checkout { get; set; }
        public int id { get; set; }
        public string[] photoUrls { get; set; }
        public bool isClassEstimated { get; set; }
    }

    public class Pricebreakdown
    {
        public Strikethroughprice strikethroughPrice { get; set; }
        public Grossprice grossPrice { get; set; }
        public object[] taxExceptions { get; set; }
        public Benefitbadge[] benefitBadges { get; set; }
        public Excludedprice excludedPrice { get; set; }
    }

    public class Strikethroughprice
    {
        public float value { get; set; }
        public string currency { get; set; }
    }

    public class Grossprice
    {
        public string currency { get; set; }
        public float value { get; set; }
    }

    public class Excludedprice
    {
        public string currency { get; set; }
        public float value { get; set; }
    }

    public class Benefitbadge
    {
        public string variant { get; set; }
        public string text { get; set; }
        public string identifier { get; set; }
        public string explanation { get; set; }
    }

    public class Checkin
    {
        public string untilTime { get; set; }
        public string fromTime { get; set; }
    }

    public class Checkout
    {
        public string fromTime { get; set; }
        public string untilTime { get; set; }
    }
}
