using System.Data.SqlClient;
using Veterinaria.Clases;

namespace Veterinaria.Repositorio
{
    public class FormulaRepositorio
    {
        private readonly string _connectionString;

        public FormulaRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Formula>> GetAllAsync()
        {
            var formulas = new List<Formula>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Codigo, MascotaId FROM Formulas";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        formulas.Add(new Formula
                        {
                            Id = reader.GetInt32(0),
                            Codigo = reader.GetString(1),
                            MascotaId = reader.GetInt32(2)
                        });
                    }
                }
            }
            return formulas;
        }

        public async Task<Formula?> GetByIdAsync(int id)
        {
            Formula? formula = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, Codigo, MascotaId FROM Formulas WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            formula = new Formula
                            {
                                Id = reader.GetInt32(0),
                                Codigo = reader.GetString(1),
                                MascotaId = reader.GetInt32(2)
                            };
                        }
                    }
                }
            }
            return formula;
        }

        public async Task<Formula> AddAsync(Formula formula)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO Formulas (Codigo, MascotaId) OUTPUT INSERTED.Id VALUES (@Codigo, @MascotaId)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Codigo", formula.Codigo);
                    cmd.Parameters.AddWithValue("@MascotaId", formula.MascotaId);
                    formula.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return formula;
        }

        public async Task<Formula?> UpdateAsync(Formula formula)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "UPDATE Formulas SET Codigo=@Codigo, MascotaId=@MascotaId WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", formula.Id);
                    cmd.Parameters.AddWithValue("@Codigo", formula.Codigo);
                    cmd.Parameters.AddWithValue("@MascotaId", formula.MascotaId);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0 ? formula : null;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM Formulas WHERE Id=@Id";
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
