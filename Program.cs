using System;
using System.Device.Gpio;
using System.Device.I2c;
using GyroscopeCompass;
using Avans.StatisticalRobot;
using GyroscopeCompass.GyroscopeCompass;
// using GyroCompass;
class Program
{
    // static void Main()
    // {
    //     Ultrasonic ultrasonic = new Ultrasonic(18);
    //     Robot.Motors(100,100);
    //     bool isDriving = true;
    //     while (true)
    //     {   int distance = ultrasonic.GetUltrasoneDistance();
    //         Console.WriteLine($"Distance: {distance} cm");
    //         if(distance > 0){
    //             if (distance < 10)
    //             {
    //               if(isDriving){
    //                 Robot.Motors(0,0);
    //                 Robot.PlayNotes("O5 Abadbadbsa");
    //                 isDriving = false;
    //               }
    //             }
    //             else
    //             {
    //               if(!isDriving){
    //                 Robot.PlayNotes("");
    //                 Robot.Motors(100,100);
    //                 isDriving = true;
    //               }
    //             }
    //         }
    //     }
    // }
    static void Main()
    {
        // AK09918 compass = new(Robot.CreateI2cDevice(0x0C));
        // compass.Initialize(AK09918Mode.Continuous100Hz);
        // bool isDriving = false;
        // Robot.Motors(0,0);
        // Robot.Wait(1000);
        // while (true)
        // {
        //     if (compass.GetMagnetData(out float  x, out float y, out float z) == AK09918Error.Ok)
        //     {
        //         if(z > 1.5 || z < -1.5){
        //             if(!isDriving){
        //                 Robot.Motors(30,-30);
        //                 isDriving = true;
        //             }
        //         }
        //         else
        //         {
        //             if(isDriving){
        //                 Robot.Motors(0,0);
        //                 isDriving = false;
        //             }
        //         }
        //         Console.WriteLine($"Magnetometer data: X: {x}, Y: {y}, Z: {z}");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Failed to read magnetometer data.");
        //     }
        // }
        GyroCompass gyroCompass = new GyroCompass();
        while (true)
        {
            // gyroCompass.GetGyroAcceleration(out float x, out float y, out float z);
            // Console.WriteLine($"Acceleration: X: {x}, Y: {y}, Z: {z}");
            gyroCompass.GetGyroAngularVelocity(out float x, out float y, out float z);
        }
    }
}