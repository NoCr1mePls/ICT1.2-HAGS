using Avans.StatisticalRobot;

namespace RobotProject.Managers
{
    public enum Speed
    {
        Slow = 50,
        Normal = 100,
        Fast = 150
    }

    public class MotorManager
    {
        private byte offset = 5; //percent
        private Speed speed = Speed.Normal;
        public void MoveForward()
        {
            Robot.Motors((short)speed, (short)((int)speed - ((int)speed / 100 * offset)));
        }
        public void MoveBackward()
        {
            Robot.Motors((short)((int)speed * -1), (short)((short)(speed - ((int)speed / 100 * offset)) * -1));
        }
        public void TurnLeft()
        {
            Robot.Motors((short)((int)speed * -1), (short)speed);
        }

        public void LeftTurn()
        {
            var timer = new System.Timers.Timer(750);
            timer.Start();
            TurnLeft();
            timer.Elapsed += (sender, e) =>
            {
                MoveForward();
                timer.Stop();
                timer.Dispose();
            };
        }
        public void TurnRight()
        {
            Robot.Motors((short)speed, (short)((int)speed * -1));
        }

        public void RightTurn(){
            var timer = new System.Timers.Timer(750);
            timer.Start();
            TurnRight();
            timer.Elapsed += (sender, e) =>
            {
                MoveForward();
                timer.Stop();
                timer.Dispose();
            };
        }
        public void Stop()
        {
            Robot.Motors(0, 0);
        }
        public void SetSpeed(Speed speed)
        {
            this.speed = speed;
        }

        public void SetOffset(byte offset)
        {
            this.offset = offset;
        }
    }
}