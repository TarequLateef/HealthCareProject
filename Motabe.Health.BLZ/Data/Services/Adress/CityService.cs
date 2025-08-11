using UserManagement.Core.Models.Address;

namespace Motabe.Health.BLZ.Data.Services.Adress
{
    public class CityService:MotabeService<City,City>
    {
        public CityService():base(false)
        {
            //
            this.Item = new City();
            this.OperationItem = new City();
            //
            this.ControllerName="Address/";
            this.ListUrl="AllCities";
            this.DetailsUrl="CityData";
        }
    }
}
