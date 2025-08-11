using Microsoft.AspNetCore.Components;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace HSB_COMP.BLZ.Comps.Table
{
    public partial class SearchDropDwon 
    {
        [Parameter]
        public string? TitleProp { get; set; }

        [Parameter] public string? FieldNameProp { get; set; }

        [Parameter] public EventCallback<SearchTable> EvSearching { get; set; }

        [Parameter] public SearchDataTypes DataTypes { get; set; } = SearchDataTypes.TextVal;
        [Parameter] public string ListID { get; set; }

        string checkAcion = "";
        private SearchTable Search = new SearchTable();

        private bool filterd = false;
        DateTime _searchDate = DateTime.Now;
        DateTime SearchDate
        {
            get => _searchDate;
            set
            {
                _searchDate=value;
                Search.SearchValue = Convert.ToString(_searchDate.ToString("yyyy-MM-dd"));
                if (!filterd) filterd=true;
                EvSearching.InvokeAsync(Search);

            }
        }
        DateCondition dCond = DateCondition.Day;
        protected override void OnInitialized()
        {
            this.checkAcion = "Contains";
            this.Search = new SearchTable
            {
                SearchValue="",
                Condition=Condition.Contains,
                FieldName=this.FieldNameProp
            };
        }
        private void SearchValue(ChangeEventArgs e)
        {
            Search.SearchValue = Convert.ToString(e.Value);
            if (!filterd) filterd=true;
            EvSearching.InvokeAsync(Search);
        }

        private void ConditionEq()
        {
            Search.Condition=Condition.Equal;
            checkAcion="Equal";
            BtnSearch();
        }
        private void ConditionNotEq()
        {
            Search.Condition=Condition.NotEqual;
            checkAcion="NotEqual";
            BtnSearch();
        }

        private void ConditionNotCont()
        {
            Search.Condition = Condition.NotContain;
            checkAcion="NotContains";
            BtnSearch();
        }

        private void ConditionCont()
        {
            Search.Condition = Condition.Contains;
            checkAcion="Contains";
            BtnSearch();
        }

        private void BtnSearch()
        {
            if (!filterd) filterd=true;
            EvSearching.InvokeAsync(Search);
        }

        private void BtnCancelSearch()
        {
            if (filterd) filterd=false;
            Search=new SearchTable
            {
                Condition=Condition.Contains,
                SearchValue="",
                FieldName=this.FieldNameProp
            };
            EvSearching.InvokeAsync(Search);
        }

    }

}
