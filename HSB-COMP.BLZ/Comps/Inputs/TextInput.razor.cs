
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace HSB_COMP.BLZ.Comps.Inputs
{
    public partial class TextInput
    {
        [Parameter] public string InputID { get; set; }
        [Parameter] public string TitleProp { get; set; } = string.Empty;
        [Parameter] public string PlaceHolder { get; set; } = string.Empty;
        [Parameter] public string InputData { get; set; } = InputType.TextType;
        [Parameter] public int FieldLength { get; set; }
        [Parameter] public int MinLength { get; set; }
        [Parameter] public bool WithIconProp { get; set; } = false;
        [Parameter] public bool WithLimitList { get; set; } = false;
        [Parameter] public bool IsRequired { get; set; } = true;
        [Parameter] public bool IsNumber { get; set; } = false;
        [Parameter] public string[] LimitList { get; set; }
        [Parameter] public string IconClassProp { get; set; }
        [Parameter] public EventCallback<string> EvGetValue { get; set; }
        [Parameter] public EventCallback<ErrorStatus> EvErrorStatus { get; set; }
        [Parameter] public string InputValue { get; set; }
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
        FieldValidation valid = new FieldValidation();
        string txtData
        {
            get => InputValue;
            set
            {
                InputValue = value;
                this.errorStatus.FieldID=InputID;
                if (IsRequired && (string.IsNullOrEmpty(InputValue) || InputValue.Length == 0))
                {

                    this.errorStatus.Error = true;
                    this.errorStatus.ErrorMessage = "يجب إدخال " + TitleProp;
                }
                else if ((InputData==InputType.EMailType && !InputValue.Contains("@") )|| (InputData==InputType.EMailType && !InputValue.Contains(".com")))
                {
                    this.errorStatus.Error=true;
                    this.errorStatus.ErrorMessage="يجب ادخال " + TitleProp + " بشكل صحيح";
                }
                else if (IsNumber && valid.IsNumberField(InputValue))
                {
                    this.errorStatus.Error=true;
                    this.errorStatus.ErrorMessage=TitleProp + " يحتوي على ارقام فقط";
                }
                else if (InputValue.Length<MinLength)
                {
                    this.errorStatus.Error=true;
                    this.errorStatus.ErrorMessage=TitleProp + " يجب ألا تقل عن " + MinLength.ToString();
                }
                else
                {
                    this.errorStatus.Error=false;
                    this.errorStatus.ErrorMessage=string.Empty;
                }
                /*if (ExErrStatus.Error) this.ErrorStatus=ExErrStatus;*/
                if (!string.IsNullOrEmpty(InputValue))
                    this.errorStatus.Done = !this.errorStatus.Error;
                EvGetValue.InvokeAsync(InputValue);
                EvErrorStatus.InvokeAsync(this.errorStatus);
            }
        }
        
        bool mouseOver { get; set; } = false;
        protected override void OnInitialized()
        {
            this.errorStatus = new ErrorStatus();
            this.errorStatus.FieldID=InputID;
            this.EvErrorStatus.InvokeAsync(errorStatus); 
        }

    }
}
