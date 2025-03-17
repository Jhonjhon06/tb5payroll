using MySql.Data.MySqlClient;

namespace tb5payroll.Services
{
    public class MySqlService
    {
        private readonly string _connectionString;

        public MySqlService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Employee> GetEmployeesFromDatabase()
        {
            var employees = new List<Employee>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Id, Name, Department, Holiday, Overtime, HoursWorked FROM tb5_employees"; // Adjust query as needed
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Id = reader["Id"].ToString(),
                            Name = reader["Name"].ToString(),
                            Department = reader["Department"].ToString(),
                            Holiday = reader.GetInt32("Holiday"),
                            Overtime = reader.GetInt32("Overtime"),
                            HoursWorked = reader.GetInt32("HoursWorked")
                        });
                    }
                }
            }

            return employees;
        }
    }

    public class Employee
    {
        public required string Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int Holiday { get; set; }
        public int Overtime { get; set; }
        public int HoursWorked { get; set; }
    }
}