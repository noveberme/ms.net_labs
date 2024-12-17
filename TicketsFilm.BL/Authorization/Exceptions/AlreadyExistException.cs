namespace TicketFilm.BL.Authorization.Exceptions;

public class AlreadyExistException : ApplicationException
{
    public AlreadyExistException(){}

    public AlreadyExistException(string message) : base(message){}
}