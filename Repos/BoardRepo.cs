using Api.Models;
using Dapper;

namespace Api.Repos;

public class BoardRepo : _BaseRepo
{
    public BoardRepo(DapperContext context) : base(context) { }

    public async Task<Board> Insert(Board item)
    {
        using var conn = _context.CreateConnection();

        var sql = @"
insert into board
    (first_name, last_name, occasion_id, pexels_photo_id, title)
values
    (@first_name, @last_name, @occasion_id, @pexels_photo_id, @title)
;SELECT LAST_INSERT_ID()
        ";

        item.board_id = await conn.QueryFirstOrDefaultAsync<int>(sql, item);
        return item;
    }
}
