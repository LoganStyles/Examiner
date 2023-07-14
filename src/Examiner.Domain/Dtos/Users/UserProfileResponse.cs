using Examiner.Domain.Dtos;
using Examiner.Domain.Entities.Content;

namespace Examiner.Domain.Dtos.Users;

/// <summary>
/// Implements contract for a user response object
/// </summary>
public class UserProfileResponse : GenericResponse
{

    public UserProfileResponse() : base(false, null)
    {

    }

    public UserProfileResponse(bool success, string? message) : base(success, message) { }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    public int StateId { get; set; }
    public string? Address { get; set; }
    public string? MobilePhone { get; set; }
    public bool IsAvailable { get; set; }
    public string? ProfilePhotoPath { get; set; }
    public string? DegreeCertificatePath { get; set; }
    public string? ShortDescription { get; set; }
    public int SubjectCategoryId { get; set; }
    public int ExperienceLevelId { get; set; }
    public int EducationDegreeId { get; set; }

    #region supporting data
    public IEnumerable<Country>? Countries { get; set; }
    public IEnumerable<EducationDegree>? EducationDegrees { get; set; }
    public IEnumerable<ExperienceLevel>? ExperienceLevels { get; set; }
    public IEnumerable<State>? SelectedCountryStates { get; set; }
    public IEnumerable<SubjectCategory>? SubjectCategories { get; set; }

    #endregion
}