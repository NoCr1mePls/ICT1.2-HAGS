using RobotProject.Managers;

namespace RobotProject.Detection
{
    public class CrashDetection
    {
        private int _distance;
        private UltrasonicManger _ultrasonicManger;

        public CrashDetection(int distance, byte leftUltrasonic, byte rightUltrasonic)
        {
            _distance = distance;
            _ultrasonicManger = new UltrasonicManger([new(leftUltrasonic), new(rightUltrasonic)]);
        }


        /// <summary>
        /// Detects if a robot is within a certain distance of an object
        /// </summary>
        /// <returns>-1 if no crash is detected</returns>
        public sbyte Detect()
        {
            int distance1 = _ultrasonicManger.GetDistance()[0];
            int distance2 = _ultrasonicManger.GetDistance()[1];
            if (distance1 < _distance && distance2 > _distance)
            {
                return 1;
            }
            else if (distance1 > _distance && distance2 < _distance)
            {
                return 2;
            }
            else if (distance1 < _distance && distance2 < _distance)
            {
                return 3;
            }
            else
            {
                return -1;

            }
        }
    }
}