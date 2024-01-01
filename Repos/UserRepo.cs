using Api.Models;
using Api.Models.Api;
using Api.Services.ThirdParty;
using Dapper;
using Microsoft.AspNetCore.Identity;

namespace Api.Repos;

public class UserRepo : _BaseRepo
{
    public UserRepo(DapperContext context) : base(context) { }

    public async Task<User> Save(GoogleUser googleUser)
    {
        using var conn = _context.CreateConnection();
        
        var upsertSql = @"
            insert into user
                (email, picture, name, given_name, family_name, locale)
            values (@email, @picture, @name, @given_name, @family_name, @locale)  
            on duplicate key update 
                email = @email,
                picture = @picture,
                name = @name,
                given_name = @given_name,
                family_name = @family_name,
                locale = @locale,
                date_modified = CURRENT_TIMESTAMP()
        ";
        await conn.ExecuteAsync(upsertSql, googleUser);

        var selectSql = @"
            select user_id, email, picture, name
            from user
            where email = @email
        ";
        return await conn.QueryFirstOrDefaultAsync<User>(selectSql, googleUser);
    }
}
