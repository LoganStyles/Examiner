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

    [MaxLength(20)]
    public string? FirstName { get; set; }

    [MaxLength(20)]
    public string? LastName { get; set; }
    [MaxLength(50)]
    public string? Location { get; set; }
    public HashSet<Subject>? Subjects { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    [StringLength(3)]
    public string? CountryCode { get; set; }
    [StringLength(10)]
    public string? MobilePhone { get; set; }

    [JsonIgnore]
    public DateTime? LastMobilePhoneVerification { get; set; }

    [JsonIgnore]
    public bool IsMobilePhoneVerified => LastMobilePhoneVerification.HasValue;

    public DateTime? LastAvailability {get; set;}
}