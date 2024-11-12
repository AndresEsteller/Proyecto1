using Microsoft.Data.SqlClient;
using System.Data;

namespace Proyecto1_AndrésEsteller.Models
{
    public class DataBaseSQL
    {
        private readonly string _connectionString;

        public DataBaseSQL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable EjecutarConsulta(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var dataTable = new DataTable();
                var command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    dataTable.Load(reader);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dataTable;
            }
        }

        public int EjecutarComando(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
        }
        public DataTable EjecutarConsultaConParametros(SqlCommand command)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var dataTable = new DataTable();
                command.Connection = connection;

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    dataTable.Load(reader);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return dataTable;
            }
        }

        public int EjecutarComandoConParametros(SqlCommand command)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                command.Connection = connection;

                try
                {
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
        }
    }
}
