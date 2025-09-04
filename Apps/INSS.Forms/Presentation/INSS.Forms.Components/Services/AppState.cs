using INSS.Forms.Domain.Models.Composite;

namespace INSS.Forms.Components.Services
{
    public class AppState
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public Address? Address { get; set; }
        public string? Telephone { get; set; }
    }
}
