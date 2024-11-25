using GyroscopeCompass.Gyroscope;
using GyroscopeCompass.Compass;
using Avans.StatisticalRobot;
using System.Device.I2c;

namespace GyroscopeCompass.GyroscopeCompass
{
    public enum PerformanceMode
    {
        Sleep = 0x00,
        Normal = 0x01,
        Fast = 0x02
    }

    public enum Range
    {
        Low,
        Medium,
        High
    }
    public class GyroCompass
    {
        private Gyro gyro;
        private Magnetometer compass;
        public GyroCompass()
        {
            gyro = new Gyroscope.Gyro(OccupiedId(0x68) ? Robot.CreateI2cDevice(0x68) : Robot.CreateI2cDevice(0x69));
            compass = new Magnetometer(Robot.CreateI2cDevice(0x0c));
            gyro.Initialize();
            compass.Initialize(CompassMode.Continuous100Hz);
        }

        public void GetGyroAcceleration(out float x, out float y, out float z)
        {
            x = gyro.GetAccelerationX();
            y = gyro.GetAccelerationY();
            z = gyro.GetAccelerationZ();
        }

        public void GetGyroAngularVelocity(out float x, out float y, out float z)
        {
            x = gyro.GetGyroscopeX();
            y = gyro.GetGyroscopeY();
            z = gyro.GetGyroscopeZ();
        }

        public int GetTemperature()
        {
            return gyro.GetTemperature();
        }

        public void SetGyroMode(PerformanceMode mode)
        {
            switch (mode)
            {
                case PerformanceMode.Sleep:
                    gyro.SetPowerMode(GyroConstants.PowerModes.Sleep);
                    break;
                case PerformanceMode.Normal:
                    gyro.SetPowerMode(GyroConstants.PowerModes.LowPower6Axis);
                    break;
                case PerformanceMode.Fast:
                    gyro.SetPowerMode(GyroConstants.PowerModes.LowNoise6Axis);
                    break;
            }
        }

        public void SetGyroRange(Range range)
        {
            switch (range)
            {
                case Range.Low:
                    gyro.SetAccelScaleRange(GyroConstants.AccelRange.RANGE_4G);
                    gyro.SetGyroScaleRange(GyroConstants.GyroRange.RANGE_500_DPS);
                    break;
                case Range.Medium:
                    gyro.SetAccelScaleRange(GyroConstants.AccelRange.RANGE_8G);
                    gyro.SetGyroScaleRange(GyroConstants.GyroRange.RANGE_1000_DPS);
                    break;
                case Range.High:
                    gyro.SetAccelScaleRange(GyroConstants.AccelRange.RANGE_16G);
                    gyro.SetGyroScaleRange(GyroConstants.GyroRange.RANGE_2000_DPS);
                    break;
            }
        }

        public void GetMagnetData(out float x, out float y, out float z)
        {
            compass.GetMagnetData(out x, out y, out z);
        }

        public void SetCompassMode(PerformanceMode mode)
        {
            switch (mode)
            {
                case PerformanceMode.Sleep:
                    compass.SwitchMode(CompassMode.PowerDown);
                    break;
                case PerformanceMode.Normal:
                    compass.SwitchMode(CompassMode.Continuous20Hz);
                    break;
                case PerformanceMode.Fast:
                    compass.SwitchMode(CompassMode.Continuous100Hz);
                    break;
            }

        }
        private static bool OccupiedId(int id)
        {
            int busId = 1;
            try
            {
                var settings = new I2cConnectionSettings(busId, id);
                using var device = I2cDevice.Create(settings);
                device.WriteByte(0x00);
                Console.WriteLine($"Device found at address: {id}. Using alternate ID");
                return true;
            }
            catch
            {
                Console.WriteLine($"No device found at address: {id}. Mounting..");
                return false;
            }
        }
    }
}