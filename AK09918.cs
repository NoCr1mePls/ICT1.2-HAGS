using System.Device.I2c;

namespace AK09918Lib
{
    public enum AK09918Mode
    {
        PowerDown = 0x00,
        SingleMeasurement = 0x01,
        Continuous10Hz = 0x02,
        Continuous20Hz = 0x04,
        Continuous50Hz = 0x06,
        Continuous100Hz = 0x08,
        SelfTest = 0x10
    }

    public enum AK09918Error
    {
        Ok,
        DataSkipped,
        NotReady,
        Timeout,
        SelfTestFailed,
        Overflow,
        WriteFailed,
        ReadFailed
    }

    public class AK09918
    {
        private const byte DeviceAddress = 0x0C;

        private const byte RegisterST1 = 0x10;
        private const byte RegisterHXL = 0x11;
        private const byte RegisterST2 = 0x18;
        private const byte RegisterCNTL2 = 0x31;
        private const byte RegisterCNTL3 = 0x32;

        private I2cDevice _i2cDevice;
        private AK09918Mode _currentMode;

        public AK09918(I2cDevice device)
        {

            _i2cDevice = device;
            _currentMode = AK09918Mode.PowerDown;
        }

        public AK09918Error Initialize(AK09918Mode mode)
        {
            if (SwitchMode(mode) != AK09918Error.Ok)
            {
                return AK09918Error.WriteFailed;
            }

            Thread.Sleep(100); // Wait for the sensor to be ready
            return AK09918Error.Ok;
        }


        public AK09918Error SwitchMode(AK09918Mode mode)
        {
            if (!I2CWriteByte(RegisterCNTL2, (byte)mode))
            {
                return AK09918Error.WriteFailed;
            }

            _currentMode = mode;
            return AK09918Error.Ok;
        }

        public AK09918Error IsDataReady()
        {
            if (!I2CReadByte(RegisterST1, out byte data))
            {
                return AK09918Error.ReadFailed;
            }

            return (data & 0x01) != 0 ? AK09918Error.Ok : AK09918Error.NotReady;
        }

        public AK09918Error GetMagnetData(out float x, out float y, out float z)
        {
            x = y = z = 0;

            // Wait for data to be ready
            for (int i = 0; i < 10; i++) // Retry 10 times
            {
                var status = IsDataReady();
                if (status == AK09918Error.Ok)
                {
                    break;
                }
                else if (i == 9) // Timeout
                {
                    return AK09918Error.Timeout;
                }
                Thread.Sleep(10); // Wait before retrying
            }

            // Read data
            if (!I2CReadBytes(RegisterHXL, out byte[] buffer, 8)) // Read 8 bytes starting from HXL
            {
                return AK09918Error.ReadFailed;
            }

            // Combine high and low bytes for each axis
            int rawX = (short)((buffer[1] << 8) | buffer[0]); // Combine HXH and HXL
            int rawY = (short)((buffer[3] << 8) | buffer[2]); // Combine HYH and HYL
            int rawZ = (short)((buffer[5] << 8) | buffer[4]); // Combine HZH and HZL

            // Scale raw data to µT and round to 2 decimal places
            x = (float)Math.Round(rawX * 0.15f,2);
            y = (float)Math.Round(rawY * 0.15f,2);
            z = (float)Math.Round(rawZ * 0.15f,2);

            // Check for overflow
            if ((buffer[7] & 0x08) != 0) // HOFL bit
            {
                return AK09918Error.Overflow;
            }

            return AK09918Error.Ok;
        }


        private bool I2CWriteByte(byte register, byte value)
        {
            try
            {
                _i2cDevice.Write(new byte[] { register, value });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"I2C Write failed: {ex.Message}");
                return false;
            }
        }

        private bool I2CReadByte(byte register, out byte value)
        {
            try
            {
                _i2cDevice.WriteByte(register);
                value = _i2cDevice.ReadByte();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"I2C Read failed: {ex.Message}");
                value = 0;
                return false;
            }
        }

        private bool I2CReadBytes(byte register, byte[] buffer, int offset, int length)
        {
            try
            {
                var readBuffer = new byte[length];
                _i2cDevice.WriteByte(register);
                _i2cDevice.Read(readBuffer);

                Array.Copy(readBuffer, 0, buffer, offset, length);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"I2C ReadBytes failed: {ex.Message}");
                return false;
            }
        }

        private bool I2CReadBytes(byte register, out byte[] buffer, int length)
        {
            buffer = new byte[length];
            return I2CReadBytes(register, buffer, 0, length);
        }
    }
}
