using Lesson17.Data.Models;

namespace Lesson17.Models;

public class BookModel : ProductModel
{
    public new ProductType ProductType { get; } = ProductType.Book;
}
