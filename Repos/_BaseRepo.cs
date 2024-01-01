using System.Data;

namespace Api.Repos;

public class _BaseRepo
{
    public readonly DapperContext _context;

    public _BaseRepo(DapperContext context)
    {
        _context = context;
    }

    public IDbConnection CreateConnection()
        => _context.CreateConnection();
}
