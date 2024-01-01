using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Api;

public class DapperContext
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _config = configuration;
        _connectionString = _config.GetConnectionString("main");
    }

    public IDbConnection CreateConnection()
        => new MySqlConnection(_connectionString);
}

