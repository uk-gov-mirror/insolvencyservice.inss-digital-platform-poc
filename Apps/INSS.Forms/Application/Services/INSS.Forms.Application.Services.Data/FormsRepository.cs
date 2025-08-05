using INSS.Forms.Domain.Models;
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
        /// Retrieves all StepForm entities associated with a given DigitalService Id.
        /// </summary>
        /// <param name="digitalServiceId">The Id of the DigitalService.</param>
        /// <returns>A list of StepForm entities.</returns>
        public async Task<List<StepForm>> GetStepFormsForDigitalServiceAsync(Guid digitalServiceId)
        {
            return await _context.Set<StepForm>()
                .Include(sf => sf.Step)
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
