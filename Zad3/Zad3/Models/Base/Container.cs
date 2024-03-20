
using Zad3.Models.Exceptions;

namespace Zad3.Models.Base;

public abstract class Container
{
    protected static int IdGenerator;
    public double CurrentWeight { get; set ; }
    public double ContainerWeight { get; }
    public double Depth { get; }
    protected int Id { get;  set; }
    public double MaxWeight { get; }

    protected Container(double containerWeight, double depth, int id, double maxWeight)
    {
        ContainerWeight = containerWeight;
        Depth = depth;
        Id = IdGenerator++;
        MaxWeight = maxWeight;
    }

    protected virtual void EmptyContainer()
    {
        this.CurrentWeight = 0;
    }

    protected virtual string GetSerialNumber(string containerType)
    {
        return "KON-" + containerType + "-" + this.Id;
    }

    protected virtual void LoadCargo(double cargoWeight)
    {
        if (cargoWeight > MaxWeight)
        {
            throw new OverfillException("cargo with weight of: " + cargoWeight + " is too heavy for this container, max weight is: " + MaxWeight);
            
        }
        else
        {
            this.CurrentWeight = cargoWeight;
        }
    }
    
    //metoda GetSerialNumber
}