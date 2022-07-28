using StudentAppMvc.Models.DTO;
using StudentAppMvc.Models.ViewModel;

namespace StudentAppMvc.Services
{
    public interface ISchoolService
    {
        #region class
        ClassDto GetClass(int id);

        ClassDto AddClass(ClassDto classDto);

        ClassDto UpdateClass(ClassDto classDto);

        List<ClassDto> ListClasses();

        ClassDto DeleteClass(int id);

        #endregion

        #region department
        DepartmentDto GetDepartment(string code);

        DepartmentDto AddDepartment(DepartmentDto classDto);

        DepartmentDto UpdateDepartment(DepartmentDto classDto);

        List<DepartmentDto> ListDepartments();

        DepartmentDto DeleteDepartment(string code);

        #endregion

    }
}
