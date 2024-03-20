namespace Zad3.Models.Exceptions;

public class TemperatureException : Exception
{
    public TemperatureException()
    {
        
    }
    public TemperatureException(string message) : base(message){}
        
}