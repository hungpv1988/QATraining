using StudentAppMvc.Models;

namespace StudentAppMvc.Services.ValidationRule
{
    public class DescriptionValidationRule : IValidationRule
    {
        public ValidationRuleResult Validate(Student student)
        {
            var desc = student.Description;
            if (string.IsNullOrEmpty(desc) || desc.Length >= 30)
            {
                return new ValidationRuleResult()
                {
                    IsSuccess = false,
                    Message = "Desc must exist and its length need to be at least 30 characters"
                };
            }

            return new ValidationRuleResult() { IsSuccess = true };
        }
    }
}
