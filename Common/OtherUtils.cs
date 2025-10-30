namespace ShopManagementSystem.Common
{
    internal static class OtherUtils
    {
        public static bool IsAllDigits(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsDigit(s[i]))
                {
                    return false;
                }
            }
            return s.Length > 0;
        }
    }
}