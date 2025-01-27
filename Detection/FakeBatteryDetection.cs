namespace RobotProject.Detection
{
    public class FakeBatteryDetection(bool returnValue) : ILowBatteryDetection
    {
        private readonly bool _returnValue = returnValue;

        public sbyte Detect()
        {
            return _returnValue ? (sbyte)1 : (sbyte)-1;
        }
    }
}