namespace RECRM_API.Extensions
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// Defines the <see cref="ValidationExtension" />
    /// </summary>
    public static class ValidationExtension
    {
        /// <summary>
        /// The FormatErrors
        /// </summary>
        /// <param name="model">The model<see cref="ModelStateDictionary"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string FormatErrors(this ModelStateDictionary model)
        {
            return string.Join(" | ", model.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage));
        }
    }
}