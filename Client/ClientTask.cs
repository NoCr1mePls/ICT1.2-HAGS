namespace RobotProject.Client
{
    public struct ClientTask
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskTime { get; set; }
        public bool TaskStatus { get; set; }
    }
}