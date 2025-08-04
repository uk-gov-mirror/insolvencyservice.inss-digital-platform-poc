namespace DS.Forms.Models
{
    /// <summary>
    /// Represents a postal address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the first line of the address.
        /// </summary>
        public string Address1 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the second line of the address.
        /// </summary>
        public string Address2 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the town or city of the address.
        /// </summary>
        public string Town { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the postcode of the address.
        /// </summary>
        public string Postcode { get; set; } = string.Empty;
    }
}
