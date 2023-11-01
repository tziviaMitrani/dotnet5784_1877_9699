
namespace DO;
/// <summary>
/// Student Entity represents a student with all its props
/// </summary>
/// <param name="Id">Personal unique ID of student (as in national id card)</param>
/// <param name="Name">Private name of the student</param>
/// <param name="Alias">Short name of the student </param>
/// <param name="IsActive">boolean flag, true if the student is active</param>
/// <param name="CurrentYear">current year of the student in the program</param>
/// <param name="BirthDate">student’s birth date</param>
public record Student
(
        int Id,
        string Name,
        string? Alias = null,
        bool IsActive = false,
        Year CurrentYear = Year.FirstYear,
        DateTime? BirthDate = null
)
{
    /// <summary>
    /// Date - creation date of the current student record
    /// </summary>
    public DateTime Date => DateTime.Now; //get only
}


/// <summary>
/// Course Entity
/// </summary>
/// <param name="Id">unique ID (created automatically)</param>
/// <param name="CourseNumber">course number as string, sample: 101-666-777</param>
/// <param name="Name">full name of the course</param>
/// <param name="Semester">semester name the course is given in</param>
public record Course
(
    int Id,
    string CourseNumber,
    string Name,
    SemesterNames Semester
);
 

