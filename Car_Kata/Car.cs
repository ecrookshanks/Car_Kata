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
            drivingProcessor = new DrivingProcessor();
            drivingInformationDisplay = new DrivingInformationDisplay(drivingProcessor);
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

        public void BrakeBy(int speed)
        {
            drivingProcessor.ReduceSpeed(speed);
        }

        public void Accelerate(int speed)
        {
            if (engine.IsRunning)
            {
                if ( drivingInformationDisplay.ActualSpeed > speed)
                {
                    drivingProcessor.ReduceSpeed(1);
                }
                else
                {
                    drivingProcessor.IncreaseSpeedTo(speed);
                    ConsumeFuel(speed);
                }
                
            }
        }

        private void ConsumeFuel(int speed)
        {
            if (speed <= 0 || speed > 250)
            {
                return;
            }
            else if (speed <= 60)
            {
                tank.Consume(0.0020);
            }
            else if (speed <= 100)
            {
                tank.Consume(0.0014);
            }
            else if (speed <= 140)
            {
                tank.Consume(0.0020);
            }
            else if (speed <= 200)
            {
                tank.Consume(0.0025);
            }
            else if (speed <= 250)
            {
                tank.Consume(0.0030);
            }

            if (tank.FillLevel == 0)
            {
                this.EngineStop();
            }
        }

        public void FreeWheel()
        {
            if (drivingInformationDisplay.ActualSpeed > 0)
            {
                drivingProcessor.ReduceSpeed(1);
            }
            else
            {
                if (drivingInformationDisplay.ActualSpeed == 0)
                {
                    this.RunningIdle();
                }
            }
            
        }

        public IFuelTankDisplay fuelTankDisplay { get { return this.display; } }

        public IDrivingInformationDisplay drivingInformationDisplay; // car #2  

        private IDrivingProcessor drivingProcessor; // car #2

        public Car(double fuelLevel, int maxAcceleration)// car #2
        {
            tank = new FuelTank(MAX_FUEL_CAPACITY, fuelLevel);
            engine = new Engine();
            display = new FuelTankDisplay(tank);
            drivingProcessor = new DrivingProcessor(maxAcceleration);
            drivingInformationDisplay = new DrivingInformationDisplay(drivingProcessor);
        }
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

    public class DrivingInformationDisplay : IDrivingInformationDisplay // car #2
    {
        private IDrivingProcessor processor;

        public DrivingInformationDisplay()
        {
            this.processor = new DrivingProcessor();
        }


        public DrivingInformationDisplay(IDrivingProcessor processor)
        {
            this.processor = processor;
        }
        public int ActualSpeed => processor.ActualSpeed;
    }

    public class DrivingProcessor : IDrivingProcessor // car #2
    {
        private int currentSpeed = 0;
        const int MAX_SPEED = 250;
        private const int MAX_BREAKING = 10;

        public DrivingProcessor(int AccelerationRate = 10)
        {
            if ( AccelerationRate > 20)
            {
                this.AccelerationRate = 20;
            }
            else if (AccelerationRate < 5)
            {
                this.AccelerationRate = 5;
            }
            else
            {
                this.AccelerationRate = AccelerationRate;
            }
        }

        public DrivingProcessor()
        {
            this.AccelerationRate = 10;
        }

        private int AccelerationRate { get; set; }
        public int ActualSpeed { get { return currentSpeed; } private set { currentSpeed = value; } }

        public void IncreaseSpeedTo(int speed)
        {
            if (currentSpeed < speed)
            {
                currentSpeed += this.AccelerationRate;
            }
            if (currentSpeed > speed)
            {
                currentSpeed = speed;
            }
            if (currentSpeed > MAX_SPEED)
            {
                currentSpeed = MAX_SPEED;
            }

        }

        public void ReduceSpeed(int speed)
        {
            if (speed > MAX_BREAKING)
            {
                speed = 10;
            }
            if (currentSpeed > 0)
            {
                currentSpeed -= speed;
            }
        }
    }
}
