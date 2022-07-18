using StudentAppMvc.Models.DTO;

namespace StudentAppMvc.Services
{
    public interface ISchoolService
    {
        ClassDto Get(int id);

        List<ClassDto> ListClasses();

        List<DepartmentDto> ListDepartments();

        ClassDto AddClass(ClassDto classDto);
    }
}
