namespace GeneralMotabea.Core.General
{
    public class MathConvertion
    {
        string _returnedNumber = string.Empty;
        public string ReturnedNumber { get; private set; }
        public MathConvertion(MathSystem Sys, int ConvertNumber)
        {
            int baseSys = 0;
            switch (Sys)
            {
                case MathSystem.Decimal:
                    baseSys=10; break;
                case MathSystem.Binary:
                    baseSys=2; break;
                case MathSystem.Octal:
                    baseSys=8; break;
                case MathSystem.Hexa:
                    baseSys=16; break;
                default:
                    break;
            }
            string strRetNo = string.Empty;
            int convNo = ConvertNumber;
            while (convNo!=0)
            {
                int divNo = Convert.ToInt16(convNo/baseSys);
                int retNo = convNo%baseSys;
                switch (retNo)
                {
                    case 10:
                        strRetNo+="A"; break;
                    case 11:
                        strRetNo+="B"; break;
                    case 12:
                        strRetNo += "C"; break;
                    case 13:
                        strRetNo += "D"; break;
                    case 14:
                        strRetNo += "E"; break;
                    case 15:
                        strRetNo += "F"; break;
                    default:
                        strRetNo+=retNo.ToString(); break;
                }
                convNo=divNo;
            }
            _returnedNumber = strRetNo;
            char[] arrNo = _returnedNumber.ToCharArray();
            Array.Reverse(arrNo);
            this.ReturnedNumber = new String(arrNo);
        }
    }

    public enum MathSystem { Decimal, Binary, Octal, Hexa }
}
