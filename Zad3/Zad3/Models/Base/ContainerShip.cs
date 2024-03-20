namespace Zad3.Models.Base;

public class ContainerShip
{
    private List<Container> containers;
    public double MaxSpeed { get; protected set; }
    public int MaxContainerCount { get; }
    public double MaxWeight { get; }

    public ContainerShip(double maxSpeed, int maxContainerCount, double maxWeight)
    {
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
            sum += container.CurrentWeight + container.ContainerWeight;
        });
        return sum;
    }

    public bool LoadShipWithContainer(Container container)
    {
        if (containers.Count == MaxContainerCount)
        {
            Console.WriteLine("this ship has maximum amount of containers loaded: " + containers.Count);
            return false;
        }

        if (GetCurrentCargoWeight() + container.ContainerWeight + container.ContainerWeight > MaxWeight)
        {
            Console.WriteLine("this ship has max load of " + MaxWeight + " tonnes, adding container " + container.GetSerialNumber());
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
            if (!this.LoadShipWithContainer(containersToBeLoaded[i]))
            {
                //unload the containers loaded up to now
                for (int j = 0; j < i ; j++)
                {
                    RemoveContainer(containersToBeLoaded[j]);
                }
                Console.WriteLine("container " + containers[i].GetSerialNumber() + " could not be loaded, cancelling the loading and unloading the previously loaded containers");
                return false;
            }
        }

        return true;
    }

    public void UnloadContainer(Container container)
    {
        if (containers.Contains(container))
        {
            container.EmptyContainer();
        }
        else
        {
            Console.WriteLine("ship does not contain container " + container.GetSerialNumber());
        }
    }

    public bool ReplaceContainers(Container containerToRemove, Container containerToLoad)
    {
        if (!containers.Contains(containerToRemove))
        {
            Console.WriteLine("ship does not contain container " + containerToRemove.GetSerialNumber());
            return false;
        }
        RemoveContainer(containerToRemove);
        if (!LoadShipWithContainer(containerToLoad))
        {
            Console.WriteLine("could not load container " + containerToLoad.GetSerialNumber() + " , loading the first one back");
            LoadShipWithContainer(containerToRemove);
            return false;
        }

        return true;
    }

    public static bool MoveContainerBetweenShip(ContainerShip shipFrom, ContainerShip shipTo, Container containerToMove)
    {
        if(!shipFrom.RemoveContainer(containerToMove))
        {
            Console.WriteLine("container " + containerToMove.GetSerialNumber()+   " was not on the ship");
            return false;
        }

        if (!shipTo.LoadShipWithContainer(containerToMove))
        {
            Console.WriteLine("could not load container " + containerToMove.GetSerialNumber() + ", loading it back to original ship");
            shipFrom.LoadShipWithContainer(containerToMove);
            return false;
        }

        return true;


    }
    
}