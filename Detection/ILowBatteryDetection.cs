namespace RobotProject.Detection
{
    public interface ILowBatteryDetection : IDetection
    {
        public new sbyte Detect();
    }
}