using Zad3.Models.Base;
using Zad3.Models.Exceptions;

namespace Zad3.Models.ContainerTypes;

public class LiquidContainer  : Container, IHazardNotifier
{

    private static List<string> dangerousCargo;
    

    public LiquidContainer( double containerWeight, double depth, double maxWeight)
        : base( containerWeight, depth,  maxWeight)
    {
        dangerousCargo = new List<string>()
        {
            "petrol"
        };
    }
    

    public bool IsDangerous { get; protected set; }
    



    public override void LoadCargo(double cargoWeight, string cargoType)
    {
        bool isCargoBeingLoadedDangerous = dangerousCargo.Contains(cargoType) ? true : false;
        if (cargoWeight + CurrentWeight > MaxWeight)
        {
            throw new OverfillException("cargo with weight of: " + cargoWeight + " is too heavy for this container, max weight is: " + MaxWeight);
            
        }
        if (isCargoBeingLoadedDangerous && cargoWeight  + CurrentWeight > (0.5 * MaxWeight))
        {
            this.NotifyAboutHazard();
            //throw new OverfillException("cargo is dangerous, you can only fill 50% of the max load which is " +
            //(0.5 * MaxWeight) + " and you're trying to load " + cargoWeight);
        }
        if (!isCargoBeingLoadedDangerous && cargoWeight + CurrentWeight > (0.9 * MaxWeight))
        {
            this.NotifyAboutHazard();
            //throw new OverfillException("this is a liquid container, you can only fill 90% of the max load which is " +
                                        //(0.9 * MaxWeight) + " and you're trying to load " + cargoWeight);
        }

        IsDangerous = isCargoBeingLoadedDangerous;
        CargoType = cargoType;
        CurrentWeight += cargoWeight;
        
    }


    public string NotifyAboutHazard()
    {
        return "dangerous event in container: " + GetSerialNumber();
    }
    
}