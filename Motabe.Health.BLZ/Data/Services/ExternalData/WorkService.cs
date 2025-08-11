

using UserManagement.Core.DTOs;
using UserManagement.Core.Models.Address;

namespace Motabe.Health.BLZ.Data.Services.ExternalData
{
    public class WorkService:MotabeService<Work,WorkDTO>
    {
        public WorkService():base(false)
        {
            this.Item = new Work();
            this.OperationItem = new WorkDTO();
            //
            this.ControllerName="Work/";
            this.ListUrl="AllWorks";
            this.AddUrl="AddWork";
            this.DetailsUrl="WorkData";
            //
        }
    }
}
