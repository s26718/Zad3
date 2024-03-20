using Zad3.Models.Base;

namespace Zad3.Models.ContainerTypes;

public class GasContainer : Container,IHazardNotifier
{
    public double Pressure { get; protected set; }
    public GasContainer( double containerWeight, double depth, double maxCargoWeight, double pressure) 
        :base(containerWeight, depth, maxCargoWeight)
    {
        Pressure = pressure;
    }
    


    public override void EmptyContainer()
    {
        CurrentCargoWeight  = CurrentCargoWeight* 0.05;
    }

    public string NotifyAboutHazard()
    {
        Console.WriteLine("dangerous event in container: " + GetSerialNumber());
        return "dangerous event in container: " + GetSerialNumber();
    }
}