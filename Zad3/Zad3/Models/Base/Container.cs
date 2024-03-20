
using Zad3.Models.Exceptions;

namespace Zad3.Models.Base;

public abstract class Container
{
    protected static int idGenerator;
    public string CargoType { get; protected set; }
    public double CurrentWeight { get; set ; }
    public double ContainerWeight { get; }
    public double Depth { get; }
    protected int Id { get;  set; }
    public double MaxWeight { get; }

    protected Container(double containerWeight, double depth,  double maxWeight)
    {
        ContainerWeight = containerWeight;
        Depth = depth;
        Id = ++idGenerator;
        MaxWeight = maxWeight;
    }

    public virtual void EmptyContainer()
    {
        CurrentWeight = 0;
    }

    public virtual string GetSerialNumber()
    {
        return "KON-" + this.GetType().Name[0] + "-" + Id;
    }

    public virtual string GetInfo()
    {
        return "Container " + GetSerialNumber() + " contains "
    }

    public virtual void LoadCargo(double cargoWeight, string cargoType)
    {
        if (cargoWeight  + CurrentWeight > MaxWeight)
        {
            throw new OverfillException("cargo with weight of: " + cargoWeight + " is too heavy for this container, max weight is: " + MaxWeight);
            
        }
        else
        {
            CargoType = cargoType;
            CurrentWeight += cargoWeight;
        }
    }
    
}