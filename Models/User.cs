namespace Api.Models;

public class User
{
    public string user_id { get; set; }
    public string email { get; set; }
    public string picture { get; set; }
    public string name { get; set; }
    public DateTime date_created { get; set; }
    public DateTime? date_modified { get; set; }
    public string given_name { get; set; }
    public string family_name { get; set; }
    public string locale { get; set; }
}
