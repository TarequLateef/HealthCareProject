using UserManagement.Core.Models.Address;

namespace Motabe.Health.BLZ.Data.Services.Adress
{
    public class CountryService:MotabeService<Country,Country>
    {
        public CountryService():base(false)
        {
            this.Item = new Country();
            this.OperationItem = new Country();
            //
            this.ControllerName="Address/";
            this.ListUrl="AllCountries";
        }
    }
}
