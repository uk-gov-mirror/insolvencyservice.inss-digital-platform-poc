namespace INSS.Forms.RCL.Models
{
    public class ButtonModel
    {
        public string Text { get; set; } = string.Empty;
        public string Type { get; set; } = "submit";
        public string Id { get; set; } = string.Empty;
        public bool Disabled { get; set; } = false;
    }
}
