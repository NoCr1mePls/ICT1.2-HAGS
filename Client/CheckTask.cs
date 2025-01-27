namespace RobotProject.Client
{
    public class TaskChecker
    {
        public static bool CheckTask(ClientTask task)
        {
            if (DateTime.Now.ToString("HH:mm").Equals(task.TaskTime))
            {
                return true;
            }
            return false;
        }
    }
}