namespace EtsyApiSharp.Models;

public enum Scope
{
    address_r,                   //Read a member's shipping addresses.
    address_w,                  // Update and delete a member's shipping address.
    billing_r,                      //  Read a member's Etsy bill charges and payments.
    cart_r,                         //  Read the contents of a member’s cart.
    cart_w,                       //Add and remove listings from a member's cart.
    email_r,                      // Read a member's email address
    favorites_r,                 // View a member's favorite listings and users.
    favorites_w,               // Add to and remove from a member's favorite listings and users.
    feedback_r,                // View all details of a member's feedback (including purchase history.)
    listings_d,                  // Delete a member's listings.
    listings_r,                   // Read a member's inactive and expired (i.e., non-public) listings.
    listings_w,                  // Create and edit a member's listings.
    profile_r,                    // Read a member's private profile information.
    profile_w,                   // Update a member's private profile information.
    recommend_r,            // View a member's recommended listings.
    recommend_w,           // Remove a member's recommended listings.
    shops_r,                     // See a member's shop description, messages and sections, even if not (yet) public.
    shops_w,                    // Update a member's shop description, messages and sections.
    transactions_r,           //  Read a member's purchase and sales data. This applies to buyers as well as sellers.
    transactions_w           //  Update a member's sales data.
}
