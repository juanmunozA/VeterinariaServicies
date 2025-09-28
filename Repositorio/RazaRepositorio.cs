using System.Data.SqlClient;
using Veterinaria.Clases;

namespace Veterinaria.Repositorio
{
    public class RazaRepositorio
    {
        private readonly string _connectionString;

        public RazaRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Raza>> GetAllAsync()
        {
            var razas = new List<Raza>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Nombre FROM Razas";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        razas.Add(new Raza
                        {
                            Id = reader.GetInt32(0),
                            NombreRaza = reader.GetString(1)
                        });
                    }
                }
            }
            return razas;
        }

        public async Task<Raza?> GetByIdAsync(int id)
        {
            Raza? raza = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Nombre FROM Razas WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            raza = new Raza
                            {
                                Id = reader.GetInt32(0),
                                NombreRaza = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return raza;
        }

        public async Task<Raza> AddAsync(Raza raza)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO Razas (Nombre) OUTPUT INSERTED.Id VALUES (@Nombre)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", raza.NombreRaza);
                    raza.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return raza;
        }

        public async Task<Raza?> UpdateAsync(Raza raza)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "UPDATE Razas SET Nombre=@Nombre WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", raza.Id);
                    cmd.Parameters.AddWithValue("@Nombre", raza.NombreRaza);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0 ? raza : null;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM Razas WHERE Id=@Id";
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
