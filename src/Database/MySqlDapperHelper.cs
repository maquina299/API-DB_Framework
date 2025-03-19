using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

public class MySqlDapperHelper
{
    private readonly string _connectionString;

    public MySqlDapperHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<T>(sql, parameters);
    }

    public async Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
    }

    public async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.ExecuteAsync(sql, parameters);
    }
}
