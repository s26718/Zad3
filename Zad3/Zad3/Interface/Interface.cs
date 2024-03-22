using Zad3.Models.Base;
using Zad3.Models.ContainerTypes;

namespace Zad3.Interface;

public class Interface
{
    private List<Container> Containers { get; set; }
    private List<ContainerShip> ContainerShips { get; set; }
    public Interface()
    {
        Containers = new List<Container>() { new GasContainer(13, 13, 13, 13) };
        //Containers = new List<Container>();
        ContainerShips = new List<ContainerShip>() { new ContainerShip(10, 100, 100000) };
        //ContainerShips = new List<ContainerShip>();
    }

    public void Run()
    {
        while (true)
        {
            DisplayMainMenu();
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddContainerShip();
                    break;
                case "2":
                    ManageAddContainerShipMenu();
                    break;
                case "3":
                    
                    if (ContainerShips.Count != 0)
                    {
                        Console.WriteLine("usuwamy");
                        DeleteContainerShip();
                    }
                    else
                    {
                        Console.WriteLine("podaj poprawna opcje, sprobuj ponownie");
                    }
                    
                    break;
                case "4":
                    if (Containers.Count != 0)
                    {
                        DeleteContainer();
                    }
                    else
                    {
                        Console.WriteLine("podaj poprawna opcje, sprobuj ponownie");
                    }
                    
                    break;
                case "5":
                    if (ContainerShips.Count != 0 && Containers.Count != 0)
                    {
                        PutContainerOnShip();
                    }
                    else
                    {
                        Console.WriteLine("podaj poprawna opcje, sprobuj ponownie");
                    }

                    break;
                    
                case "b":
                    return;
                default:
                    Console.WriteLine("podaj poprawna opcje, sprobuj ponownie");
                    break;
            }
        }
    }
    public void DisplayMainMenu()
    {
        Console.WriteLine("Lista kontenerowców:");
        ContainerShips.ForEach(ship =>
        {
            Console.WriteLine(ship.GetInfo());
        });
        Console.WriteLine("Lista kontenerów:");
        Containers.ForEach(container =>
        {
            Console.WriteLine(container.GetInfo());
        });
        Console.WriteLine("Możliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec:");
        Console.WriteLine("2. Dodaj kontener");
        if (ContainerShips.Count != 0)
        {
            Console.WriteLine("3. Usun kontenerowiec");
        }
        if (Containers.Count != 0)
        {
            Console.WriteLine("4. Usun kontener");
        }

        if (ContainerShips.Count != 0 && Containers.Count != 0)
        {
            Console.WriteLine("5. Umiesc kontener na jednym z kontenerowców");
        }
        
    }

    public void DisplayAddContainerShipMenu()
    {
        Console.WriteLine("\nMenu dodawania kontenerów:");
        Console.WriteLine("1. Dodaj kontener ciekłych substancji");
        Console.WriteLine("2. Dodaj kontener gazowy");
        Console.WriteLine("3. Dodaj kontener chłodniczy");
        Console.WriteLine("b. Powrót do poprzedniego menu");
    }

    public void AddContainerShip()
    {
        Console.WriteLine("Podaj maksymalna predkosc statku:");
        double maxSpeed = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalna ilosc kontenerow:");
        int maxContainerCount = int.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną ładowność statku w tonach:");
        double maxWeight = double.Parse(Console.ReadLine());


        ContainerShip containerShip = new ContainerShip(maxSpeed, maxContainerCount, maxWeight);
        ContainerShips.Add(containerShip);

        Console.WriteLine("Dodano nowy kontenerowiec.");
    }

    public void PutContainerOnShip()
    {
        Console.WriteLine("Lista kontenerów:");
        Containers.ForEach(container => { Console.WriteLine(container.GetInfo()); });
        Console.WriteLine("podaj id kontenera do wlozenia na statek:");
        string idContainerToMove = Console.ReadLine().ToUpper();
        Container containerToLoad = Containers.Find(container => container.GetSerialNumber().Equals(idContainerToMove));
        if (containerToLoad == null)
        {
            Console.WriteLine("Nie ma takiego kontenera");
            return;
        }
        Console.WriteLine("Lista statków:");
        ContainerShips.ForEach(ship => { Console.WriteLine(ship.GetInfo()); });
        Console.WriteLine("podaj id statku, zeby wlozyc kontener:");
        string idShip = Console.ReadLine().ToUpper();
        ContainerShip shipToLoad = ContainerShips.Find(ship => ship.Id.ToString().Equals(idShip));
        if (shipToLoad == null)
        {
            Console.WriteLine("Nie ma takiego statku");
            return;
        }

        if (shipToLoad.LoadShipWithContainer(containerToLoad))
        {
            Containers.Remove(containerToLoad);
        }
        else
        {
            Console.WriteLine("nie udalo zaladowac się kontenera");
        }

    }

    public void ManageAddContainerShipMenu()
    {
        while (true)
        {
            DisplayAddContainerShipMenu();
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Dodajesz kontener ciekłych substancji");
                    AddLiquidContainer();
                    break;
                case "2":
                    Console.WriteLine("Dodajesz kontener gazowy");
                    AddGasContainer();
                    break;
                case "3":
                    Console.WriteLine("Dodajesz kontener chłodniczy");
                    AddCoolingContainer();
                    break;
                case "b":
                    return;
                default:
                    Console.WriteLine("podaj poprawna opcje, sprobuj ponownie");
                    break;
                
            }
        }
    }
    private void AddCoolingContainer()
    {
        Console.WriteLine("Podaj wagę kontenera:");
        double containerWeight = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj głębokość kontenera:");
        double depth = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną wagę ładunku kontenera:");
        double maxCargoWeight = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj temperaturę kontenera:");
        double temperature = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj dozwolony rodzaj ładunku kontenera:");
        string allowedCargoType = Console.ReadLine();

        CoolingContainer coolingContainer = new CoolingContainer(containerWeight, depth, maxCargoWeight, temperature, allowedCargoType);
        Containers.Add(coolingContainer);

        Console.WriteLine("Dodano nowy kontener chłodniczy.");
    }
    private void AddLiquidContainer( )
    {
        Console.WriteLine("Podaj wagę kontenera:");
        double containerWeight = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj głębokość kontenera:");
        double depth = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną wagę ładunku kontenera:");
        double maxCargoWeight = double.Parse(Console.ReadLine());

        LiquidContainer liquidContainer = new LiquidContainer(containerWeight, depth, maxCargoWeight);
        Containers.Add(liquidContainer);

        Console.WriteLine("Dodano nowy kontener ciekłych substancji.");
    }

    private void AddGasContainer()
    {
        Console.WriteLine("Podaj wagę kontenera:");
        double containerWeight = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj głębokość kontenera:");
        double depth = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj maksymalną wagę ładunku kontenera:");
        double maxCargoWeight = double.Parse(Console.ReadLine());

        Console.WriteLine("Podaj ciśnienie kontenera:");
        double pressure = double.Parse(Console.ReadLine());

        GasContainer gasContainer = new GasContainer(containerWeight, depth, maxCargoWeight, pressure);
        Containers.Add(gasContainer);

        Console.WriteLine("Dodano nowy kontener gazowy.");
    }

    private void DeleteContainer()
    {
        Console.WriteLine("Lista kontenerów:");
        Containers.ForEach(container => { Console.WriteLine(container.GetInfo()); });
        Console.WriteLine("podaj id kontenera do usuniecia:");
        string idToDelete = Console.ReadLine().ToUpper();
        Container containerToDelete = Containers.Find(container => container.GetSerialNumber().Equals(idToDelete));
        if (Containers.Remove(containerToDelete))
        {
            Console.WriteLine("Usunieto.");
        }
        else
        {
            Console.WriteLine("Nie ma takiego kontenera");
        }
    }
    private void DeleteContainerShip()
    {
        Console.WriteLine("Lista statków:");
        ContainerShips.ForEach(ship => { Console.WriteLine(ship.GetInfo()); });
        Console.WriteLine("podaj id statku do usuniecia:");
        string idToDelete = Console.ReadLine().ToUpper();
        ContainerShip shipToDelete = ContainerShips.Find(ship => ship.Id.Equals(idToDelete));
        if (ContainerShips.Remove(shipToDelete))
        {
            Console.WriteLine("Usunieto.");
        }
        else
        {
            Console.WriteLine("Nie ma takiego statku");
        }
    }
}