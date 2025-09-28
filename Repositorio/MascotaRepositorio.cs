using System.Data.SqlClient;
using Veterinaria.Clases;

namespace Veterinaria.Repositorio
{
    public class MascotaRepositorio
    {
        private readonly string _connectionString;

        public MascotaRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Mascota>> GetAllAsync()
        {
            var mascotas = new List<Mascota>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Nombre, Edad, ClienteId, RazaId FROM Mascotas";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        mascotas.Add(new Mascota
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Edad = reader.GetInt32(2),
                            ClienteId = reader.GetInt32(3),
                            RazaId = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                        });
                    }
                }
            }
            return mascotas;
        }

        public async Task<Mascota?> GetByIdAsync(int id)
        {
            Mascota? mascota = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Nombre, Edad, ClienteId, RazaId FROM Mascotas WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            mascota = new Mascota
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Edad = reader.GetInt32(2),
                                ClienteId = reader.GetInt32(3),
                                RazaId = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                            };
                        }
                    }
                }
            }
            return mascota;
        }

        public async Task<Mascota> AddAsync(Mascota mascota)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO Mascotas (Nombre, Edad, ClienteId, RazaId)
                              OUTPUT INSERTED.Id
                              VALUES (@Nombre, @Edad, @ClienteId, @RazaId)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", mascota.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", mascota.Edad);
                    cmd.Parameters.AddWithValue("@ClienteId", mascota.ClienteId);
                    cmd.Parameters.AddWithValue("@RazaId", (object?)mascota.RazaId ?? DBNull.Value);

                    mascota.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return mascota;
        }

        public async Task<Mascota?> UpdateAsync(Mascota mascota)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"UPDATE Mascotas SET Nombre=@Nombre, Edad=@Edad, ClienteId=@ClienteId, RazaId=@RazaId WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", mascota.Id);
                    cmd.Parameters.AddWithValue("@Nombre", mascota.Nombre);
                    cmd.Parameters.AddWithValue("@Edad", mascota.Edad);
                    cmd.Parameters.AddWithValue("@ClienteId", mascota.ClienteId);
                    cmd.Parameters.AddWithValue("@RazaId", (object?)mascota.RazaId ?? DBNull.Value);

                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0 ? mascota : null;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM Mascotas WHERE Id=@Id";
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
