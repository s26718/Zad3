using System.Xml.Serialization;
using Zad3.Models.Base;
using Zad3.Models.Exceptions;

namespace Zad3.Models.ContainerTypes;

public class LiquidContainer  : Container, IHazardNotifier
{  
    public LiquidContainer(double containerWeight, double depth, int id, double maxWeight)
    : base( containerWeight,  depth,  id,  maxWeight){}
    

    private bool IsDangerous { get; }
    

    protected override string GetSerialNumber(string containerType)
    {
        return base.GetSerialNumber("L");
    }

    protected override void LoadCargo(double cargoWeight)
    {
        if (this.IsDangerous && cargoWeight > (0.5 * MaxWeight))
        {
            this.NotifyAboutHazard();
            //throw new OverfillException("cargo is dangerous, you can only fill 50% of the max load which is " +
            //(0.5 * MaxWeight) + " and you're trying to load " + cargoWeight);
        }
        if (this.IsDangerous && cargoWeight > (0.9 * MaxWeight))
        {
            this.NotifyAboutHazard();
            throw new OverfillException("this is a liquid container, you can only fill 90% of the max load which is " +
                                        (0.9 * MaxWeight) + " and you're trying to load " + cargoWeight);
        }
    }


    public string NotifyAboutHazard()
    {
        return "dangerous event in container: " + this.GetSerialNumber("L");
    }
    
}