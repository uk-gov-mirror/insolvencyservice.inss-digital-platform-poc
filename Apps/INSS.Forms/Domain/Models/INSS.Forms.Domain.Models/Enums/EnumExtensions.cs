using System.ComponentModel;

namespace INSS.Forms.Domain.Models.Enums
{
    /// <summary>
    /// Provides extension methods for working with <see cref="Enum"/> types.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves the <see cref="DescriptionAttribute"/> value of an enum member, if present; otherwise, returns the enum member's name.
        /// </summary>
        /// <param name="value">The enum value to get the description for.</param>
        /// <returns>
        /// The description specified by the <see cref="DescriptionAttribute"/> if it exists; otherwise, the enum member's name as a string.
        /// </returns>
        public static string GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi!.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
