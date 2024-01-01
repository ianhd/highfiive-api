namespace Api.Models.Api;

public record GoogleUser(
    string name, 
    string picture, 
    string email, 
    string given_name, 
    string family_name,
    string locale
);
