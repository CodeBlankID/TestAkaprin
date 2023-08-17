using MySqlConnector;
using System.Text.Json.Serialization;

namespace TestAkaPrin.Models
{
    public class Identity
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        private IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

        private string Queryraw(string? type)
        {
            switch (type)
            {
                case "SELECT":
                    return "SELECT * FROM Person";
                case "SELECTBYID":
                    return "SELECT * FROM Person where id=@id";
                case "INSERT":
                    return "INSERT INTO person (name,email,phone,address)VALUES (@name, @email, @phone,@address)";
                case "UPDATE":
                    return "UPDATE Person SET name=@name,email=@email,phone=@phone,address=@address WHERE id=@id";
                case "DELETE":
                    return "DELETE FROM Person WHERE id=@id";
                default:
                    return "";
            }
        }

        public string dbModify(Identity val,string type, int id=0)
        {
            using (var connection = new MySqlConnection(this.DbConnect()))
            {
                connection.Open();
                var command = new MySqlCommand(this.Queryraw(type), connection);
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = val.Name ?? "";
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = val.Email ?? "";
                command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = val.phone ?? "";
                command.Parameters.Add("@address", MySqlDbType.VarChar).Value = val.address ?? "";
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                   return "true";
                }
                connection.Close();
                 return "false";

            }
        }

        public List<Identity> dbShow(string type,int id=0)
        {
            var list = new List<Identity>();
            using (var connection = new MySqlConnection(this.DbConnect()))
            {
                connection.Open();
                var command = new MySqlCommand(this.Queryraw(type), connection);
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var listperson = new Identity();
                    listperson.Id = (int)reader["id"];
                    listperson.Name = reader["name"].ToString();
                    listperson.Email = reader["email"].ToString();
                    listperson.phone = reader["phone"].ToString();
                    listperson.address = reader["address"].ToString();

                    list.Add(listperson);
                }
                connection.Close();
            }

            return list;
        }

        public string DbConnect()
        {
            return config.GetRequiredSection("ConnectionStrings:DefaultConnection").Get<string>();
        }
    }

    
}
