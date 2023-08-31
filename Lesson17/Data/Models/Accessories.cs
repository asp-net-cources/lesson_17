namespace Lesson17.Data.Models;

public class Accessories : Product
{
    public new ProductType ProductType { get; } = ProductType.Accessories;
}
