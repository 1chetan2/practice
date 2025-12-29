using firstProgram.Models;
using firstProgram.ViewModels;

public interface IStudentService
{
    PaginatedList<Student> GetStudents(
        int page,
        int pageSize,
        string searchString,
        string sortColumn,
        string sortOrder);

    List<Student> LoadMoreStudents(
        int page,
        int pageSize,
        string searchString,
        string sortColumn,
        string sortOrder);

    Student GetById(int id);
    void Create(Student student);
    void Update(Student student);
    void Delete(int id);
}
