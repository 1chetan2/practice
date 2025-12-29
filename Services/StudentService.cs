using firstprogram.Data;
using firstProgram.Models;
using firstProgram.ViewModels;
using Microsoft.EntityFrameworkCore;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public PaginatedList<Student> GetStudents(
        int page,
        int pageSize,
        string searchString,
        string sortColumn,
        string sortOrder)
    {
        IQueryable<Student> query = _context.Students.AsNoTracking();

        // search
        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(s =>
                s.StudentName.Contains(searchString) ||
                s.Email.Contains(searchString) ||
                s.Course.Contains(searchString));
        }

        // sorting
        query = sortColumn switch
        {
            "StudentName" => sortOrder == "asc"
                ? query.OrderBy(s => s.StudentName)
                : query.OrderByDescending(s => s.StudentName),

            "Email" => sortOrder == "asc"
                ? query.OrderBy(s => s.Email)
                : query.OrderByDescending(s => s.Email),

            "Course" => sortOrder == "asc"
                ? query.OrderBy(s => s.Course)
                : query.OrderByDescending(s => s.Course),

            "EnrollmentDate" => sortOrder == "asc"
                ? query.OrderBy(s => s.EnrollmentDate)
                : query.OrderByDescending(s => s.EnrollmentDate),

            _ => sortOrder == "asc"
                ? query.OrderBy(s => s.Id)
                : query.OrderByDescending(s => s.Id)
        };

        int totalCount = query.Count();

        var students = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedList<Student>(
            students, totalCount, page, pageSize, sortColumn, sortOrder);
    }

    //for lazyLoading
    public List<Student> LoadMoreStudents(
        int page,
        int pageSize,
        string searchString,
        string sortColumn,
        string sortOrder)
    {
        IQueryable<Student> query = _context.Students.AsNoTracking();

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(s =>
                s.StudentName.Contains(searchString) ||
                s.Email.Contains(searchString) ||
                s.Course.Contains(searchString));
        }

        query = sortColumn switch
        {
            "StudentName" => sortOrder == "asc"
                ? query.OrderBy(s => s.StudentName)
                : query.OrderByDescending(s => s.StudentName),

            "Email" => sortOrder == "asc"
                ? query.OrderBy(s => s.Email)
                : query.OrderByDescending(s => s.Email),

            "Course" => sortOrder == "asc"
                ? query.OrderBy(s => s.Course)
                : query.OrderByDescending(s => s.Course),

            "EnrollmentDate" => sortOrder == "asc"
                ? query.OrderBy(s => s.EnrollmentDate)
                : query.OrderByDescending(s => s.EnrollmentDate),

            _ => sortOrder == "asc"
                ? query.OrderBy(s => s.Id)
                : query.OrderByDescending(s => s.Id)
        };

        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public Student GetById(int id)
    {
        return _context.Students.Find(id);
    }

    public void Create(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public void Update(Student student)
    {
        _context.Students.Update(student);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var student = _context.Students.Find(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}
