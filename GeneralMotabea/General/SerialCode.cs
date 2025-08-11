namespace GeneralMotabea.Core.General
{
    public class SerialCode
    {
        int NumberOfLetter { get; set; }
        int LastCode { get; set; }

        /*public static string Code { get; private set; } = string.Empty;*/
        /// <summary>
        /// make a number coding for data
        /// </summary>
        /// <param name="NumberOfLetter">count of code letter</param>
        /// <param name="BaseCode">the last number of code</param>
        /// <returns></returns>
        public static string CovnertCode(int NumberOfLetter, string BaseCode)
        {
            string compCode = string.Empty;
            int baseLen = BaseCode.Length;
            if (baseLen < NumberOfLetter)
                for (int i = baseLen; i < NumberOfLetter; i++)
                    compCode+="0";
            return compCode + BaseCode;
        }
        public SerialCode(int NumberOfLetter, int LastCode)
        {
            this.NumberOfLetter=NumberOfLetter;
            this.LastCode=LastCode;
        }

        public string GenerateCode
        {
            get
            {
                string newCode = (LastCode + 1).ToString();
                if (newCode.Length>NumberOfLetter) return "وصل الكود لأخر قيمة";
                string compCode = string.Empty;
                int baseLen = newCode.Length;
                if (baseLen < NumberOfLetter)
                    for (int i = baseLen; i < NumberOfLetter; i++)
                        compCode+="0";
                return compCode + newCode;
            }
        }
    }

    public enum AppOperations { Create, Update, Delete, Stop, Print, List }
}
