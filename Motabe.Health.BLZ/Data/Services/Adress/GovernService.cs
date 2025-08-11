using UserManagement.Core.Models.Address;

namespace Motabe.Health.BLZ.Data.Services.Adress
{
    public class GovernService : MotabeService<Govern, Govern>
    {
        public GovernService() : base(false)
        {
            //
            this.Item = new Govern();
            this.OperationItem = new Govern();
            //
            this.ControllerName="Address/";
            this.ListUrl="AllGoverns";
            this.DetailsUrl="GovernData";
        }
    }
}
