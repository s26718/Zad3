namespace Zad3.Models.Exceptions;

public class OverfillException : Exception
{
    public OverfillException()
    {
        
    }
    public OverfillException(string message) : base(message){}
        
}