using Zad3.Models.Base;

namespace Zad3.Models.ContainerTypes;

public class GasContainer : Container,IHazardNotifier
{
    public double Pressure { get; protected set; }
    public GasContainer( double containerWeight, double depth, double maxWeight, double pressure) 
        :base(containerWeight, depth, maxWeight)
    {
        Pressure = pressure;
    }
    


    public override void EmptyContainer()
    {
        CurrentWeight  *=  0.05;
    }

    public string NotifyAboutHazard()
    {
        return "dangerous event in container: " + GetSerialNumber();
    }
}