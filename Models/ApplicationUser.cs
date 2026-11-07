using Microsoft.AspNetCore.Identity;

namespace Recipendium_Reparsed.Models
{
    /// <summary>
    /// Custom user class that extends IdentityUser with a DisplayName property.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Display name shown in the navbar. Falls back to email if empty.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
    }
}
