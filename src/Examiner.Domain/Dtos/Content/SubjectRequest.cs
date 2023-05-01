using System.ComponentModel.DataAnnotations;

namespace Examiner.Domain.Dtos.Content;

public record SubjectRequest(
    [Required]
    int categoryId
);