using INSS.Forms.Domain.Models.Configuration;
using Microsoft.EntityFrameworkCore;


namespace INSS.Forms.Application.Services.Data
{
    /// <summary>
    /// Represents the Entity Framework database context for configuration-related entities,
    /// including digital services, sections, forms, and their associations.
    /// </summary>
    public class ConfigurationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDbContext"/> class
        /// using the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the set of digital services.
        /// </summary>
        public DbSet<DigitalService> DigitalServices { get; set; } = null!;

        /// <summary>
        /// Gets or sets the set of digital service section associations.
        /// </summary>
        public DbSet<DigitalServiceSection> DigitalServiceSections { get; set; } = null!;

        /// <summary>
        /// Gets or sets the set of sections.
        /// </summary>
        public DbSet<Section> Sections { get; set; } = null!;

        /// <summary>
        /// Gets or sets the set of section form associations.
        /// </summary>
        public DbSet<SectionForm> SectionForms { get; set; } = null!;

        /// <summary>
        /// Gets or sets the set of forms.
        /// </summary>
        public DbSet<Form> Forms { get; set; } = null!;

        /// <summary>
        /// Configures the entity mappings and relationships for the configuration database context.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite key for DigitalServiceSection
            modelBuilder.Entity<DigitalServiceSection>()
                .HasKey(dss => new { dss.DigitalServiceId, dss.SectionId });

            // Configure composite key for SectionForm
            modelBuilder.Entity<SectionForm>()
                .HasKey(sf => new { sf.SectionId, sf.FormId });
        }
    }
}
