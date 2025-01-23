using Avans.StatisticalRobot;

namespace RobotProject.Managers
{
    public class UltrasonicManger
    {
        private List<Ultrasonic> _ultrasonics;
        public List<Ultrasonic> Ultrasonics
        {
            get
            {
                return _ultrasonics;
            }
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                _ultrasonics = value;
            }
        }
        public UltrasonicManger(Ultrasonic[] ultrasonics)
        {
            ArgumentNullException.ThrowIfNull(ultrasonics);
            _ultrasonics = [.. ultrasonics];
        }
        public int[] GetDistance()
        {
            int[] distances = new int[_ultrasonics.Count];
            for (int i = 0; i < _ultrasonics.Count; i++)
            {
                distances[i] = _ultrasonics[i].GetUltrasoneDistance();
            }
            return distances;
        }
    }
}