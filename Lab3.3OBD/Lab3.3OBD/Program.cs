using System;
using System.Collections.Generic;

public interface IDriveable
{
    void Move();
    void Stop();
}

public class Road
{
    public string Name { get; set; }
    public double Length { get; set; }
    public int Lanes { get; set; }  
    public double TrafficLevel { get; set; } 

    public Road(string name, double length, int lanes)
    {
        Name = name;
        Length = length;
        Lanes = lanes;
        TrafficLevel = 0.5; 
    }

    public void SetTrafficLevel(double level)
    {
        TrafficLevel = Math.Max(0, Math.Min(1, level));
    }
}

public class Vehicle : IDriveable
{
    public string Type { get; set; }
    public double Speed { get; set; } 
    public bool IsMoving { get; set; } 

    public Vehicle(string type, double speed)
    {
        Type = type;
        Speed = speed;
        IsMoving = false;
    }

    public void Move()
    {
        if (!IsMoving)
        {
            Console.WriteLine($"{Type} почав рухатися зі швидкістю {Speed} км/год.");
            IsMoving = true;
        }
    }

    public void Stop()
    {
        if (IsMoving)
        {
            Console.WriteLine($"{Type} зупинився.");
            IsMoving = false;
        }
    }
}

public class TrafficSimulation
{
    private Road Road { get; set; }
    private List<Vehicle> Vehicles { get; set; }

    public TrafficSimulation(Road road)
    {
        Road = road;
        Vehicles = new List<Vehicle>();
    }

    public void AddVehicle(Vehicle vehicle)
    {
        Vehicles.Add(vehicle);
    }

    public void SimulateTraffic()
    {
        Console.WriteLine($"Симуляція на дорозі: {Road.Name} з рівнем трафіку {Road.TrafficLevel * 100}%.\n");

        foreach (var vehicle in Vehicles)
        {
            if (Road.TrafficLevel < 0.3)
            {
                vehicle.Move();
            }
            else if (Road.TrafficLevel >= 0.3 && Road.TrafficLevel < 0.7)
            {
                Console.WriteLine($"{vehicle.Type} рухається, але з перешкодами через середній трафік.");
                vehicle.Move();
            }
            else
            {
                vehicle.Stop();
                Console.WriteLine($"{vehicle.Type} не може рухатись через високий рівень трафіку.");
            }
        }
        Console.WriteLine();
    }
    public void AdjustTraffic(double newLevel)
    {
        Road.SetTrafficLevel(newLevel);
        Console.WriteLine($"Рівень трафіку на дорозі {Road.Name} був змінений на {newLevel * 100}%.\n");
    }
}

public class Program
{
    public static void Main()
    {
        var road1 = new Road("Дорога 1", 10, 4);

        var car = new Vehicle("Автомобіль", 80);
        var truck = new Vehicle("Вантажівка", 60);
        var bus = new Vehicle("Автобус", 50);

        var trafficSimulation = new TrafficSimulation(road1);

        trafficSimulation.AddVehicle(car);
        trafficSimulation.AddVehicle(truck);
        trafficSimulation.AddVehicle(bus);

        trafficSimulation.SimulateTraffic();

        trafficSimulation.AdjustTraffic(0.8);  
        trafficSimulation.SimulateTraffic(); 
    }
}
