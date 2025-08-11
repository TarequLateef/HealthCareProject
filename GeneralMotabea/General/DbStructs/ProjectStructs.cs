
namespace GeneralMotabea.Core.General.DbStructs
{

    public struct ForiegnKeys
    {
        public const string UserLog = "UserLogID";
    }

    public struct TableNames
    {
        public const string UserTbl = "UserDataTbl";
        public const string RolesTbl = "RolesTbl";
        public const string UserRolesTbl = "UserRolesTbl";
        public const string UserPassTbl = "UserPassTbl";
        public const string UserLogTbl = "UserLogTbl";
        public const string AppData = "AppDataTBL";
        public const string UserApp = "UserAppTBL";
    }

    public struct CompanyTables
    {
        public const string FieldTable = "SpecFieldTBL";
        public const string CompanyTable = "CompanyTBL";
        public const string CompanyRespTable = "CompanyResponsiblTBL";
        public const string CompApp = "CompanyAppTBL";
        public const string Work = "WorkTBL";
        public const string Occupation = "OccupationTBL";

    }

    public struct AddressTables
    {
        public const string Country = "CountryTBL";
        public const string Govern = "GovernateTBL";
        public const string City = "CityTBL";
    }

    public struct GoodTables
    {
        public const string BaseGood = "GoodTBL";
        public const string Properties = "PropertyTBL";
        public const string PropertyValue = "PropValTBL";
        public const string PropertyName = "PropNameTBL";
        public const string GoodProperty = "GoodFullDataTBL";
        public const string MeasureUnit = "MeasureUnitTBL";
    }

    public struct InventTables
    {
        public const string InvType = "InventoryTypeTBL";
        public const string InventTBL = "InventoryTBL";
        public const string AddInv = "AddInvoiceTBL";
        public const string AddDetInv = "AddDetInvTBL";
        public const string SubInv = "SubInvoiceTBL";
        public const string SubDetInv = "SubDetInvTBL";
        public const string RetInv = "RetInvoiceTBL";
        public const string RetDetInv = "RetDetInvTBL";
        public const string DelInv = "DelInvoiceTBL";
        public const string DetDetInv = "DelDetInvTBL";
        public const string GoodBalance = "GoodBalanceTBL";
        public const string InvHistory = "InvHistTBL";
    }

    public struct SupplyTables
    {
        public const string Supply = "SupplierTBL";
        public const string SuppInv = "SupplyInvoiceTBL";
        public const string SuppInvDet = "SupplyInvDetTBL";
        public const string SuppDebit = "SuppDebitTBL";
        public const string SuppCash = "SuppCashTBL";
        public const string SuppGoods = "SuppGoodsTBL";
    }

    public struct SchemaNames
    {
        public const string Security = "Security";
        public const string CompSchema = "Company";
        public const string GoodSchema = "Goods";
        public const string InvnetSchema = "Invent";
        public const string SupplySchema = "Supply";
        public const string Address = "Address";
    }
}
