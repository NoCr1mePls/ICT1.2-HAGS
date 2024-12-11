using System;
using System.Device.Gpio;
using System.IO.Ports;
namespace Hardware.Touchpad{
    public enum TouchpadKey{
        One = 0xE1,
        Two = 0xE2,
        Three = 0xE3,
        Four = 0xE4,
        Five = 0xE5,
        Six = 0xE6,
        Seven = 0xE7,
        Eight = 0xE8,
        Nine = 0xE9,
        Star = 0xEA,
        Zero = 0xEB,
        Hash = 0xEC
    }
    class Touchpad
    {
        static SerialPort serialPort;

        public Touchpad()
        {
            // Initialize the serial port
            serialPort = new SerialPort("/dev/ttyAMA0", 9600);
        }

        public TouchpadKey ReadData()
        {  
            if (serialPort.BytesToRead > 0)
            {
                int data = serialPort.ReadByte();
                switch (data)
                {
                    case 0xE1:
                        return TouchpadKey.One;
                    case 0xE2:
                        return TouchpadKey.Two;
                    case 0xE3:
                        return TouchpadKey.Three;
                    case 0xE4:
                        return TouchpadKey.Four;
                    case 0xE5:
                        return TouchpadKey.Five;
                    case 0xE6:
                        return TouchpadKey.Six;
                    case 0xE7:
                        return TouchpadKey.Seven;
                    case 0xE8:
                        return TouchpadKey.Eight;
                    case 0xE9:
                        return TouchpadKey.Nine;
                    case 0xEA:
                        return TouchpadKey.Star;
                    case 0xEB:
                        return TouchpadKey.Zero;
                    case 0xEC:
                        return TouchpadKey.Hash;
                    default:
                        return 0;
                }
            }
            return 0;

        }
        public void ClosePort()
        {
            serialPort.Close();
        }

        public void OpenPort()
        {
            serialPort.Open();
        }
        public void PrintData()
        {
            if (serialPort.BytesToRead > 0)
            {
                int data = serialPort.ReadByte();
                switch (data)
                {
                    case 0xE1:
                        Console.WriteLine("1");
                        break;
                    case 0xE2:
                        Console.WriteLine("2");
                        break;
                    case 0xE3:
                        Console.WriteLine("3");
                        break;
                    case 0xE4:
                        Console.WriteLine("4");
                        break;
                    case 0xE5:
                        Console.WriteLine("5");
                        break;
                    case 0xE6:
                        Console.WriteLine("6");
                        break;
                    case 0xE7:
                        Console.WriteLine("7");
                        break;
                    case 0xE8:
                        Console.WriteLine("8");
                        break;
                    case 0xE9:
                        Console.WriteLine("9");
                        break;
                    case 0xEA:
                        Console.WriteLine("*");
                        break;
                    case 0xEB:
                        Console.WriteLine("0");
                        break;
                    case 0xEC:
                        Console.WriteLine("#");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}