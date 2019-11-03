namespace Moodis.Constants.Enums
{
    public enum Response
    {
        OK,
        GeneralError,
        ApiError,
        //Registration-specific errors
        UserExists,
        ApiTrainingError,
        RegistrationDone,
        //SignIn Errors
        UserNotFound,
        //Registration and singIn errors
        SerializationError,
    }
}
