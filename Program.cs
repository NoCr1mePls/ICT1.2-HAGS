using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Avans.StatisticalRobot;
using System.Device.Pwm;
using Speaker;

class Program
{

    static async Task Main(string[] args)
    {

        Console.WriteLine("Starting...");
        var endsongPlayer = new WavSpeaker("/mnt/usb/Endsong.wav", false);
        var sadPlayer = new WavSpeaker("/mnt/usb/Sad.wav", true);
        Ultrasonic ultra = new(5);
        bool isInRange = false;
        Console.WriteLine("Ready");
        while (true)
        {
            var distance = ultra.GetUltrasoneDistance();
            Console.WriteLine($"Distance: {distance} cm");
            if (distance > 0)
            {

                if (distance <= 12)
                {
                    if (!isInRange)
                    {
                        isInRange = true;
                        endsongPlayer.Stop();
                        sadPlayer.PlayAsync();
                    }
                }
                else
                {
                    if (isInRange)
                    {
                        isInRange = false;
                        endsongPlayer.PlayAsync();
                        sadPlayer.Stop();
                    }
                }
            }
            Robot.Wait(1);
        }
    }
}