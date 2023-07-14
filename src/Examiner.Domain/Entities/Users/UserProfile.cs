using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Examiner.Domain.Entities.Content;

namespace Examiner.Domain.Entities.Users;

public class UserProfile : BaseEntity
{

    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public UserIdentity User { get; set; } = null!;

    [MaxLength(30)]
    public string? FirstName { get; set; }

    [MaxLength(30)]
    public string? LastName { get; set; }
    public HashSet<Subject>? Subjects { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    public int CountryId { get; set; }
    public int StateId { get; set; }
    public string? Address { get; set; }
    [StringLength(10)]
    public string? MobilePhone { get; set; }

    [JsonIgnore]
    public DateTime? LastMobilePhoneVerification { get; set; }

    [JsonIgnore]
    public bool IsMobilePhoneVerified => LastMobilePhoneVerification.HasValue;
    public bool IsAvailable { get; set; }
    public DateTime? LastAvailability { get; set; }
    public string? ProfilePhotoPath { get; set; }
    public string? DegreeCertificatePath { get; set; }
    public string? ShortDescription { get; set; }
    public int ExperienceLevelId { get; set; }
    public int EducationDegreeId { get; set; }
}