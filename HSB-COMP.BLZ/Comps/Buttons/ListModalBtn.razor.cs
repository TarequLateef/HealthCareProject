using Microsoft.AspNetCore.Components;

namespace HSB_COMP.BLZ.Comps.Buttons
{
    public partial class ListModalBtn
    {
        [Parameter] public string TitleProp { get; set; } = string.Empty;
        [Parameter] public string CreateWinLink { get; set; } = string.Empty;
        [Parameter] public string TableCaption { get; set; } = string.Empty;
        [Parameter] public bool WithCreateBtn { get; set; } = true;
        [Parameter] public bool WithRecycle { get; set; } = false;
        [Parameter] public bool WithPrinting { get; set; } = false;
        [Parameter] public int RowsCount { get; set; }
        [Parameter] public int CurrPage { get; set; }
        [Parameter] public int RowsPerPage { get; set; } = 5;
        [Parameter] public bool WithExit { get; set; } = false;
        [Parameter] public RenderFragment ElemTableHead { get; set; }
        [Parameter] public RenderFragment ElemTableBody { get; set; }
        [Parameter] public RenderFragment ElemCreateProp { get; set; }
        [Parameter] public EventCallback<int> EvChangePage { get; set; }
        [Parameter] public EventCallback<bool> EvShowIgnored { get; set; }
        [Parameter] public EventCallback EvShowCreateModal { get; set; }
        [Parameter] public EventCallback EvExit { get; set; }
        /*public int RowsPerPage { get; set; } = 5;*/
        bool mouseOver { get; set; } = false;
        bool operateCreate { get; set; } = false;
        bool withNavigation => this.RowsCount > this.RowsPerPage;
        bool showIgnored = false;
        void CreateItem()
        {
            operateCreate = true;
            EvChangePage.InvokeAsync();
            EvShowCreateModal.InvokeAsync();
        }
        void ChangePage(int pg) { EvChangePage.InvokeAsync(pg); CurrPage = pg; }
        Dictionary<string, object> modalAttr { get; set; } = new() { { "data-bs-dismiss", "modal" } };

    }
}
