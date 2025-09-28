using System.Data.SqlClient;
using Veterinaria.Clases;



namespace Veterinaria.Repositorio
{
    public class ClienteRepositorio
    {
        private readonly string _connectionString;

        public ClienteRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            var clientes = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Nombre, Cedula, Correo FROM Clientes";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        clientes.Add(new Cliente
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Cedula = reader.GetString(2),
                            Correo = reader.GetString(3)
                        });
                    }
                }
            }
            return clientes;
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            Cliente? cliente = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Nombre, Cedula, Correo FROM Clientes WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cliente = new Cliente
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Cedula = reader.GetString(2),
                                Correo = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return cliente;
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO Clientes (Nombre, Cedula, Correo)
                              OUTPUT INSERTED.Id
                              VALUES (@Nombre, @Cedula, @Correo)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                    cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                    cliente.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return cliente;
        }

        public async Task<Cliente?> UpdateAsync(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"UPDATE Clientes SET Nombre=@Nombre, Cedula=@Cedula, Correo=@Correo WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", cliente.Id);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                    cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0 ? cliente : null;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM Clientes WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0;
                }
            }
        }
    }
}
