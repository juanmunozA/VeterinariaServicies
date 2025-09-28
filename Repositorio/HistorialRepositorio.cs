using System.Data.SqlClient;
using Veterinaria.Clases;

namespace Veterinaria.Repositorio
{
    public class HistorialRepositorio
    {
        private readonly string _connectionString;

        public HistorialRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<HistorialClinico>> GetAllAsync()
        {
            var historiales = new List<HistorialClinico>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"SELECT Id, Codigo, Fecha, MascotaId, ClienteId, FormulaId, VeterinarioId, Observaciones 
                              FROM HistorialesClinicos";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        historiales.Add(new HistorialClinico
                        {
                            Id = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            Fecha = reader.GetDateTime(2),
                            MascotaId = reader.GetInt32(3),
                            ClienteId = reader.GetInt32(4),
                            FormulaId = reader.GetInt32(5),
                            VeterinarioId = reader.GetInt32(6),
                            Observaciones = reader.GetString(7)
                        });
                    }
                }
            }
            return historiales;
        }

        public async Task<HistorialClinico?> GetByIdAsync(int id)
        {
            HistorialClinico? historial = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"SELECT Id, Codigo, Fecha, MascotaId, ClienteId, FormulaId, VeterinarioId, Observaciones 
                              FROM HistorialesClinicos WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            historial = new HistorialClinico
                            {
                                Id = reader.GetInt32(0),
                                Codigo = reader.GetString(1),
                                Fecha = reader.GetDateTime(2),
                                MascotaId = reader.GetInt32(3),
                                ClienteId = reader.GetInt32(4),
                                FormulaId = reader.GetInt32(5),
                                VeterinarioId = reader.GetInt32(6),
                                Observaciones = reader.GetString(7)
                            };
                        }
                    }
                }
            }
            return historial;
        }

        public async Task<HistorialClinico> AddAsync(HistorialClinico historial)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO HistorialesClinicos (Codigo, Fecha, MascotaId, ClienteId, FormulaId, VeterinarioId, Observaciones)
                              OUTPUT INSERTED.Id
                              VALUES (@Codigo, @Fecha, @MascotaId, @ClienteId, @FormulaId, @VeterinarioId, @Observaciones)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Codigo", historial.Codigo);
                    cmd.Parameters.AddWithValue("@Fecha", historial.Fecha);
                    cmd.Parameters.AddWithValue("@MascotaId", historial.MascotaId);
                    cmd.Parameters.AddWithValue("@ClienteId", historial.ClienteId);
                    cmd.Parameters.AddWithValue("@FormulaId", historial.FormulaId);
                    cmd.Parameters.AddWithValue("@VeterinarioId", historial.VeterinarioId);
                    cmd.Parameters.AddWithValue("@Observaciones", historial.Observaciones);
                    historial.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return historial;
        }

        public async Task<HistorialClinico?> UpdateAsync(HistorialClinico historial)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"UPDATE HistorialesClinicos 
                              SET Codigo=@Codigo, Fecha=@Fecha, MascotaId=@MascotaId, ClienteId=@ClienteId, 
                                  FormulaId=@FormulaId, VeterinarioId=@VeterinarioId, Observaciones=@Observaciones
                              WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", historial.Id);
                    cmd.Parameters.AddWithValue("@Codigo", historial.Codigo);
                    cmd.Parameters.AddWithValue("@Fecha", historial.Fecha);
                    cmd.Parameters.AddWithValue("@MascotaId", historial.MascotaId);
                    cmd.Parameters.AddWithValue("@ClienteId", historial.ClienteId);
                    cmd.Parameters.AddWithValue("@FormulaId", historial.FormulaId);
                    cmd.Parameters.AddWithValue("@VeterinarioId", historial.VeterinarioId);
                    cmd.Parameters.AddWithValue("@Observaciones", historial.Observaciones);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0 ? historial : null;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM HistorialesClinicos WHERE Id=@Id";
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
