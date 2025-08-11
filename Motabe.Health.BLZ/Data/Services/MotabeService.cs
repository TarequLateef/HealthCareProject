

using HSB_COMP.BLZ.Comps;
using Motabe.Health.BLZ.Data.Interface;
using static HSB_COMP.BLZ.Comps.SelectComp.SelectListComp;

namespace Motabe.Health.BLZ.Data
{
    public class MotabeService<T, DTO> : IMotabesService<T, DTO>
    {
        protected readonly HttpClient _http;
        public string UserWinUrl => "http://localhost:40048/";
        /*public string UserWinUrl => "http://169.254.88.71:7059/";*/

        #region Basics
        public T Item { get; set; }
        public DTO OperationItem { get; set; }
        public IList<T> MainList { get; set; }
        public IList<T> DataList { get; set; }
        public IList<T> OperationList { get; set; }
        public IList<T> SearchList { get; set; }
        public int FieldsPerPage { get; set; } = 8;
        public bool ShowAll { get; set; } = false;
        public bool ShowAval { get; set; } = true;
        public int CurrPage { get; set; } = 1;
        public int ParentListCount => this.Searching ? this.SearchList.Count : this.OperationList.Count;
        public string CurrUserID { get; set; } = string.Empty;

        #endregion

        #region Searching
        public bool Searching { get; set; } = false;
        public IList<SearchTable> SearchArr = new List<SearchTable>();

        protected private void ManageSearchArr(SearchTable st)
        {
            bool done = false;
            if (!this.SearchArr.Any()) SearchArr.Add(st);
            else
            {
                while (!done)
                {
                    foreach (var item in SearchArr)
                    {
                        if (item.FieldName==st.FieldName)
                        {
                            if (string.IsNullOrEmpty(st.SearchValue))
                            {
                                SearchArr.Remove(item);
                                done=true; break;
                            }
                            item.SearchValue=st.SearchValue;
                            item.Condition=st.Condition;
                            done=true; break;
                        }
                    }
                    if (!done) { SearchArr.Add(st); done=true; }
                }
            }
        }


        #endregion

        #region Navigation
        public bool Autherize { get; protected set; } = true;

        private string _cont;
        public string ControllerName { get => _http.BaseAddress+_cont; protected set => _cont = value; }
        private string _listUrl = string.Empty;
        public string ListUrl
        {
            get => this.ControllerName+_listUrl+(this.Autherize ? "?userID="+CurrUserID : string.Empty);
            protected set => _listUrl= value;
        }
        private string _addUrl = string.Empty;
        public string AddUrl
        {
            get => ControllerName+_addUrl;
            protected set => _addUrl=value;
        }
        private string _updateUrl = string.Empty;
        public string UpdateUrl
        {
            get => ControllerName+_updateUrl;
            protected set => _updateUrl=value;
        }
        private string _detUrl = string.Empty;
        public string DetailsUrl
        {
            get => ControllerName+_detUrl+(this.Autherize ? "?userID=" + this.CurrUserID : string.Empty);
            protected set => _detUrl=value;
        }
        private string _delUrl = string.Empty;
        public string DeleteUrl
        {
            get => ControllerName+_delUrl+(this.Autherize ? "?userID=" + this.CurrUserID : string.Empty);
            protected set => _delUrl=value;
        }
        private string _stopUrl = string.Empty;
        public string StopRestoreUrl
        {
            get => ControllerName+_stopUrl;
            protected set => _stopUrl=value;
        }
        private string _downFileUrl = string.Empty;
        public string DownloadFileUrl
        {
            get => ControllerName+_downFileUrl+(this.Autherize ? "?userID=" + this.CurrUserID : string.Empty);
            protected set { _downFileUrl =value; }
        }
        private string _downUrl = string.Empty;
        public string DownloadURL
        {
            get => ControllerName+_downUrl+(this.Autherize ? "?userID=" + this.CurrUserID : string.Empty);
            protected set => _downUrl = value;
        }
        #endregion

        #region Windows
        public string ListWin { get; protected set; }
        public string AddWin { get; protected set; }
        public string UpdateWin { get; protected set; }
        public string DetailsWin { get; protected set; }
        public string DeleteWin { get; protected set; }
        public string StopRestoreWin { get; protected set; }
        #endregion

        #region Item Properity
        public bool ItemAvaliable { get; set; }
        public bool ItemDeletable { get; set; }
        public string Message { get; set; }
        #endregion

        public MotabeService(bool basicApp=true)
        {
            this._http= new HttpClient();

            _http.BaseAddress=basicApp ?
                new Uri("https://localhost:7129/api/")
                : new Uri("https://localhost:7061/API/");

            /*_http.BaseAddress=basicApp ?
                new Uri("http://169.254.88.71:7129/api/")
                : new Uri("http://169.254.88.71:7058/API/");*/

            #region List
            this.DataList=new List<T>();
            this.MainList=new List<T>();
            this.OperationList=new List<T>();
            this.SearchList=new List<T>();

            #endregion
        }
        #region Events
        public bool SaveNotification { get; set; } = false;
        public bool ItemSaved { get; set; }
        public async Task<IList<T>> GetDataList(string actionUrl) =>
            await _http.GetFromJsonAsync<List<T>>(actionUrl);


        public async Task<IList<T>> GetDataList(string actionUrl, bool aval) =>
            await _http.GetFromJsonAsync<List<T>>(actionUrl+(Autherize ? "&" : "?")+"aval="+aval);

        public async Task<HttpResponseMessage> AddItem(DTO item, string url) =>
            await _http.PostAsJsonAsync<DTO>(url, item);

        public async Task<HttpResponseMessage> UptateItem(DTO item, string url, string id) =>
            await _http.PutAsJsonAsync(url+"?id="+id, item);

        public async Task<HttpResponseMessage> UptateItem(DTO item, string url) =>
            await _http.PutAsJsonAsync(url, item);

        public async Task<HttpResponseMessage> StopRestoreItem(DTO item, string url, string id) =>
            await _http.PutAsJsonAsync(url+"?id="+id, item);

        public async Task<HttpResponseMessage> StopRestoreItem(DTO item, string url) =>
            await _http.PutAsJsonAsync(url, item);

        public async Task DeleteItem(string url, int id) =>
            await _http.DeleteAsync(url+(Autherize ? "&" : "?")+"id="+id.ToString());

        public async Task DeleteItem(string url, string id) =>
            await _http.DeleteAsync(url+(Autherize ? "&" : "?")+"id="+id);

        public async Task DeleteItem(string url) => await _http.DeleteAsync(url);

        public async Task<T> GetData(string actionUrl, string itemID) =>
            await _http.GetFromJsonAsync<T>(actionUrl+(Autherize ? "&" : "?")+"id="+itemID);

        /*public async Task<T> GetData(string actionUrl, int itemID) =>
            await _http.GetFromJsonAsync<T>(actionUrl+(Autherize ? "&" : "?")+"id="+itemID);*/

        public async Task<T> GetData(string actionUrl) => await _http.GetFromJsonAsync<T>(actionUrl);
        public IList<T> ReturnList(int pg)
        {
            this.CurrPage=pg;
            return this.Searching ?
                this.ShowRange(this.SearchList, pg) : this.ShowRange(this.OperationList, pg);
        }
        public void ChangePage(int pg)
        {
            this.CurrPage=pg==0 ? 1 : pg;
            this.DataList= this.Searching ?
                this.ShowRange(this.SearchList, pg) : this.ShowRange(this.OperationList, pg);
        }
        public IList<T> ShowRange(IList<T> list, int currPage) =>
         list.Skip((currPage-1)*FieldsPerPage).Take(FieldsPerPage).ToList();
        #endregion

        #region Validations
        //Validation
        IList<FieldValidation> ValidList = new List<FieldValidation>();
        public IList<ErrorStatus> ErrorList = new List<ErrorStatus>();

        public void ReloadErrors(ErrorStatus err)
        {
            if (!string.IsNullOrEmpty(err.FieldID))
                if (!this.ErrorList.Any(e => e.FieldID==err.FieldID))
                    this.ErrorList.Add(err);
                else
                {
                    ErrorStatus errItem = this.ErrorList.First(e => e.FieldID==err.FieldID);
                    int errIndx = this.ErrorList.IndexOf(errItem);
                    this.ErrorList[errIndx] = err;
                }
        }

        #endregion
    }
}
