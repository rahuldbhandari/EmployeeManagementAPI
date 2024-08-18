namespace EmployeeManagementAPI.Models
{
    public class ValidatorResponseModel
    {
        public bool IsValid { get; set; }
        public Dictionary<string, string> ValidationErrors { get; }

        public ValidatorResponseModel()
        {
            ValidationErrors = new Dictionary<string, string>();
        }

        public bool AddValidationError(string controlName, string errorMsg)
        {
            bool isAdded = false;
            if (!ValidationErrors.ContainsKey(controlName))
            {
                ValidationErrors.Add(controlName, errorMsg);
                isAdded = true;
            }

            return isAdded;
        }
    }
}
