namespace Zad3.Models.Base;

public class ContainerShip
{
    private List<Container> containers;
    protected static int idGenerator;
    public int Id { get;   }
    public double MaxSpeed { get; protected set; }
    public int MaxContainerCount { get; }
    public double MaxWeight { get; }

    public ContainerShip(double maxSpeed, int maxContainerCount, double maxWeight)
    {
        Id = ++idGenerator;
        containers = new List<Container>();
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxWeight = maxWeight;
    }

    public double GetCurrentCargoWeight()
    {
        double sum = 0;
        containers.ForEach(container =>
        {
            sum += container.CurrentCargoWeight + container.ContainerWeight;
        });
        return sum;
    }

    public bool LoadShipWithContainer(Container container)
    {
        if (containers.Count == MaxContainerCount)
        {
            Console.WriteLine("ship " +Id+ " has maximum amount of containers loaded: " + containers.Count+ " so container " + container.GetSerialNumber() + " could not be loaded");
            return false;
        }

        if (GetCurrentCargoWeight() + container.CurrentCargoWeight + container.ContainerWeight > MaxWeight)
        {
            Console.WriteLine(" ship" +Id+ " has max load of " + MaxWeight + " tonnes, cannot add container " + container.GetSerialNumber() + " as it would make total weight " + (GetCurrentCargoWeight() + container.CurrentCargoWeight + container.ContainerWeight) + " tonnes");
            return false;
        }
        containers.Add(container);
        return true;
    }

    public bool RemoveContainer(Container container)
    {
        return containers.Remove(container);
    }

    public bool LoadShipWithContainers(List<Container> containersToBeLoaded)
    {
        for (int i = 0; i < containersToBeLoaded.Count; i++)
        {
            if (!LoadShipWithContainer(containersToBeLoaded[i]))
            {
                //unload the containers loaded up to now
                for (int j = 0; j < i ; j++)
                {
                    RemoveContainer(containersToBeLoaded[j]);
                }
                Console.WriteLine("container " + containers[i].GetSerialNumber() + " could not be loaded on ship "+Id+", cancelling the loading and unloading the previously loaded containers");
                return false;
            }
        }

        return true;
    }

    public Container GetContainerBySerialNumber(string serialNumber)
    {
        foreach(Container container in containers)
        {
            if (container.GetSerialNumber() == serialNumber)
            {
                return container;
            }
        }

        return null;
    }

    public void UnloadContainer(string containerSerialNumber)
    {
        Container containerToUnload = GetContainerBySerialNumber(containerSerialNumber);
        if (containerToUnload == null)
        {
            Console.WriteLine("ship " +Id+ " does not contain container " + containerSerialNumber + " so it could not be removed");
        }
        else
        {
            containerToUnload.EmptyContainer();
        }
        
       
    }

    public bool ReplaceContainers(string serialNumberContainerToRemove, Container containerToLoad)
    {
        Container containerToRemove = GetContainerBySerialNumber(serialNumberContainerToRemove);
        if (containerToRemove == null)
        {
            Console.WriteLine("ship "+Id+"does not contain container " + containerToRemove.GetSerialNumber());
            return false;
        }
        RemoveContainer(containerToRemove);
        if (!LoadShipWithContainer(containerToLoad))
        {
            Console.WriteLine("could not load container " + containerToLoad.GetSerialNumber() + " on ship " + Id +" , loading the first one back");
            LoadShipWithContainer(containerToRemove);
            return false;
        }
        Console.WriteLine("successfully replaced container " + containerToRemove.GetSerialNumber() + " with container " + containerToLoad.GetSerialNumber()+ " on ship " +Id );
        return true;
    }

    public static bool MoveContainerBetweenShip(ContainerShip shipFrom, ContainerShip shipTo, string SerialNumberContainerToMove)
    {
        Container containerToMove = shipFrom.GetContainerBySerialNumber(SerialNumberContainerToMove);
        if(containerToMove == null)
        {
            Console.WriteLine("container " + containerToMove.GetSerialNumber()+   " was not on the ship " + shipFrom.Id + ", so it could not be moved");
            return false;
        }
        
        if (!shipTo.LoadShipWithContainer(containerToMove))
        {
            Console.WriteLine("could not load container " + containerToMove.GetSerialNumber() + " on ship " + shipTo.Id + ", loading it back to original ship " + shipFrom.Id);
            shipFrom.LoadShipWithContainer(containerToMove);
            return false;
        }

        shipFrom.RemoveContainer(containerToMove);
        Console.WriteLine("successfully moved container " + containerToMove.GetSerialNumber() + " from ship " + shipFrom.Id + " to ship " + shipTo.Id);

        return true;
    }

    public string GetInfo()
    {
       string info =  "Ship with id "+Id + " has max speed of " + MaxSpeed +" knots, it can have max " + MaxContainerCount +
           " containers, current load: " + GetCurrentCargoWeight() + " with max load of " + MaxWeight + " tonnes\n"+ containers.Count+" Containers on the ship: \n";
       containers.ForEach(container =>
       {
           info += "    "+ container.GetInfo();
       });
       
       return info;
    }
    
}