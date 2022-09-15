using StudentAppMvc.Models;

namespace StudentAppMvc.Services.ValidationRule
{
    public interface IValidationRule
    {
        ValidationRuleResult Validate(Student student);
    }
}
