using System.Data.SqlClient;
using Veterinaria.Clases;

namespace Veterinaria.Repositorio
{
    public class FormulaMedicamentoRepositorio
    {
        private readonly string _connectionString;

        public FormulaMedicamentoRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<FormulaMedicamento>> GetAllAsync()
        {
            var lista = new List<FormulaMedicamento>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, FormulaId, MedicamentoId, Cantidad FROM FormulaMedicamentos";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new FormulaMedicamento
                        {
                            Id = reader.GetInt32(0),
                            FormulaId = reader.GetInt32(1),
                            MedicamentoId = reader.GetInt32(2),
                            Cantidad = reader.GetInt32(3)
                        });
                    }
                }
            }
            return lista;
        }

        public async Task<FormulaMedicamento?> GetByIdAsync(int id)
        {
            FormulaMedicamento? item = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT Id, FormulaId, MedicamentoId, Cantidad FROM FormulaMedicamentos WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            item = new FormulaMedicamento
                            {
                                Id = reader.GetInt32(0),
                                FormulaId = reader.GetInt32(1),
                                MedicamentoId = reader.GetInt32(2),
                                Cantidad = reader.GetInt32(3)
                            };
                        }
                    }
                }
            }
            return item;
        }

        public async Task<FormulaMedicamento> AddAsync(FormulaMedicamento fm)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = @"INSERT INTO FormulaMedicamentos (FormulaId, MedicamentoId, Cantidad)
                              OUTPUT INSERTED.Id
                              VALUES (@FormulaId, @MedicamentoId, @Cantidad)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FormulaId", fm.FormulaId);
                    cmd.Parameters.AddWithValue("@MedicamentoId", fm.MedicamentoId);
                    cmd.Parameters.AddWithValue("@Cantidad", fm.Cantidad);
                    fm.Id = (int)await cmd.ExecuteScalarAsync();
                }
            }
            return fm;
        }

        public async Task<FormulaMedicamento?> UpdateAsync(FormulaMedicamento fm)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "UPDATE FormulaMedicamentos SET FormulaId=@FormulaId, MedicamentoId=@MedicamentoId, Cantidad=@Cantidad WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", fm.Id);
                    cmd.Parameters.AddWithValue("@FormulaId", fm.FormulaId);
                    cmd.Parameters.AddWithValue("@MedicamentoId", fm.MedicamentoId);
                    cmd.Parameters.AddWithValue("@Cantidad", fm.Cantidad);
                    int rows = await cmd.ExecuteNonQueryAsync();
                    return rows > 0 ? fm : null;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "DELETE FROM FormulaMedicamentos WHERE Id=@Id";
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
