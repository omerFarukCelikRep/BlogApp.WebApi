namespace BlogApp.MVCUI.Models.Users;

public class UserMainSliderVM
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public List<string> Topics { get; set; }
}
