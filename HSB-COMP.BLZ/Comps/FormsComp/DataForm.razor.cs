
using HSB_COMP.BLZ.Comps.Table;
using Microsoft.AspNetCore.Components;

namespace HSB_COMP.BLZ.Comps.FormsComp
{
    public partial class DataForm
    {
        [Parameter] public string TitleProp { get; set; } = string.Empty;
        [Parameter] public bool ModalFormProp { get; set; } = false;
        [Parameter] public string ListLinkProp { get; set; } = string.Empty;
        [Parameter] public string CreateLinkProp { get; set; } = string.Empty;
        [Parameter] public string UpdateLinkProp { get; set; } = string.Empty;
        [Parameter] public string ItemIDProp { get; set; } = string.Empty;
        [Parameter] public string MsgProp { get; set; } = string.Empty;
        [Parameter] public string NavTitleProp { get; set; } = string.Empty;
        [Parameter] public bool WithDataStatusProp { get; set; } = true;
        [Parameter] public bool WithUpdateProp { get; set; } = false;
        [Parameter] public bool WithCreatProp { get; set; } = false;
        [Parameter] public bool WithPagingProp { get; set; } = true;
        [Parameter] public bool WithSearchList { get; set; } = true;
        [Parameter] public bool AvalData { get; set; } = true;
        [Parameter] public bool DeletableData { get; set; } = false;
        [Parameter] public int SelectedIndex { get; set; } = 0;
        [Parameter] public int ItemsCount { get; set; } = 1;
        [Parameter] public RenderFragment ElemData { get; set; }
        [Parameter] public RenderFragment ElemTabs { get; set; }
        [Parameter] public RenderFragment? ElemUpdate { get; set; }
        [Parameter] public EventCallback<string> EvUpdateItem { get; set; }
        //[Parameter] public string? SearchFieldName { get; set; }
        [Parameter] public EventCallback EvRestore { get; set; }
        [Parameter] public EventCallback EvDelete { get; set; }
        [Parameter] public EventCallback EvCreate { get; set; }
        [Parameter] public EventCallback<int> EvNavigateItem { get; set; }
        //[Parameter] public EventCallback<SearchTable> EvSearhing { get; set; }
        //void CreateItem() => EvCreate.InvokeAsync();
        int selectedIndex => SelectedIndex+1; 
        void ResotreOrStop() => EvRestore.InvokeAsync();
        void DeleteItem() => EvDelete.InvokeAsync();
        async Task ChangeIndex(int indx) => await EvNavigateItem.InvokeAsync(indx-1);
    }
}
