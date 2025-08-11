
namespace HSB_COMP.BLZ.Comps
{
    #region Validation
    public class FieldValidation
    {
        public IList<ValidCondition> Valids = new List<ValidCondition>();
        public string FieldName { get; private set; } = string.Empty;
        /*public FieldValidation(string fieldName) { this.FieldName=fieldName; this.err=new ErrorStatus(); }*/
        public ErrorStatus err { get; private set; }
        string[] Numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "." };
        public void Validate()
        {
            IList<ValidCondition> vals = this.Valids;
            this.err=new ErrorStatus();
            foreach (var item in vals.ToList())
            {
                this.err.Error = item.Condition;
                if (item.Condition)
                {
                    this.err.ErrorMessage=item.ValidText;
                    break;
                }
            }
            this.Valids.Clear();
        }

        public bool IsNumberField(string val)
        {
            bool isNumber = true;
            char[] valChar = val.ToCharArray();
            foreach (var item in valChar)
            {
                isNumber =  this.Numbers.Any(n => n==item.ToString());
                if (!isNumber) break;
            }
            return !isNumber;
        }
    }

    public class ValidCondition
    {
        public bool Condition { get; set; }
        public string ValidText { get; set; } = string.Empty;
    }

    public class ErrorStatus
    {
        public bool Error { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        public bool Done { get; set; } = false;
        public string FieldID { get; set; } = string.Empty;
    }

    #endregion

    public struct InputType
    {
        public const string TextType = "text";
        public const string PassType = "password";
        public const string EMailType = "email";
        public const string DateType = "date";
        public const string DateTimeType = "datetime";
        public const string DTLoc = "datetime-local";
        public const string NumberType = "number";
    }

    public struct ModalSize
    {
        public const string Large = "modal-lg";
        public const string Small = "modal-sm";
        public const string ExtraLarge = "modal-xl";
    }
}
