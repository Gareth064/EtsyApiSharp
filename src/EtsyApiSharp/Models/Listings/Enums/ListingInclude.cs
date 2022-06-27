namespace EtsyApiSharp.Models.Listings.Enums
{
    public enum ListingInclude
    {
        Shipping,
        Images,
        Shop,
        User,
        // TODO: Bug inlcluding translation in listing call https://github.com/etsy/open-api/issues/517
        // Translations, 
        Inventory
    }
}
