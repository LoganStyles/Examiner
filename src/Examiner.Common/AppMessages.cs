namespace Examiner.Common;

public static class AppMessages
{

    #region context
    public const string REGISTRATION = "Registration";
    public const string AUTHENTICATION = "Authentication";
    public const string CHANGE_PASSWORD = "Change password";
    public const string ROLE = "Role select";
    public const string CODE_VERIFICATION = "Code verification";
    public const string CODE_CREATION = "Code creation";
    public const string CODE_SUPPLIED = "Supplied code";
    public const string CODE_RESEND = "Code resend";
    public const string EMAIL = "Email";
    public const string MOBILE_PHONE = "Mobile phone";
    public const string ROLE_TUTOR = "Tutor";
    public const string USER = "User";
    public const string SUBJECT_CATEGORY = "Subject Category";
    public const string COUNTRY = "Country";
    public const string STATE = "State";
    public const string SUBJECT = "Subject";
    public const string PROFILE_PHOTO = "Profile photo";

    #endregion
    public const string SUCCESSFUL = "successful";
    public const string FAILED = "failed";
    public const string UPDATE = "update";
    public const string EXPIRED = "expired";
    public const string REMOVAL = "removal";
    public const string INVALID_REQUEST = "Invalid request body";
    public const string EXISTS = "exists";
    public const string NOT_EXIST = "does not exist";
    public const string VALID = "is valid";
    public const string VERIFIED = "is verified";
    public const string NOT_VERIFIED = "not verified";
    public const string INVALID_FORMAT = "has invalid format";
    public const string NO_CODE_INITIATED = "has no code initiated";
    public const string INCOMPLETE = "incomplete";

    public const string SENDING = "sending";

    #region authentication
    public const string PASSWORDS_DO_NOT_MATCH = "Passwords do not match!";
    public const string INVALID_EMAIL_PASSWORD = "Invalid email or password";
    public const string UNABLE_TO_AUTHENTICATE_USER = "Unable to authenticate";
    public const string INVALID_PASSWORD = "Password: Expecting a minimum of 6 characters having a minimum of 1 uppercase, lowercase, and special characters";
    #endregion

    #region users
    public const string NOT_ACTIVE = "is not active!";
    public const string HAS_NO_ROLE = "has no role";
    public const string ACCOUNT_NOT_VERIFIED = "has not verified account";

    #endregion
    public const string UNABLE_TO_GENERATE_TOKEN = "Unable to generate token";

    #region exceptions
    public const string EXCEPTION_ERROR="An error occurred while ";
    #endregion

}