using System.Data.SqlClient;
using Veterinaria.Clases;

namespace Veterinaria.Repositorio
{
    public class MedicamentoRepositorio
    {
        private readonly string _connectionString;

        public MedicamentoRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Medicamento>> GetAllAsync()
        {
            var medicamentos = new List<Medicamento>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Nombre FROM Medicamentos";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        medicamentos.Add(new Medicamento
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }
            return medicamentos;
        }

        public async Task<Medicamento?> GetByIdAsync(int id)
        {
            Medicamento? medicamento = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Nombre FROM Medicamentos WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            medicamento = new Medicamento
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1)
                            };
                        }
                    }
                }
            }
            return medicamento;
        }

        public async Task<Medicamento> AddAsync(Medicamento medicamento)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO Medicamentos (Nombre) OUTPUT INSERTED.Id VALUES (@Nombre)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", medicamento.Nombre);
                    medicamento.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return medicamento;
        }

        public async Task<Medicamento?> UpdateAsync(Medicamento medicamento)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "UPDATE Medicamentos SET Nombre=@Nombre WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", medicamento.Id);
                    cmd.Parameters.AddWithValue("@Nombre", medicamento.Nombre);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0 ? medicamento : null;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM Medicamentos WHERE Id=@Id";
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
