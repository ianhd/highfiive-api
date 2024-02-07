using Api.Models;
using Dapper;

namespace Api.Repos;

public class PostRepo : _BaseRepo
{
    public PostRepo(DapperContext context) : base(context) { }

    public async Task<List<Post>> GetForBoard(int board_id)
    {
        using var conn = _context.CreateConnection();

        var sql = @"
select * from post
where board_id = @board_id
order by sort_order
        ";

        return (await conn.QueryAsync<Post>(sql, new { board_id })).ToList();
    }

    public async Task Delete(int post_id)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(@"
            delete from post where post_id = @post_id
        ", new { post_id });
    }

    public async Task Insert(Post item)
    {
        using var conn = _context.CreateConnection();

        var sql = @"
insert into post
    (board_id, body, from_name, giphy_image)
values
    (@board_id, @body, @from_name, @giphy_image)
        ";

        await conn.ExecuteAsync(sql, item);
    }
}
