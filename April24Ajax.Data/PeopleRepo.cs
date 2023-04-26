using System.Data.SqlClient;

namespace April24Ajax.Data
{
    public class PeopleRepo
    {
        private string _connectionString;
        public PeopleRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddPerson(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO People " +
                "VALUES (@firstName, @lastName, @age)";
            command.Parameters.AddWithValue("@firstName", person.FirstName);
            command.Parameters.AddWithValue("@lastName", person.LastName);
            command.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public List<Person> GetAllPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Person> people = new List<Person>();
            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            connection.Close();
            return people;
        }
        public Person GetPersonById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM People " +
                "WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            Person p = new();
            if (reader.Read())
            {
                p.Id = (int)reader["Id"];
                p.FirstName = (string)reader["FirstName"];
                p.LastName = (string)reader["LastName"];
                p.Age = (int)reader["Age"];
            }
            connection.Close();
            return p;
        }
        public void EditPerson(Person p)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age " +
                "WHERE Id = @id";
            command.Parameters.AddWithValue("@firstName", p.FirstName);
            command.Parameters.AddWithValue("@lastName", p.LastName);
            command.Parameters.AddWithValue("@age", p.Age);
            command.Parameters.AddWithValue("@id", p.Id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void DeleteById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM People " +
                "WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}