using Zad3.Models.Base;
using Zad3.Models.Exceptions;

namespace Zad3.Models.ContainerTypes;

public class CoolingContainer : Container
{
    private static Dictionary<string, double> requiredTemperatures = new Dictionary<string,double>()
    {
        {"Bananas",13.3 },
        {"Chocolate",18 },
        {"Fish",2 },
        {"Meat",-15 },
        {"Ice cream",-18 },
        {"Frozen pizza",-30 },
        {"Cheese",7.2 },
        {"Sausages",5 },
        {"Eggs",19 },
    };
    public string AllowedCargoType { get; protected set; }

    public double Temperature { get; protected set; }
    public CoolingContainer(double containerWeight, double depth , double maxCargoWeight, double temperature, string allowedCargoType) 
        :base(containerWeight, depth, maxCargoWeight)
    {
        AllowedCargoType = allowedCargoType;
        Temperature = temperature;
    }
    


    public override void LoadCargo(double cargoWeight, string cargoType)
    {
        if ( CurrentCargoWeight != 0 && cargoType != AllowedCargoType)
        {
            Console.WriteLine("container " + GetSerialNumber() + " can currently store only  " + AllowedCargoType +
                                                   " , so loading it with " + cargoType + " is impossible");
            return;
        }
        if (cargoWeight + CurrentCargoWeight > MaxCargoWeight)
        {
            Console.WriteLine("cargo with weight of: " + cargoWeight + " is too heavy for this container, max weight is: " + MaxCargoWeight);
            return;
        }

        if (Temperature < requiredTemperatures[cargoType])
        {
            Console.WriteLine("temperature of the container "+ GetSerialNumber() + "(" + Temperature + ") is too low for " +
                                           cargoType + ", lowest possible is " + requiredTemperatures[cargoType]);
            return;
        }
        
        
        CurrentCargoWeight += cargoWeight; 
        AllowedCargoType = cargoType;
        
    }
}