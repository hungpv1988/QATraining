using StudentAppMvc.Models;

namespace StudentAppMvc.Services.ValidationRule
{
    public class NameValidationRule : IValidationRule
    {
        public ValidationRuleResult Validate(Student student)
        {
            var name = student.Name;
            if (name == null || name.Length > 20)
            {

                return new ValidationRuleResult()
                {
                    IsSuccess = false,
                    Message = "Name must exist and its length need to be less than 20 characters"
                };
            }

            return new ValidationRuleResult() { IsSuccess = true };
        }
    }
}
