using INSS.Forms.Domain.Models.Configuration;
using Microsoft.EntityFrameworkCore;

namespace INSS.Forms.Application.Services.Data
{
    public class FormsRepository
    {
        private readonly DbContext _context;

        public FormsRepository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all SectionForm entities associated with a given DigitalService Id.
        /// </summary>
        /// <param name="digitalServiceId">The Id of the DigitalService.</param>
        /// <returns>A list of SectionForm entities.</returns>
        public async Task<List<SectionForm>> GetStepFormsForDigitalServiceAsync(Guid digitalServiceId)
        {
            return await _context.Set<SectionForm>()
                .Include(sf => sf.Section)
                .Include(sf => sf.Form)
                .Where(sf => sf.Form is DigitalServiceForm && ((DigitalServiceForm)sf.Form).DigitalServiceId == digitalServiceId)
                .ToListAsync();
        }
    }

    // Example of a Form implementation that links to DigitalService
    public class DigitalServiceForm : Form
    {
        public Guid DigitalServiceId { get; set; }
        public DigitalService DigitalService { get; set; } = null!;
    }
}
