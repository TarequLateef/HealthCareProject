using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HSB_COMP.BLZ.Comps.SelectComp
{
    public partial class SelectListComp : ComponentBase
    {
        [Parameter] public bool IsRequired { get; set; } = true;
        [Parameter] public string SearchID { get; set; } = "0";
        [Parameter] public bool WithFullSize { get; set; } = true;
        [Parameter] public string SelectTitle { get; set; } = string.Empty;
        [Parameter] public string ItemsCount { get; set; } = string.Empty;
        [Parameter] public bool EnableSelect { get; set; } = true;
        [Parameter] public bool WithSelectLock { get; set; } = false;
        [Parameter] public bool WithIconProp { get; set; } = false;
        [Parameter] public bool WithAddProp { get; set; } = false;
        [Parameter] public bool WithAddModal { get; set; } = false;
        [Parameter] public string AddLinkProp { get; set; } = string.Empty;
        [Parameter] public bool WithUpdateProp { get; set; } = false;
        /*[Parameter] public bool WithUpdateModal { get; set; } = false;*/
        [Parameter] public string UpdateLinkProp { get; set; } = string.Empty;
        [Parameter] public string InfoLinkProp { get; set; } = string.Empty;
        [Parameter] public bool WithSearchProp { get; set; } = false;
        [Parameter] public string IconClassProp { get; set; } = string.Empty;
        [Parameter] public ErrorStatus ExErrStatus { get; set; } = new ErrorStatus();
        ErrorStatus errorStatus
        {
            get => ExErrStatus;
            set
            {
                ExErrStatus=value;
                EvErrorStatus.InvokeAsync(this.ExErrStatus);
            }
        }
        [Parameter] public RenderFragment? ElemSelectItems { get; set; }
        [Parameter] public RenderFragment? ElemAddModal { get; set; }
        [Parameter] public RenderFragment? ElemUpdateModal { get; set; }
        [Parameter]public EventCallback<string> EvSelectItem { get; set; }
        [Parameter] public EventCallback<ErrorStatus> EvErrorStatus { get; set; }
        [Parameter] public EventCallback<string> EvAddItem { get; set; }
        [Parameter] public EventCallback<string> EvUpdateItem { get; set; }
        [Parameter] public EventCallback<string> EvSearch { get; set; }
        [Parameter] public EventCallback<bool> EvEnableSelect { get; set; }
        private string _selectValue { get; set; } = "0";
        private string selectValue
        {
            get => string.IsNullOrEmpty(_selectValue) ? "0" : _selectValue;
            set
            {
                _selectValue = value;
                /*ShowInfoModalElemProp=true;*/
                if (IsRequired && !string.IsNullOrEmpty(_selectValue) && _selectValue!="0")
                {
                    this.errorStatus.Error=false;
                    this.errorStatus.Done=true;
                    this.errorStatus.FieldID=SearchID;
                }
                if (_selectValue=="0")
                {
                    this.errorStatus.Error=true;
                    this.errorStatus.Done=false;
                    this.errorStatus.FieldID=SearchID;
                }

                EvSelectItem.InvokeAsync(_selectValue);
                EvErrorStatus.InvokeAsync(errorStatus);
            }
        }
        void ChangeSlectParam()
        {
            EnableSelect = !EnableSelect;
            EvEnableSelect.InvokeAsync(EnableSelect);
        }
        bool adding { get; set; } = false;
        bool updating { get; set; } = false;
        bool searcing { get; set; } = false;
        string newValue { get; set; } = string.Empty;
        string updateValue { get; set; } = string.Empty;
        string srchValue { get; set; } = string.Empty;
        public enum Condition
        { Equal, GreaterThan, LessThan, Contains, NotContain, EqualorGreaterThan, EqualorLessThan, NotEqual }

        public class SearchTable
        {
            public string SearchValue { get; set; } = string.Empty;
            public Condition Condition = Condition.Contains;
            public string FieldName { get; set; } = string.Empty;
        }
        public enum SearchDataTypes { TextVal, DateVal }
        public enum DateCondition { Day, Month, Year, Between, NotBetween }
        /*[Parameter] public bool ShowInfoModalElemProp { get; set; } = false;
        [Parameter] public bool WithSearchProp { get; set; } = true;
        [Parameter]
        public bool WithAddItemProp { get; set; } = false;
        [Parameter]
        public bool InnerAddingProp { get; set; } = false;
        [Parameter]
        public bool WithInformationProp { get; set; } = false;
        [Parameter] public string InfoLinkProp { get; set; } = string.Empty;
        [Parameter] public string InfoItemID { get; set; } = string.Empty;
        [Parameter] public string AddLinkProp { get; set; } = string.Empty;
        [Parameter] public string AddItemTitleProp { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<string> EvSearchText { get; set; }
        [Parameter]
        public EventCallback<string> EvAddItem { get; set; }
        [Parameter]
        public RenderFragment? ElemListCount { get; set; }
        [Parameter]
        public RenderFragment? ElemInfoModal { get; set; }
        private string InputValue { get; set; } = string.Empty;*/
        /*private string _searchText { get; set; } = string.Empty;*/
        /*private bool EraseSearch { get; set; } = false;*/

        /*private void Searching()
        {
            EraseSearch=true;
            EvSearchText.InvokeAsync(_searchText);
        }

        private void EraseingSearch()
        {
            EraseSearch=false;
            _searchText=string.Empty;
            EvSearchText.InvokeAsync(_searchText);
        }


        void Submit() => EvAddItem.InvokeAsync(InputValue);*/
    }

}
