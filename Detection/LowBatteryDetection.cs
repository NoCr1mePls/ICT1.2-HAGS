using Avans.StatisticalRobot;

namespace RobotProject.Detection
{
    public class LowBatteryDetection : ILowBatteryDetection
    {
        private short _threshold;

        public LowBatteryDetection(int threshold)
        {
            _threshold = (short)threshold;
        }
        
        public sbyte Detect()
        {
            if (Robot.ReadBatteryMillivolts() < _threshold)
            {
                return 1;
            }
            return 0;
        }
    }
}