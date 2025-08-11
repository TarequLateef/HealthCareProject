
using Microsoft.AspNetCore.Components;

namespace HSB_COMP.BLZ.Comps.FormsComp
{
    public partial class UpdateForm
    {
        [Parameter] public bool ModalFormProp { get; set; } = false;
        [Parameter] public string ListLinkProp { get; set; } = string.Empty;
        [Parameter] public bool DisableSave { get; set; } = false;
        [Parameter] public string CreateLinkProp { get; set; } = string.Empty;
        [Parameter] public bool WithDataStatusProp { get; set; } = true;
        [Parameter] public bool AvalData { get; set; } = true;
        [Parameter] public bool DeletableData { get; set; } = false;
        [Parameter] public string MsgProp { get; set; } = string.Empty;
        [Parameter] public string ItemIDProp { get; set; } = string.Empty;
        [Parameter] public RenderFragment ElemOperation { get; set; }
        [Parameter] public EventCallback EvSave { get; set; }
        [Parameter] public EventCallback EvExit { get; set; }
        [Parameter] public EventCallback EvRestore { get; set; }
        [Parameter] public EventCallback EvDelete { get; set; }
        Dictionary<string, object> modalAttr { get; set; } = new()
    {
            {"data-bs-dismiss","modal"}
    };
        void Exit() => EvExit.InvokeAsync();
        void ResotreOrStop() => EvRestore.InvokeAsync();
        void DeleteItem() => EvDelete.InvokeAsync();
        void SaveItem() => EvSave.InvokeAsync();

    }
}
