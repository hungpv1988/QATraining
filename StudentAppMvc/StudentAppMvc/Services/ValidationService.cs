using StudentAppMvc.Models;
using StudentAppMvc.Services.ValidationRule;

namespace StudentAppMvc.Services
{
    public class CustomValidationService  
    {
        private ValidationService _validationService;
        public virtual IEnumerable<ValidationRuleResult> Validate(Student student) 
        {
            // do extra things
           return _validationService.Validate(student);
        }
    }
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
            {
                return Enumerable.Empty<ValidationRuleResult>();
            }

            var validationResults = new List<ValidationRuleResult>();
            foreach (var validationRule in _validationRules) 
            {
                var result = validationRule.Validate(student);
                if (result.IsSuccessful) 
                {
                    continue;
                }
                validationResults.Add(result);
            }

            return validationResults;
        }
    }
}
