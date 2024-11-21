using System;
using System.Device.Gpio;
using System.Device.I2c;
using Avans.StatisticalRobot;
// using GyroCompass;

class Program
{
    static void Main()
    {
        var imu = new ICM20600(Robot.CreateI2cDevice(0x69));
        imu.Initialize();
        // bool boolean = false;
        // Console.WriteLine($"Device ID: 0x{imu.GetDeviceID():X2}");
        Button button = new Button(6);
        bool isDriving = false;
        imu.SetAccelScaleRange(ICM20600Constants.AccelRange.RANGE_2G);
        while (true)
        {
            //     int accelX = imu.GetAccelerationX();
            //     int gyroX = imu.GetGyroscopeX();

            //     if (button.GetState().Equals("Pressed"))
            //     {
            //         if (boolean == false)
            //         {
            //             imu.SetGyroScaleRange(ICM20600Constants.GyroRange.RANGE_250_DPS);
            //             boolean = true;
            //         }
            //         else
            //         {
            //             boolean = false;
            //             imu.SetGyroScaleRange(ICM20600Constants.GyroRange.RANGE_2000_DPS);
            //         }
            //     }
            //     Console.WriteLine($"Acceleration X: {accelX} Gyroscope X: {gyroX}");
            var gyroX = imu.GetGyroscopeX();
            var gyroY = imu.GetGyroscopeY();
            var gyroZ = imu.GetGyroscopeZ();
            Console.WriteLine($"Gyroscope X: {gyroX} Gyroscope Y: {gyroY} Gyroscope Z: {gyroZ}");
            
            // if (button.GetState().Equals("Pressed"))
            // {
            //     if (!isDriving)
            //     {
            //         Robot.Motors(100, 100);
            //         isDriving = true;
            //     }
            //     else
            //     {
            //         Robot.Motors(0, 0);
            //         isDriving = false;
            //     }
            // }
        }
    }
}