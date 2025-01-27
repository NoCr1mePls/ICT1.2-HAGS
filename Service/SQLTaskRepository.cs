using Microsoft.Data.SqlClient;
using RobotProject.Client;
namespace RobotProject.Service
{
    public class SQLTaskRepository
    {
        public static void SaveTask(ClientTask task)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand command = new("INSERT INTO ClientTask (TaskID, Name, Description, Time, Status) VALUES (@ID, @Name, @Description, @Time, @Status)", connection);
            command.Parameters.AddWithValue("@ID", task.TaskID);
            command.Parameters.AddWithValue("@Name", task.TaskName);
            command.Parameters.AddWithValue("@Description", task.TaskDescription);
            command.Parameters.AddWithValue("@Time", task.TaskTime);
            command.Parameters.AddWithValue("@Status", task.TaskStatus);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static List<ClientTask> GetAllTasks()
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand command = new("SELECT * FROM ClientTask", connection);
            using SqlDataReader reader = command.ExecuteReader();
            List<ClientTask> tasks = [];
            while (reader.Read())
            {
                tasks.Add(new ClientTask
                {
                    TaskID = reader.GetInt32(0),
                    TaskName = reader.GetString(1),
                    TaskDescription = reader.GetString(2),
                    TaskTime = reader.GetString(3),
                    TaskStatus = reader.GetBoolean(4)
                });
            }
            return tasks;
        }

        public static void EmptyTable()
        {
            string _connectionString = "Server= aei-sql2.avans.nl,1443; Database= DB2199319; UID= ITI2199319; password= X2rpI6G1; TrustServerCertificate= True;";
            using SqlConnection connection = new(_connectionString);
            connection.Open();
            using SqlCommand command = new("DELETE FROM ClientTask", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}