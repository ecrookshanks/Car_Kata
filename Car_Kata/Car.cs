using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

using static System.Math;

namespace Car_Kata
{
    public class Car : ICar
    {
        private readonly double MAX_FUEL_CAPACITY = 60;
        IFuelTank tank;
        IEngine engine;
        IFuelTankDisplay display;


        public Car() : this(20)
        { }

        public Car(double fuelLevel)
        {
            tank = new FuelTank(MAX_FUEL_CAPACITY, fuelLevel);
            engine = new Engine();
            display = new FuelTankDisplay(tank);
        }
        public bool EngineIsRunning => engine.IsRunning;

        public void EngineStart()
        {
            if (tank.FillLevel == 0)
            {
                return;
            }
            engine.Start();
        }

        public void EngineStop()
        {
            engine.Stop();
        }

        public void Refuel(double liters)
        {
            tank.Refuel(liters);  
        }

        public void RunningIdle()
        {
            if (EngineIsRunning)
            {
                tank.Consume(0.0003);
                if (tank.FillLevel == 0)
                {
                    engine.Stop();
                }
            }
        }

        public IFuelTankDisplay fuelTankDisplay { get { return this.display; } }
    }

    public class Engine : IEngine
    {
        private bool running = false;
        public bool IsRunning => running;

        public void Consume(double liters)
        {
            
        }

        public void Start()
        {
            running = true;
        }

        public void Stop()
        {
            running = false;
        }
    }

    public class FuelTank : IFuelTank
    {
        public double Capacity { get; private set; }
        private double fillLevel;

        public FuelTank(double capacity, double initialLevel)
        {
            Capacity = capacity;
            fillLevel = initialLevel;
            if (fillLevel < 0)
            {
                fillLevel = 0;
            }
            if (fillLevel > Capacity)
            {
                fillLevel = Capacity;
            }
        }


        public double FillLevel => Round(fillLevel, 2);

        public bool IsOnReserve => fillLevel <= 5.0;

        public bool IsComplete => fillLevel >= 59.8;

        public void Consume(double liters)
        {

            fillLevel -= liters;
            if (fillLevel < 0)
            {
                fillLevel = 0.0;
            }
        }

        public void Refuel(double liters)
        {
            fillLevel += liters;
            if (fillLevel > Capacity)
            {
                fillLevel = Capacity;
            }
        }
    }

    public class FuelTankDisplay : IFuelTankDisplay
    {
        IFuelTank tank;

        public FuelTankDisplay(IFuelTank tank)
        {
            this.tank = tank;
        }

        public double FillLevel => tank.FillLevel;

        public bool IsOnReserve => tank.IsOnReserve;

        public bool IsComplete => tank.IsComplete;
    }
}
