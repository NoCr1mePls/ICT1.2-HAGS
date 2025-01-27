using Hardware.Touchpad;
using RobotProject.Hardware.GyroCompass;
using RobotProject.Detection;
using RobotProject.Managers;
using Avans.StatisticalRobot;
using RobotProject.Service;
using RobotProject.Client;
using Speaker;
class Program
{
    static void Main(string[] args)
    {                                           //Initialisation to inform the user that the robot is starting up
        LCD16x2 screen = new(0x3E);                                                                                //0x3E is the I2C adress 
        Console.Clear();
        Console.WriteLine("Starting up...");
        screen.SetText("Starting up...");

        MotorManager motorManager = new();      //Initialisation of the robot's hardware
        GyroCompass gyro = new();
        IDetection[] detections = [
            new IllegaltiltDetection(gyro),     //0
            // new FakeBatteryDetection(false),         //1  This is a fake             //The boolean is the return value of the fake detection
            new LowBatteryDetection(700),       //1                                                                //700 is the milivolts threshhold
            new SuddenMotionDetection(gyro),    //2
            new CrashDetection(10, 18, 16)      //3                                                                //10 is the distance, 18 pin of front ultrasonic, 16 pin of right ultrasonic
        ];
        List<ClientTask> tasks = SQLTaskRepository.GetAllTasks();
        Touchpad.OpenPort();
        bool isRunning = false;
        bool isTurning = false;
        WavSpeaker jump = new("/mnt/usb/Jump.wav");
        WavSpeaker sad = new("/mnt/usb/Sad.wav");

        screen.SetText("Robot ready!\nPress # to start");
        Console.WriteLine("Robot ready!");

        motorManager.Stop();
        while (true)
        {
            if (!isRunning) //Start robot when the user presses #
            {
                if (Touchpad.ReadData().Equals('#'))
                {
                    isRunning = true;
                    screen.SetText("Driving...");
                    motorManager.MoveForward();
                }
            }

            else
            {
                for (int i = 0; i < (detections.Length - 1); i++) //Check for all detection systems (except the crash detection) if an interruption gets detected
                {
                    if (detections[i].Detect() == 1) 
                    {
                        Console.WriteLine($"Error detected by: {detections[i].GetType().Name}");
                        motorManager.Stop();
                        isRunning = false;
                        screen.SetText("Error detected!");
                        _ = sad.PlayAsync();
                        break;
                    }
                }
                switch (detections[3].Detect())                  //Check if all an obstacle is in the way
                {
                    case 1:                                //Theres an obstacle in front of robot
                        if (!isTurning)
                        {
                            isTurning = true;
                            motorManager.RightTurn();
                            isTurning = false;
                        }
                        break;
                    case 2:                                //There is an obstacle right infront of the robot
                        if (!isTurning)
                        {
                            isTurning = true;
                            motorManager.LeftTurn();
                            isTurning = false;
                        }
                        break;
                    case 3:                                //The robot is stuck
                        motorManager.Stop();
                        isRunning = false;
                        screen.SetText("Halted!\n Press #");
                        _ = sad.PlayAsync();
                        break;
                }
            }
        }
    }
}
/*
    TODO
V   - Implement Tasks
V   - Implement battery manager
V   - Implement services
V    - Implement a database service
V   - Use MQTT to send status data to broker
V   - Use SQL to store data in a database
V    - Notify user with sound and screen of given task
V    - ^implement a notification system using MQTT and logging
V    - Implement basic object avoidance system
*/