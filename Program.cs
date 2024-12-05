using Avans.StatisticalRobot;
using GyroscopeCompass.GyroscopeCompass;
using Speaker;

class Program
{

    static void Main(string[] args)
    {

        Console.WriteLine("Starting...");
        var jonkler = new WavSpeaker("/mnt/usb/Fein.wav", false);
        var caretaker = new WavSpeaker("/mnt/usb/Caretaker.wav", true);
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
                        jonkler.Stop();
                        _ = caretaker.PlayAsync();
                    }
                }
                else
                {
                    if (isInRange)
                    {
                        isInRange = false;
                        _ = jonkler.PlayAsync();
                        caretaker.Stop();
                    }
                }
            }
            Robot.Wait(1);
        }
    }
}