using INSS.Forms.Components.Models;
using INSS.Forms.Domain.Models.Enums;

namespace INSS.Forms.Components.Helpers
{
    public static class EnumHelper
    {
        public static List<RadioOption<string>> ToRadioOptions<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new RadioOption<string>
                {
                    Value = e.ToString(),
                    Text = e.GetDescription(),
                })
                .ToList();
        }
    }
}
