using System;
using System.Collections.Generic;
using System.Linq;

public interface IReproducible
{
    void Reproduce();
}

public interface IPredator
{
    void Hunt(LivingOrganism target);
}

public class LivingOrganism
{
    public int Energy { get; set; }
    public int Age { get; set; }
    public int Size { get; set; }

    public LivingOrganism(int energy, int age, int size)
    {
        Energy = energy;
        Age = age;
        Size = size;
    }

    public void Grow()
    {
        Age++;
        Size++;
    }

    public void ConsumeEnergy(int amount)
    {
        Energy -= amount;
        if (Energy < 0) Energy = 0;
    }
}

public class Animal : LivingOrganism, IPredator, IReproducible
{
    public int Speed { get; set; }

    public Animal(int energy, int age, int size, int speed)
        : base(energy, age, size)
    {
        Speed = speed;
    }

    public void Hunt(LivingOrganism target)
    {
        if (target.Energy > 0)
        {
            Console.WriteLine($"Тварина полює на {target.GetType().Name}");
            target.ConsumeEnergy(10);
            Energy += 5;
        }
    }

    public void Reproduce()
    {
        if (Energy > 10)
        {
            Energy += 10;
            Console.WriteLine("Тварина розмножилася!");
        }
    }
}

public class Plant : LivingOrganism, IReproducible
{
    public int SunlightNeeded { get; set; }

    public Plant(int energy, int age, int size, int sunlightNeeded)
        : base(energy, age, size)
    {
        SunlightNeeded = sunlightNeeded;
    }

    public void Reproduce()
    {
        if (Energy > 5)
        {
            Energy += 5;
            Console.WriteLine("Рослина розмножилася!");
        }
    }
}

public class Microorganism : LivingOrganism, IReproducible
{
    public int ReproductionRate { get; set; }

    public Microorganism(int energy, int age, int size, int reproductionRate)
        : base(energy, age, size)
    {
        ReproductionRate = reproductionRate;
    }

    public void Reproduce()
    {
        if (Energy > 3)
        {
            Energy += ReproductionRate;
            Console.WriteLine("Мікроорганізм розмножився!");
        }
    }
}

public class Ecosystem
{
    private List<LivingOrganism> organisms;

    public Ecosystem()
    {
        organisms = new List<LivingOrganism>();
    }

    public void AddOrganism(LivingOrganism organism)
    {
        organisms.Add(organism);
    }

    public void Interaction()
    {
        foreach (var organism in organisms)
        {
            if (organism.Energy < 10)
            {
                Console.WriteLine($"{organism.GetType().Name} не має достатньо енергії для розмноження!");
            }
        }

        foreach (var predator in organisms.OfType<IPredator>())
        {
            foreach (var target in organisms.OfType<LivingOrganism>())
            {
                if (target != predator)
                {
                    predator.Hunt(target);
                }
            }
        }

        foreach (var organism in organisms.OfType<IReproducible>())
        {
            organism.Reproduce();
        }

        foreach (var organism in organisms)
        {
            organism.Grow();
        }
    }

    public void DisplayStatus()
    {
        foreach (var organism in organisms)
        {
            Console.WriteLine($"{organism.GetType().Name}: Енергія = {organism.Energy}, Вік = {organism.Age}, Розмір = {organism.Size}");
        }
    }
}

// Головний клас для запуску
public class Program
{
    public static void Main(string[] args)
    {
        var ecosystem = new Ecosystem();

        var animal = new Animal(100, 2, 50, 10);
        var plant = new Plant(80, 1, 30, 10);
        var microorganism = new Microorganism(50, 1, 5, 2);

        ecosystem.AddOrganism(animal);
        ecosystem.AddOrganism(plant);
        ecosystem.AddOrganism(microorganism);

        ecosystem.Interaction();

        ecosystem.DisplayStatus();
    }
}
