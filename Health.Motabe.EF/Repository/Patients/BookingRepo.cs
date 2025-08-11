using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Health.Motabe.EF.Repository.Patients
{
    public class BookingRepo:OperationRepository<Booking>,IBookRepo
    {
        protected readonly HealthAppContext _Hctx;
        public BookingRepo(HealthAppContext healthAppContext) : base(healthAppContext) => 
            _Hctx = healthAppContext;

        public async Task<IList<Booking>> AvailableListAsync() =>
            await this.Find(b => !b.EndDate.HasValue || (b.EndDate.HasValue && b.EndDate.Value>DateTime.Now), new[] { PatientTab.BaseData });

        public async Task<IList<Booking>> BannedListAsync() =>
            await this.Find(b => b.EndDate.HasValue && b.EndDate.Value<=DateTime.Now, new[] { PatientTab.BaseData });

        public bool IsAvalaible(Booking Item) =>
            !Item.EndDate.HasValue || (Item.EndDate.HasValue && Item.EndDate.Value>DateTime.Now);

        public async Task<bool> Repeated(Booking _booking) =>
            await _Hctx.Bookings.AnyAsync(b => _booking.PatientID==b.PatientID && _booking.StartDate.Date==b.StartDate.Date);

        public async Task<Booking> RestoreStop(Booking sItem)
        {
            sItem.EndDate=sItem.EndDate.HasValue ? null : DateTime.Now;
            return await this.Update(sItem.BookID, sItem);
        }

        public Booking RestorStop(Booking sItem)
        {
            sItem.EndDate=sItem.EndDate.HasValue ? null : DateTime.Now;
            return this.Update(sItem);
        }
    }
}
