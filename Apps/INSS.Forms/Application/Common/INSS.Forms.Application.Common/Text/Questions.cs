namespace INSS.Forms.Application.Common.Text
{
    /// <summary>
    /// Provides helper methods for formatting label text.
    /// </summary>
    public class Questions
    {
        /// <summary>
        /// Returns a formatted label string in the form "What is your {columnLabel}?".
        /// The <paramref name="columnLabel"/> is converted to lowercase.
        /// </summary>
        /// <param name="columnLabel">The label to be included in the question.</param>
        /// <returns>A formatted question string.</returns>
        public static string WhatIsYour(string columnLabel)
        {
            return $"What is your {columnLabel.ToLower()}?";
        }

        /// <summary>
        /// Returns a formatted label string in the form "What is the {columnLabel}?".
        /// The <paramref name="columnLabel"/> is converted to lowercase.
        /// </summary>
        /// <param name="columnLabel">The label to be included in the question.</param>
        /// <returns>A formatted question string.</returns>
        public static string WhatIsThe(string columnLabel)
        {
            return $"What is the {columnLabel.ToLower()}?";
        }

        /// <summary>
        /// Returns a formatted label string in the form "What date was the {columnLabel}?".
        /// The <paramref name="columnLabel"/> is converted to lowercase.
        /// </summary>
        /// <param name="columnLabel">The label to be included in the question.</param>
        /// <returns>A formatted question string.</returns>
        public static string WhatDateWasThe(string columnLabel)
        {
            return $"What date was the {columnLabel.ToLower()}?";
        }

        /// <summary>
        /// Returns a formatted label string in the form "Who is the {columnLabel}?".
        /// The <paramref name="columnLabel"/> is converted to lowercase.
        /// </summary>
        /// <param name="columnLabel">The label to be included in the question.</param>
        /// <returns>A formatted question string.</returns>
        public static string WhoIsThe(string columnLabel)
        {
            return $"Who is the {columnLabel.ToLower()}?";
        }

    }
}
