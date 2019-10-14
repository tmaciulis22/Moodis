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
        //SignIn Erros
        UserNotFound,
        //Registration and singIn errors
        SerializationError,
    }
}
