using RobotProject.Hardware.GyroCompass;
namespace RobotProject.Detection
{
    public class SuddenMotionDetection(GyroCompass gyroCompass) : IDetection
    {
        public GyroCompass GyroscopeCompass { get; } = gyroCompass;
        private short _threshold = 600;

        public sbyte Detect()
        {
            GyroscopeCompass.GetGyroAcceleration(out float x, out float y, out float z);
            if (x > _threshold || y > _threshold || z > _threshold || x < -_threshold || y < -_threshold || z < -_threshold)
            {
                return 1;
            }
            return -1;
        }

        public void SetThreshold(short threshold)
        {
            _threshold = threshold;
        }
    }
}