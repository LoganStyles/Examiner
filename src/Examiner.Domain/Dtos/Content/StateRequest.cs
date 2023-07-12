using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Content;

public record StateRequest(
    [Required]
    int countryId
);