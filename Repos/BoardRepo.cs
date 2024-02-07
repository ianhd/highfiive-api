using Api.Models;
using Dapper;

namespace Api.Repos;

public class BoardRepo : _BaseRepo
{
    public BoardRepo(DapperContext context) : base(context) { }

    public async Task Delete(int board_id)
    {
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(@"
            delete from post where board_id = @board_id
            ;delete from board where board_id = @board_id
        ", new { board_id });
    }

    public async Task<Board> Get(int board_id)
    {
        using var conn = _context.CreateConnection();
        var board = await conn.QueryFirstOrDefaultAsync<Board>(@"
            select * from board where board_id = @board_id
        ", new { board_id });

        return board;
    }

    public async Task<List<Board>> GetAll(int user_id)
    {
        using var conn = _context.CreateConnection();
        return (await conn.QueryAsync<Board>(@"
            select b.board_id, b.first_name, b.last_name, b.pexels_photo_id, b.title, b.date_created, count(p.board_id) as num_posts
            from board b
                left join post p on p.board_id = b.board_id
            where user_id = @user_id
            group by b.board_id, b.first_name, b.last_name, b.pexels_photo_id, b.title, b.date_created
            order by date_created desc
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
