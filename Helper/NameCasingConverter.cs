namespace EmployeeManagementAPI.Helper
{
    public class NameCasingConverter
    {
        public static string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
