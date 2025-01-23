using RobotProject.Hardware.GyroCompass;

namespace RobotProject.Detection
{
    public class IllegaltiltDetection(GyroCompass gyroCompass) : IDetection
    {
        GyroCompass gyroCompass = gyroCompass;
        private short _threshold = 30;
        public sbyte Detect()
        {
            gyroCompass.GetGyroAngularVelocity(out float x, out float y, out float z);
            if (x > _threshold || x < -_threshold)
            {
                return 1;
            }	
            return -1;
        }
        public void SetThreshold(short xThreshold)
        {
            _threshold = xThreshold;
        }
    }
}