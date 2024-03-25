using System;
using System.Collections.Generic;
using Zad3.Interface;
using Zad3.Models.Base;
using Zad3.Models.ContainerTypes;

class Program
{
    public static void TestRun()
    {
        // Tworzenie kontenerów
        Container liquidContainer = new LiquidContainer(1000, 5, 5000);
        Container gasContainer = new GasContainer(1200, 6, 4000, 100);
        Container coolingContainer = new CoolingContainer(1500, 7, 6000, 5, "Meat");
        // Ładowanie ładunku do kontenerów
        liquidContainer.LoadCargo(2000, "water");
        //liquidContainer.LoadCargo(6000, "petrol"); // Spowoduje OverfillException
        liquidContainer.EmptyContainer();
        //dangerous event, dangerous cargo więcej niż 50%
        liquidContainer.LoadCargo(3000, "Hydrogen");
        gasContainer.LoadCargo(3000, "Helium");
        gasContainer.EmptyContainer();
        //pozostaje 5%
        Console.WriteLine(gasContainer.GetInfo());
        coolingContainer.LoadCargo(1000, "Meat");
        //coolingContainer.EmptyContainer();
        coolingContainer.LoadCargo(1000, "Fish");
        //coolingContainer.LoadCargo(1000, "Fish"); // nieprawidlowy cargo - teraz jest meat, nie mozna razem
        coolingContainer.EmptyContainer();
        coolingContainer.LoadCargo(1000, "Chocolate"); //nieprawidlowa temperatura





        // Dodanie kontenerów na statek
        ContainerShip ship = new ContainerShip(20, 10, 800000);
        List<Container> ContainerList = new List<Container>();
        ContainerList.Add(liquidContainer);
        ContainerList.Add(gasContainer);
        ContainerList.Add(coolingContainer);

        ship.LoadShipWithContainers(ContainerList);
        Console.WriteLine("statek po załadowaniu listą 3 kontenerów:");
        Console.WriteLine(ship.GetInfo());
        ship.RemoveContainer(coolingContainer);
        Console.WriteLine("statek po usunięciu chłodzącego kontenera:");
        Console.WriteLine(ship.GetInfo());
        Console.WriteLine("próba rozładowania kontenera, którego nie ma na statku:");
        ship.UnloadContainer("jakis numer");
        Console.WriteLine("rozładowanie kontenera, który jest na statku:");
        ship.UnloadContainer("KON-L-1");
        Console.WriteLine(ship.GetInfo());

        ContainerShip smallShip = new ContainerShip(20, 2, 5000);
        //liquidContainer.LoadCargo(3000,"water");
        Console.WriteLine("Za dużo kontenerów na statek:");
        smallShip.LoadShipWithContainer(liquidContainer);
        smallShip.LoadShipWithContainer(coolingContainer);
        smallShip.LoadShipWithContainer(gasContainer);


        Console.WriteLine(smallShip.GetInfo());
        Console.WriteLine(ship.GetInfo());


        // Zastąpienie kontenera na statku innym kontenerem
        Container newContainer = new LiquidContainer(1200, 5, 5000);
        Console.WriteLine("Zastąpienie kontenera KON-L-1" + "na statku innym kontenerem " +
                          newContainer.GetSerialNumber());
        ship.ReplaceContainers("KON-L-1", newContainer);
        Console.WriteLine(ship.GetInfo());

        Console.WriteLine("=============================");




        // Przeniesienie kontenera między dwoma statkami
        ContainerShip anotherShip = new ContainerShip(25, 15, 90000);
        Console.WriteLine("Przeniesienie kontenera KON-L-4 na nowy statek " + anotherShip.Id);
        ContainerShip.MoveContainerBetweenShip(ship, anotherShip, "KON-L-4");

        Console.WriteLine(ship.GetInfo());
        Console.WriteLine(anotherShip.GetInfo());



    }
    

    static void Main(string[] args)
    {
        TestRun();
        ConsoleInterface interf = new ConsoleInterface();
        interf.Run();

    }
}