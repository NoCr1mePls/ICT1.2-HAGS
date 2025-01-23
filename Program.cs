using Hardware.Touchpad;
using RobotProject.Hardware.GyroCompass;
using RobotProject.Detection;
using RobotProject.Managers;
class Program
{
    static void Main(string[] args)
    {
        GyroCompass gyroCompass = new GyroCompass();
        MotorManager motorManager = new();
        Touchpad.OpenPort();
        bool isDriving = true;
        IDetection[] detections = [
            new IllegaltiltDetection(gyroCompass),
            new SuddenMotionDetection(gyroCompass)
        ];
        List<(float x, float y, float z)> data = [];
        motorManager.MoveForward();
        while (true)
        {
            if (detections[0].Detect() == 1)
            {
                Console.WriteLine("Illegal tilt detected");
                if (isDriving)
                {
                    motorManager.Stop();
                    isDriving = false;
                }
            }

            if (detections[1].Detect() == 1)
            {
                Console.WriteLine("Sudden motion detected");
            }

            switch (Touchpad.ReadData())
            {
                case '1':
                    motorManager.MoveForward();
                    isDriving = true;
                    break;
                case '0':
                    motorManager.Stop();
                    isDriving = false;
                    break;
            }
            // gyroCompass.GetGyroAngularVelocity(out float x, out float y, out float z);
            // Console.WriteLine($"x: {x}, y: {y + 120}, z: {z}");
            // data.Add((x, y, z));
        }
        Touchpad.ClosePort();
        Console.WriteLine($"Data size: {data.Count}\nMax x: {data.Max(x => x.x)}\nMax y: {data.Max(y => y.y) + 120}");
        Console.WriteLine($"Min x: {data.Min(x => x.x)}\nMin y: {data.Min(y => y.y) + 120}");
    }
}

/*
    TODO
    - Implement Tasks
    - Implement basic object avoidance system
    - Implement services
       - Implement MQTT
            - Implement a publisher
            - Implement a subscriber
       - Implement a database service
         - Implement a logging service
    - Use MQTT to send status data to broker
    - Use SQL to store data in a database
    - Notify user with sound and screen of given task
    - ^implement a notification system using MQTT and logging
*/