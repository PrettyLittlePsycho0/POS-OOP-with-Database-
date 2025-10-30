namespace ShopManagementSystem.Common
{
    internal static class FileUtils
    {
        public static string GetPath(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\", fileName);
            return Path.GetFullPath(path);
        }

        public static bool IsFileEmpty(string filePath)
        {
            if (!File.Exists(filePath)) return true;

            foreach (string line in File.ReadLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line)) return false;
            }

            return true;
        }
        
        public static string ParseRecord(string record, int field, char separator)
        {
            string[] fields = record.Split(separator);
            if (field - 1 < fields.Length) return fields[field - 1];
            return string.Empty;
        }
    }
}