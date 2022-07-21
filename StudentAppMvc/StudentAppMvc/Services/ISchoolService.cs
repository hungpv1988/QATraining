using StudentAppMvc.Models.DTO;
using StudentAppMvc.Models.ViewModel;

namespace StudentAppMvc.Services
{
    public interface ISchoolService
    {
        ClassDto GetClass(int id);

        List<ClassDto> ListClasses();

        List<DepartmentDto> ListDepartments();

        ClassDto AddClass(ClassDto classDto);

        ClassDto UpdateClass(ClassDto classDto);
    }
}
