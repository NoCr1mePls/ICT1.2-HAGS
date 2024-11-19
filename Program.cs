using System.Device.Gpio;
using Avans.StatisticalRobot;

Console.WriteLine("Hello world");

Led led = new Led(5);
Button button = new Button(6);

while (true)
{   
    led.SetOn();
    Robot.Wait(100);
    led.SetOff();
    Robot.Wait(100);
    if(button.GetState().Equals("Pressed")){
        Robot.Motors(100,100);
    }
    else{
        Robot.Motors(0,0);
    }
}