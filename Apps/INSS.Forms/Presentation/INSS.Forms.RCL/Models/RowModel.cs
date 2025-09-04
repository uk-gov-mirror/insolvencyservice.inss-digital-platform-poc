namespace INSS.Forms.RCL.Models
{
    public class RowModel
    {
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string AriaLabel { get; set; } = string.Empty;
        public string[] Values { get; set; } = { };
        public int ItemIndex { get; set; } = 0;
        public int PageIndex { get; set; } = 0;
    }
}
