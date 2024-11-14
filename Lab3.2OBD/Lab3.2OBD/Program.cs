using System;
using System.Collections.Generic;

public interface IConnectable
{
    void Connect(Computer other);
    void Disconnect(Computer other);
    void SendData(string data, IConnectable receiver);
    void ReceiveData(string data);
}

public class Computer
{
    public string IPAddress { get; set; }
    public int Power { get; set; } 
    public string OperatingSystem { get; set; }

    public Computer(string ipAddress, int power, string operatingSystem)
    {
        IPAddress = ipAddress;
        Power = power;
        OperatingSystem = operatingSystem;
    }
}

public class Server : Computer, IConnectable
{
    public Server(string ipAddress, int power, string operatingSystem)
        : base(ipAddress, power, operatingSystem) { }

    private List<Computer> connectedDevices = new List<Computer>();

    public void Connect(Computer other)
    {
        connectedDevices.Add(other);
        Console.WriteLine($"Сервер підключився до комп'ютера з IP {other.IPAddress}");
    }

    public void Disconnect(Computer other)
    {
        connectedDevices.Remove(other);
        Console.WriteLine($"Сервер відключився від комп'ютера з IP {other.IPAddress}");
    }

    public void SendData(string data, IConnectable receiver)
    {
        Console.WriteLine($"Сервер відправляє дані до комп'ютера з IP {((Computer)receiver).IPAddress}: {data}");
        receiver.ReceiveData(data);
    }

    public void ReceiveData(string data)
    {
        Console.WriteLine($"Сервер отримав дані: {data}");
    }
}

public class Workstation : Computer, IConnectable
{
    public Workstation(string ipAddress, int power, string operatingSystem)
        : base(ipAddress, power, operatingSystem) { }

    private List<Computer> connectedDevices = new List<Computer>();

    public void Connect(Computer other)
    {
        connectedDevices.Add(other);
        Console.WriteLine($"Робоча станція підключилася до комп'ютера з IP {other.IPAddress}");
    }

    public void Disconnect(Computer other)
    {
        connectedDevices.Remove(other);
        Console.WriteLine($"Робоча станція відключилася від комп'ютера з IP {other.IPAddress}");
    }

    public void SendData(string data, IConnectable receiver)
    {
        Console.WriteLine($"Робоча станція відправляє дані до комп'ютера з IP {((Computer)receiver).IPAddress}: {data}");
        receiver.ReceiveData(data);
    }

    public void ReceiveData(string data)
    {
        Console.WriteLine($"Робоча станція отримала дані: {data}");
    }
}

public class Router : Computer, IConnectable
{
    public Router(string ipAddress, int power, string operatingSystem)
        : base(ipAddress, power, operatingSystem) { }

    private List<Computer> connectedDevices = new List<Computer>();

    public void Connect(Computer other)
    {
        connectedDevices.Add(other);
        Console.WriteLine($"Маршрутизатор підключив комп'ютер з IP {other.IPAddress}");
    }

    public void Disconnect(Computer other)
    {
        connectedDevices.Remove(other);
        Console.WriteLine($"Маршрутизатор відключив комп'ютер з IP {other.IPAddress}");
    }

    public void SendData(string data, IConnectable receiver)
    {
        Console.WriteLine($"Маршрутизатор передає дані до комп'ютера з IP {((Computer)receiver).IPAddress}: {data}");
        receiver.ReceiveData(data);
    }

    public void ReceiveData(string data)
    {
        Console.WriteLine($"Маршрутизатор отримав дані: {data}");
    }
}

public class Network
{
    private List<IConnectable> devices = new List<IConnectable>();

    public void AddDevice(IConnectable device)
    {
        devices.Add(device);
        Console.WriteLine($"Пристрій з IP {((Computer)device).IPAddress} додано до мережі.");
    }

    public void RemoveDevice(IConnectable device)
    {
        devices.Remove(device);
        Console.WriteLine($"Пристрій з IP {((Computer)device).IPAddress} видалено з мережі.");
    }

    public void SendDataBetweenDevices(string data, IConnectable sender, IConnectable receiver)
    {
        sender.SendData(data, receiver);
    }

    public void ConnectDevices(IConnectable device1, IConnectable device2)
    {
        device1.Connect((Computer)device2);
        device2.Connect((Computer)device1);
    }

    public void DisconnectDevices(IConnectable device1, IConnectable device2)
    {
        device1.Disconnect((Computer)device2);
        device2.Disconnect((Computer)device1);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var server = new Server("192.168.0.1", 1000, "Linux");
        var workstation = new Workstation("192.168.0.2", 500, "Windows");
        var router = new Router("192.168.0.3", 200, "RouterOS");

        var network = new Network();

        network.AddDevice(server);
        network.AddDevice(workstation);
        network.AddDevice(router);

        network.ConnectDevices(server, workstation);
        network.ConnectDevices(workstation, router);

        network.SendDataBetweenDevices("Привіт, робоча станція!", server, workstation);
        network.SendDataBetweenDevices("Маршрутизатор, передай дані!", workstation, router);

        network.DisconnectDevices(server, workstation);
        network.DisconnectDevices(workstation, router);

        network.RemoveDevice(server);
        network.RemoveDevice(workstation);
        network.RemoveDevice(router);
    }
}
