using Avans.StatisticalRobot;
using GyroscopeCompass.GyroscopeCompass;
using Hardware.Touchpad;
using Speaker;
using Hardware.OLedDisplay;
using System.Device.I2c;
class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Starting...");
        // var jonkler = new WavSpeaker("/mnt/usb/Jonkler.wav", false);
        // var sad = new WavSpeaker("/mnt/usb/Sad.wav", true);
        // Touchpad.OpenPort();
        // Console.WriteLine("Ready");
        // while (true){
        //     var data = Touchpad.ReadData();
        //     if(data != 0){
        //         switch(data){
        //             case '1':
        //                 _ = jonkler.PlayAsync();
        //             break;
        //             case '2':
        //                 _ = sad.PlayAsync();
        //             break;
        //             case '3':
        //             jonkler.Stop();
        //             sad.Stop();
        //             break;
        //             case '4':
        //             Touchpad.ClosePort();
        //             return;
        //         }
        //     }
        // }

        // var display = new Display(Robot.CreateI2cDevice(0x3D));
        // display.PutChar('A');
        try
        {
            
            Console.WriteLine("Scanning I2C bus...");
            ScanI2CBus();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error initializing I2C: {ex.Message}");
        }
    }
    public static void ScanI2CBus()
    {
        const byte minAddress = 0x03; // Minimum address for I2C devices
        const byte maxAddress = 0x77; // Maximum address for I2C devices

        for (byte address = minAddress; address <= maxAddress; address++)
        {
            try
            {
                // Create an I2C device instance for the current address
                I2cDevice currentDevice = Robot.CreateI2cDevice(address);
                currentDevice.WriteByte(0x00); // Send a "dummy" byte to check if the device responds
                Console.WriteLine($"Device found at address 0x{address:X2}");
            }
            catch (Exception ex)
            {
                // If an exception occurs, it could mean no device is at this address
                // or there was an I2C communication error
                // Console.WriteLine($"No device at address 0x{address:X2} or communication error.");
            }
        }
    }
}