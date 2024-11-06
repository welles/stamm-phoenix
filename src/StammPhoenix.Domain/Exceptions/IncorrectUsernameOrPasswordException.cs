namespace StammPhoenix.Domain.Exceptions;

public class IncorrectUsernameOrPasswordException()
    : StatusCodeException(401, "Username or password is incorrect");
