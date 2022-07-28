using StudentAppMvc.Models;
using StudentAppMvc.Models.DTO;

namespace StudentAppMvc.ExtensionMethods
{
    public static class SchoolExtensionMethods
    {
        public static ClassDto ToClassDto(this Class givenClass) 
        {
            return new ClassDto()
            {
                Id = givenClass.Id,
                Name = givenClass.Name,
                DepartmentCode = givenClass.DepartmentCode,
                DepartmentName = givenClass.Department?.Name
            };
        }

        public static DepartmentDto ToDepartmentDto(this Department department) 
        {
            return new DepartmentDto()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
            };
        }
    }
}
