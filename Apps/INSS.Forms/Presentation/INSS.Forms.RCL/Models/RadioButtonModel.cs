namespace INSS.Forms.RCL.Models
{
    public class RadioButtonModel
    {
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Hint { get; set; } = string.Empty;
        public bool Inline { get; set; } = false;
        public string Value { get; set; } = string.Empty;
        public List<RadioOption<string>> Items { get; set; } = new List<RadioOption<string>>();
    }
}
