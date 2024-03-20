using Zad3.Models.Base;
using Zad3.Models.Exceptions;

namespace Zad3.Models.ContainerTypes;

public class LiquidContainer  : Container, IHazardNotifier
{

    private static List<string> dangerousCargo;
    

    public LiquidContainer( double containerWeight, double depth, double maxCargoWeight)
        : base( containerWeight, depth,  maxCargoWeight)
    {
        dangerousCargo = new List<string>()
        {
            "Petrol","Hydrogen"
        };
    }
    

    public bool IsDangerous { get; protected set; }
    



    public override void LoadCargo(double cargoWeight, string cargoType)
    {
        bool isCargoBeingLoadedDangerous = dangerousCargo.Contains(cargoType);
        if (cargoWeight + CurrentCargoWeight > MaxCargoWeight)
        {
            throw new OverfillException("cargo with weight of: " + cargoWeight + " is too heavy for this container, max cargo weight is: " + MaxCargoWeight);
            
        }
        if (isCargoBeingLoadedDangerous && cargoWeight  + CurrentCargoWeight > (0.5 * MaxCargoWeight))
        {
            this.NotifyAboutHazard();

        }
        if (!isCargoBeingLoadedDangerous && cargoWeight + CurrentCargoWeight > (0.9 * MaxCargoWeight))
        {
            this.NotifyAboutHazard();

        }

        IsDangerous = isCargoBeingLoadedDangerous;
        CurrentCargoWeight += cargoWeight;
        
    }


    public string NotifyAboutHazard()
    {
        Console.WriteLine("dangerous event in container: " + GetSerialNumber());
        return "dangerous event in container: " + GetSerialNumber();
    }
    
}