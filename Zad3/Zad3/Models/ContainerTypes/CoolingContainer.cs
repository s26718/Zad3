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

    public double Temperature { get; protected set; }
    public CoolingContainer(double containerWeight, double depth , double maxWeight, double temperature) 
        :base(containerWeight, depth, maxWeight)
    {
        Temperature = temperature;
    }
    


    public override void LoadCargo(double cargoWeight, string cargoType)
    {
        if ( CurrentWeight != 0 && cargoType != CargoType)
        {
            throw new NotAllowedCargoTypeException("this container can currently store only  " + CargoType +
                                                   " , so loading it with " + cargoType + " is impossible");
        }
        if (cargoWeight + CurrentWeight > MaxWeight)
        {
            throw new OverfillException("cargo with weight of: " + cargoWeight + " is too heavy for this container, max weight is: " + MaxWeight);
        }

        if (Temperature < requiredTemperatures[cargoType])
        {
            throw new TemperatureException("temperature of the container(" + Temperature + ") is too low for " +
                                           cargoType + ", lowest possible is " + requiredTemperatures[cargoType]);
        }
        
        else
        {
            CurrentWeight = cargoWeight;
            CargoType = cargoType;
        }
    }
}