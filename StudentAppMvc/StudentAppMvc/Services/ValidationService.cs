using StudentAppMvc.Models;
using StudentAppMvc.Services.ValidationRule;

namespace StudentAppMvc.Services
{
    public class ValidationService
    {
        private IEnumerable<IValidationRule> _validationRules;

        public ValidationService(IEnumerable<IValidationRule> validationRules)
        { 
            _validationRules = validationRules;
        }

        public virtual IEnumerable<ValidationRuleResult> Validate(Student student)
        {
            if (_validationRules == null)
                //return Enumerable.Empty<ValidationRuleResult>();
                yield return new ValidationRuleResult();
            foreach (IValidationRule validationRule in _validationRules)
            {
                var result = validationRule.Validate(student);
                if (result.IsSuccess)
                {
                    yield break;
                }
                yield return result;
            }
        }

    }
}
