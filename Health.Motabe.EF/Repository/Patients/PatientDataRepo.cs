using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.Interfaces.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.EF;

namespace Health.Motabe.EF.Repository.Patients
{
    public class PatientDataRepo : OperationRepository<PatientBaseData>, IPatientDataRepo
    {
        readonly HealthAppContext _ctx;
        public PatientDataRepo(HealthAppContext ctx) : base(ctx) => _ctx=ctx;

        public async Task<ReturnState<PatientBaseData>> ByPhone(string phone)
        {
            PatientBaseData PDitem = await this.FindSingle(pd => pd.Phone==phone);
            if (PDitem is null)
                return this.failState(true);
            return new ReturnState<PatientBaseData>
            {
                Item=PDitem,
                Message=PDitem.Phone,
                Status=false
            };
        }

        public ReturnState<PatientBaseData> ErrorState() =>
            new ReturnState<PatientBaseData>
            {
                Item=new PatientBaseData(),
                Message="خطأ ما",
                Status=false
            };

        private ReturnState<PatientBaseData> failState(bool done) =>
                new ReturnState<PatientBaseData>
                {
                    Item = new PatientBaseData(),
                    Status=done,
                    Message="غير موجود"
                };
    }
}
