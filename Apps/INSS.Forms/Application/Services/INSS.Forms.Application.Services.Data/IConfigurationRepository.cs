using INSS.Forms.Domain.Models.Configuration;

namespace INSS.Forms.Application.Services.Data
{
    public interface IConfigurationRepository
    {
        public Task<List<SectionForm>> GetFormsForDigitalServiceAsync(Guid digitalServiceId);
    }
}
