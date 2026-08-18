namespace EtsyApiSharp.Models;
/// <summary>
/// Represents Scope.
/// </summary>

public enum Scope
{
    /// <summary>
    /// Read a member's shipping addresses.
    /// </summary>
    address_r,
    /// <summary>
    /// Update and delete a member's shipping address.
    /// </summary>
    address_w,
    /// <summary>
    /// Read a member's Etsy bill charges and payments.
    /// </summary>
    billing_r,
    /// <summary>
    /// Read the contents of a memberÃƒÆ’Ã†â€™Ãƒâ€šÃ‚Â¢ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â€šÂ¬Ã…Â¡Ãƒâ€šÃ‚Â¬ÃƒÆ’Ã‚Â¢ÃƒÂ¢Ã¢â€šÂ¬Ã…Â¾Ãƒâ€šÃ‚Â¢s cart.
    /// </summary>
    cart_r,
    /// <summary>
    /// Add and remove listings from a member's cart.
    /// </summary>
    cart_w,
    /// <summary>
    /// Read a member's email address
    /// </summary>
    email_r,
    /// <summary>
    /// View a member's favorite listings and users.
    /// </summary>
    favorites_r,
    /// <summary>
    /// Add to and remove from a member's favorite listings and users.
    /// </summary>
    favorites_w,
    /// <summary>
    /// View all details of a member's feedback (including purchase history.)
    /// </summary>
    feedback_r,
    /// <summary>
    /// Delete a member's listings.
    /// </summary>
    listings_d,
    /// <summary>
    /// Read a member's inactive and expired (i.e., non-public) listings.
    /// </summary>
    listings_r,
    /// <summary>
    /// Create and edit a member's listings.
    /// </summary>
    listings_w,
    /// <summary>
    /// Read a member's private profile information.
    /// </summary>
    profile_r,
    /// <summary>
    /// Update a member's private profile information.
    /// </summary>
    profile_w,
    /// <summary>
    /// View a member's recommended listings.
    /// </summary>
    recommend_r,
    /// <summary>
    /// Remove a member's recommended listings.
    /// </summary>
    recommend_w,
    /// <summary>
    /// See a member's shop description, messages and sections, even if not (yet) public.
    /// </summary>
    shops_r,
    /// <summary>
    /// Update a member's shop description, messages and sections.
    /// </summary>
    shops_w,
    /// <summary>
    /// Read a member's purchase and sales data. This applies to buyers as well as sellers.
    /// </summary>
    transactions_r,
    /// <summary>
    /// Update a member's sales data.
    /// </summary>
    transactions_w
}
