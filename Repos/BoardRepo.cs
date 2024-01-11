using Api.Models;
using Dapper;

namespace Api.Repos;

public class BoardRepo : _BaseRepo
{
    public BoardRepo(DapperContext context) : base(context) { }

    public async Task<List<Board>> GetAll(int user_id)
    {
        using var conn = _context.CreateConnection();
        return (await conn.QueryAsync<Board>(@"
            select * from board where user_id = @user_id
        ", new { user_id })).ToList();
    }

    public async Task<Board> Insert(Board item)
    {
        using var conn = _context.CreateConnection();

        var sql = @"
insert into board
    (first_name, user_id, last_name, occasion_id, pexels_photo_id, title)
values
    (@first_name, @user_id, @last_name, @occasion_id, @pexels_photo_id, @title)
;SELECT LAST_INSERT_ID()
        ";

        item.board_id = await conn.QueryFirstOrDefaultAsync<int>(sql, item);
        return item;
    }
}
