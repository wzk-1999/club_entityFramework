namespace zhankui_wang_Practice_Asst_5.Utilities
{
    public static class Utility
    {
        public static string PostalCode(string inputPostalCode)
        {

            return inputPostalCode.ToUpper().Replace(" ", "").Trim();

        }
    }
}
