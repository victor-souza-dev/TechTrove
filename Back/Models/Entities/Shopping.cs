namespace Back.Models.Entities;

public class Shopping
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public Shopping(Guid productId, Guid userId)
    {
        ProductId = productId;
        UserId = userId;
    }
}