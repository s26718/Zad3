namespace Zad3.Models.Exceptions;

public class NotAllowedCargoTypeException : Exception
{
    public NotAllowedCargoTypeException()
    {
        
    }
    public NotAllowedCargoTypeException(string message) : base(message){}
        
}