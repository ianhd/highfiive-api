namespace Api.Models;

public class Board
{
    public int board_id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string title { get; set; }
    public int pexels_photo_id { get; set; }
    public int occasion_id { get; set; }
    public DateTime date_created { get; set; }
}
