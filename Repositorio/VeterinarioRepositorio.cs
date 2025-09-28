using System.Data.SqlClient;
using Veterinaria.Clases;

namespace Veterinaria.Repositorio
{
    public class VeterinarioRepositorio
    {
        private readonly string _connectionString;

        public VeterinarioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Veterinario>> GetAllAsync()
        {
            var veterinarios = new List<Veterinario>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Documento, Nombre FROM Veterinarios";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        veterinarios.Add(new Veterinario
                        {
                            Id = reader.GetInt32(0),
                            Documento = reader.GetString(1),
                            Nombre = reader.GetString(2)
                        });
                    }
                }
            }
            return veterinarios;
        }

        public async Task<Veterinario?> GetByIdAsync(int id)
        {
            Veterinario? veterinario = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Documento, Nombre FROM Veterinarios WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            veterinario = new Veterinario
                            {
                                Id = reader.GetInt32(0),
                                Documento = reader.GetString(1),
                                Nombre = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return veterinario;
        }

        public async Task<Veterinario> AddAsync(Veterinario veterinario)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO Veterinarios (Documento, Nombre)
                              OUTPUT INSERTED.Id
                              VALUES (@Documento, @Nombre)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Documento", veterinario.Documento);
                    cmd.Parameters.AddWithValue("@Nombre", veterinario.Nombre);
                    veterinario.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return veterinario;
        }

        public async Task<Veterinario?> UpdateAsync(Veterinario veterinario)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"UPDATE Veterinarios SET Documento=@Documento, Nombre=@Nombre WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", veterinario.Id);
                    cmd.Parameters.AddWithValue("@Documento", veterinario.Documento);
                    cmd.Parameters.AddWithValue("@Nombre", veterinario.Nombre);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0 ? veterinario : null;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM Veterinarios WHERE Id=@Id";
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
