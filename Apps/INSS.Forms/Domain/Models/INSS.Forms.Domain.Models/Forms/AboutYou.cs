using INSS.Forms.Domain.Models.Abstract;

namespace INSS.Forms.Domain.Models.Forms
{
    /// <summary>
    /// Represents information about a person completing the form.
    /// </summary>
    public class AboutYou : FormBase
    {
        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the person.
        /// </summary>
        public Address Address { get; set; } = new Address();

        /// <summary>
        /// Gets or sets the date of birth of the person.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the telephone number of the person.
        /// </summary>
        public string Telephone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the person.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
