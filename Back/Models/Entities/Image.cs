namespace Back.Models.Entities;

public class Image
{
    public Guid Id { get; private set; }
    public string Src { get; private set; }
    public Guid UserId { get; set; }

    public Image(string src, Guid userId)
    {
        Src = src;
        UserId = userId;
    }
}
