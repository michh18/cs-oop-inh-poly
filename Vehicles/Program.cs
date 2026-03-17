namespace Vehicles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        public abstract class Vehicle
        { 
            public string Make { get; set; }
            public string Model { get; set; }
            public int Speed { get; set; }
            public Engine EngineType { get; set; }

            public Vehicle(string make, string model, Engine engineType)
            {
                Make = make;
                Model = model;
                EngineType = engineType;
            }

            public void StartEngine() 
            {
                EngineType.Start();
            }

            public void Drive() 
            {
                if (EngineType.Running)
                {
                    Accelerate();
                }
                else 
                {
                    Console.WriteLine($"{Make} {Model} is not running.");
                }
            }

            protected abstract void Accelerate();
        }

        // Creating Car subclass from Vehicle 
        public class Car : Vehicle 
        {
            public Car(string make, string model) : base(make, model, new CarEngine()) 
            { }

            protected override void Accelerate()
            {
                string message = $"{Make} {Model} speed: ";
                double fuelMod = EngineType.FuelType switch
                {
                    FuelType.Unleaded => 1.5D,
                    FuelType.Leaded => 1.2D,
                    FuelType.Diesel => 1.8D,
                    _ => throw new InvalidOperationException($"Unexpected value: {EngineType.FuelType}")
                };
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"{message}{((EngineType.Horsepower * fuelMod) * Math.Exp(Math.Sqrt(i)) / 2):F2}mph");
                }
            }
        }

        // Creating Motorcycle subclass from Vehicle 
        public class Motorcycle : Vehicle 
        {
            public bool HasSidecar { get; set; }
            public Motorcycle(string make, string model, bool hasSidecar) 
                : base(make, model, new MotorcycleEngine())
            {
                HasSidecar = hasSidecar;
            }

            protected  override void Accelerate()
            {
                string message = $"{Make} {Model} speed: ";
                double fuelMod = EngineType.FuelType switch
                {
                    FuelType.Unleaded => 1.5D,
                    FuelType.Leaded => 1.2D,
                    FuelType.Diesel => 1.8D,
                    _ => throw new InvalidOperationException($"Unexpected value: {EngineType.FuelType}")
                };
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"{message}{((EngineType.Horsepower * fuelMod) * Math.Exp(Math.Sqrt(i)) / 2) * (HasSidecar ? 0.8 : 1.0)}mph");
                }
            }
        }

        public abstract class Engine 
        { 
            public bool Running { get; set; }
            public int Horsepower { get; set; }
            public FuelType FuelType { get; set; }

            public Engine(int horsepower, FuelType fuelType)
            { 
                Horsepower = horsepower;
                FuelType = fuelType;
                Running = false;
            }

            public void Start() 
            {
                Running = true;
            }
        }

        // Creating CarEngine subclass from Engine 
        public class CarEngine : Engine 
        {
            public CarEngine() : base(150, FuelType.Unleaded) { }
                
        }
        // Creating MotorcycleEngine subclass from Engine 
        public class MotorcycleEngine : Engine 
        {
            public MotorcycleEngine() : base(70, FuelType.Diesel) { }
        }
    }
}
