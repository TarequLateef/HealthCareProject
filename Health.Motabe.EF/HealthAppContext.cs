using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace Health.Motabea.EF
{
    public class HealthAppContext : DbContext
    {
        public HealthAppContext(DbContextOptions<HealthAppContext> options) : base(options) { }

        #region Patient
        public DbSet<PatientBaseData> PatientBase { get; set; }
        public DbSet<PatientData> PatientDatas { get; set; }
        public DbSet<PatientDiag> PatientDiags { get; set; }
        public DbSet<PatientSymptom> PatientSymptom { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<PatCt> PatCts { get; set; }
        public DbSet<PatAnalysis> PatAnalysis { get; set; }
        public DbSet<PatMed> PatMed { get; set; }
        public DbSet<PatientRays> PatientRays { get; set; }
        public DbSet<PatientBio> PatientBios { get; set; }
        #endregion
        #region Service
        public DbSet<Symptoms> Symptoms { get; set; }
        public DbSet<Diagnostic> Diagnostics { get; set; }
        public DbSet<Biometrics> Biometrics { get; set; }
        public DbSet<CT> CT { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
        public DbSet<Rays> Rays { get; set; }
        #endregion
    }
}
