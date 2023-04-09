﻿using Car_Kata;


namespace Car_Kata_Tests
{
    public class CarTwoTests
    {
        [Fact]
        public void TestMaxAccelerationOutOfRange()
        {
            var car = new Car(10, 600);
            car.EngineStart();
            car.Accelerate(30);
            int speed = car.drivingInformationDisplay.ActualSpeed;
            Assert.Equal(20, speed);
        }


        [Fact]
        public void TestMotorStartAndStop()
        {
            var car = new Car();

            Assert.False(car.EngineIsRunning, "Engine could not be running.");

            car.EngineStart();

            Assert.True(car.EngineIsRunning, "Engine should be running.");

            car.EngineStop();

            Assert.False(car.EngineIsRunning, "Engine could not be running.");
        }

        [Fact]
        public void TestStartSpeed()
        {
            var car = new Car();

            car.EngineStart();

            Assert.Equal(0, car.drivingInformationDisplay.ActualSpeed);
        }

        [Fact]
        public void TestFreeWheelSpeed()
        {
            var car = new Car();

            car.EngineStart();

            Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

            Assert.Equal(100, car.drivingInformationDisplay.ActualSpeed);

            car.FreeWheel();
            car.FreeWheel();
            car.FreeWheel();

            Assert.Equal(97, car.drivingInformationDisplay.ActualSpeed);
        }

        [Fact]
        public void TestAccelerateBy10()
        {
            var car = new Car();

            car.EngineStart();

            Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

            car.Accelerate(160);
            Assert.Equal(110, car.drivingInformationDisplay.ActualSpeed);
            car.Accelerate(160);
            Assert.Equal(120, car.drivingInformationDisplay.ActualSpeed);
            car.Accelerate(160);
            Assert.Equal(130, car.drivingInformationDisplay.ActualSpeed);
            car.Accelerate(160);
            Assert.Equal(140, car.drivingInformationDisplay.ActualSpeed);
            car.Accelerate(145);
            Assert.Equal(145, car.drivingInformationDisplay.ActualSpeed);
        }

        [Fact]
        public void TestAccelerateLessThanActual()
        {
            var car = new Car();

            car.EngineStart();

            Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

            car.Accelerate(160);
            Assert.Equal(110, car.drivingInformationDisplay.ActualSpeed);

            car.Accelerate(50);
            Assert.Equal(109, car.drivingInformationDisplay.ActualSpeed);


        }

        [Fact]
        public void TestBraking()
        {
            var car = new Car();

            car.EngineStart();

            Enumerable.Range(0, 10).ToList().ForEach(s => car.Accelerate(100));

            car.BrakeBy(20);

            Assert.Equal(90, car.drivingInformationDisplay.ActualSpeed);

            car.BrakeBy(10);

            Assert.Equal(80, car.drivingInformationDisplay.ActualSpeed);
        }

        [Fact]
        public void TestConsumptionSpeedUpTo30()
        {
            var car = new Car(1, 20);

            car.EngineStart();

            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);
            car.Accelerate(30);

            Assert.Equal(0.98, car.fuelTankDisplay.FillLevel);
        }
    }
}