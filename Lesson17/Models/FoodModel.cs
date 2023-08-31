using Lesson17.Data.Models;

namespace Lesson17.Models;

public class FoodModel : ProductModel
{
    public new ProductType ProductType { get; } = ProductType.Food;
}
