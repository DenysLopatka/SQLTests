﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SQLTests
{
    public class SqlHelper
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionString;
        private SqlConnection _sqlConnection;

        public SqlHelper(string dbName)
        {
            _sqlConnectionString = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };
        }

        public void OpenConnection()
        {
            _sqlConnection = new SqlConnection(_sqlConnectionString.ConnectionString);
            _sqlConnection.Open();
        }

        public void CloseConnection() => _sqlConnection.Close();

        public void ExecuteNonQuery(string request)
        {
            var command = new SqlCommand(request, _sqlConnection);
            command.ExecuteNonQuery();
        }

        public void Insert(string table, Dictionary<string, string> parameters)
        {
            var columns = string.Empty;
            var values = string.Empty;

            foreach (var (key, value) in parameters)
            {
                columns += $"{key},";
                values += $"{value},";
            }

            var command = new SqlCommand($"insert into {table} ({columns.TrimEnd(',')}) values ({values.TrimEnd(',')})",
                _sqlConnection);
            command.ExecuteNonQuery();
        }

        public bool IsRowExistedInTable(string table, Dictionary<string, string> parameters)
        {
            var whereParameters = string.Empty;

            foreach (var (key, value) in parameters)
                whereParameters += $" and {key}={value}";

            var sqlDataAdapter = new SqlDataAdapter(
                $"select * from {table} where {whereParameters.Substring(5)}",
                _sqlConnection);
            var dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            return dataTable.Rows.Count > 0;
        }

        public void Update(string table, Dictionary<string, string> parameters, Dictionary<string, string> whereParameters)
        {
            var set = string.Empty;
            var where = string.Empty;

            foreach (var (key, value) in parameters)
                set += $" and {key}={value}";

            foreach (var (key, value) in whereParameters)
                where += $" and {key}={value}";            

            var command = new SqlCommand(
                $"update {table} set {set.Substring(5)} where {where.Substring(5)}", _sqlConnection);

            command.ExecuteNonQuery();
        }

        public void Delete(string table, Dictionary<string, string> parameters)
        {
            var where = string.Empty;            

            foreach (var (key, value) in parameters)
                where += $" and {key}={value}";

            

            var command = new SqlCommand(
                $"delete from {table} where {where.Substring(5)}", _sqlConnection);

            command.ExecuteNonQuery();
        }
    }
}