using StudentAppMvc.Models;

namespace StudentAppMvc.Services.ValidationRule
{
    public class NameValidationRule : IValidationRule
    {
        public ValidationRuleResult Validate(Student student)
        {
            var name = student.Name;
            if (string.IsNullOrEmpty(name) || name.Length <=20) 
            {
                return new ValidationRuleResult()
                {
                    IsSuccessful = false,
                    Message = "Name must exist and its length need to be at least 20 characters"
                };
            }

            return new ValidationRuleResult() { IsSuccessful = true };
        }
    }
}
