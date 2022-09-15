using StudentAppMvc.Services.ValidationRule;

namespace StudentAppMvc.Models.ViewModels
{
    public class DepartmentValidationRule : IValidationRule
    {
        public ValidationRuleResult Validate(Student student)
        {
            ValidationRuleResult result = new ValidationRuleResult();
           var department = student.Department;
            if (department == null || department.Length < 5 )
            {
                result.IsSuccess = false;
                result.Message = "Department is not null and its length must be at least 5 characters";
            }
            return result;
        }
    }
}
