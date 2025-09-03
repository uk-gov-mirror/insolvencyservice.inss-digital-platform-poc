namespace INSS.Forms.RCL.Models
{
    public class InputModel
    {
        public string Name { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string PlaceHolder { get; set; } = string.Empty;
        public string Type { get; set; } = "text";
        public bool SmallText { get; set; } = false;
    }
}
