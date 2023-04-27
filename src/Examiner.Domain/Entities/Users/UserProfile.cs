using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Examiner.Domain.Entities.Users;

public class UserProfile : BaseEntity
{

    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public UserIdentity User { get; set; } = null!;

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