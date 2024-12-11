using Avans.StatisticalRobot;
using GyroscopeCompass.GyroscopeCompass;
using Hardware.Touchpad;
using Speaker;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting...");
        var jonkler = new WavSpeaker("/mnt/usb/Jonkler.wav", false);
        var sad = new WavSpeaker("/mnt/usb/Sad.wav", true);
        var pad = new Touchpad();
        pad.OpenPort();
        Console.WriteLine("Ready");
        while (true){
            var data = pad.ReadData();
            if(data != 0){
                switch(data){
                    case TouchpadKey.One:
                    jonkler.PlayAsync();
                    break;
                    case TouchpadKey.Two:
                    sad.PlayAsync();
                    break;
                    case TouchpadKey.Three:
                    jonkler.Stop();
                    sad.Stop();
                    break;
                    case TouchpadKey.Four:
                    pad.ClosePort();
                    return;
                }
            }
        }
    }
}