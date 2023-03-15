using Examiner.Common.Dtos;

namespace Examiner.Authentication.Domain.Dtos;

public class UserResponse:GenericResponse{

    public UserResponse():base(false,null)
    {
        
    }

    public UserResponse(bool success, string? message): base(success, message){}

    public Guid? Id {get;set;}
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }
    public string? MobilePhone { get; set; }
}