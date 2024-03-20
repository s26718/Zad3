
using Zad3.Models.Exceptions;

namespace Zad3.Models.Base;

public abstract class Container
{
    private static int idGenerator;
    public double CurrentCargoWeight { get; set ; }
    public double ContainerWeight { get; }
    public double Depth { get; }
    protected int Id { get;   }
    public double MaxCargoWeight { get; }

    protected Container(double containerWeight, double depth,  double maxCargoWeight)
    {
        ContainerWeight = containerWeight;
        Depth = depth;
        Id = ++idGenerator;
        MaxCargoWeight = maxCargoWeight;
    }

    public virtual void EmptyContainer()
    {
        CurrentCargoWeight = 0;
    }

    public virtual string GetSerialNumber()
    {
        return "KON-" + this.GetType().Name[0] + "-" + Id;
    }

    public virtual string GetInfo()
    {
        return "Container " + GetSerialNumber() + " weighs " + ContainerWeight + " tonnes and is loaded with "+ CurrentCargoWeight + " tonnes, together:  "+(ContainerWeight + CurrentCargoWeight) +"\n";
    }

    public virtual void LoadCargo(double cargoWeight, string cargoType)
    {
        if (cargoWeight  + CurrentCargoWeight > MaxCargoWeight)
        {
            throw new OverfillException("cargo with weight of: " + cargoWeight + " is too heavy for this container, max cargo weight is: " + MaxCargoWeight);
            
        }
        {
            CurrentCargoWeight += cargoWeight;
        }
    }
    
}