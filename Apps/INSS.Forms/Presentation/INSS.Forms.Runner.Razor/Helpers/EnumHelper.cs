using INSS.Forms.Application.Common.Extensions;
using INSS.Forms.RCL.Models;

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
