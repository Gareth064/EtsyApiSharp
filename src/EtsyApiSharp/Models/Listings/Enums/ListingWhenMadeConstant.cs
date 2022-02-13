namespace EtsyApiSharp.Models.Listings.Enums
{
    /// <summary>
    /// Constant string values to accomodate the When Made property on
    /// the ShopListing and ShopListingWithAssociations models. 
    /// Had to do this instead of the traditional Enum because 
    /// some values begin with a number which c# does not allow.
    /// </summary>
    public static class ListingWhenMadeConstant
    {
        public const string _MadeToOrder = "made_to_order";
        public const string _2020_2022 = "2020_2022";
        public const string _2010_2019 = "2010_2019";
        public const string _2003_2009 = "2003_2009";
        public const string _before_2003 = "before_2003";
        public const string _2000_2002 = "2000_2002";
        public const string _1990s = "1990s";
        public const string _1980s = "1980s";
        public const string _1970s = "1970s";
        public const string _1960s = "1960s";
        public const string _1950s = "1950s";
        public const string _1940s = "1940s";
        public const string _1930s = "1930s";
        public const string _1920s = "1920s";
        public const string _1910s = "1910s";
        public const string _1900s = "1900s";
        public const string _1800s = "1800s";
        public const string _1700s = "1700s";
        public const string _Before1700 = "before_1700";
    }
}
