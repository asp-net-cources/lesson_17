// using Lesson17.Data;
// using Lesson17.Data.EF;
// using Lesson17.Models;
// using Microsoft.AspNetCore.Mvc;
//
// namespace Lesson17.Controllers;
//
// [Route("[controller]")]
// public class ProductController : ControllerBase
// {
//     public readonly IDataContext context = new EFDataContext();
//
//     [HttpGet("{id}")]
//     public ProductModel? GetProduct([FromRoute]int id)
//     {
//         var totalPrice = context.SelectProducts();
//
//         var dbProduct =  context.SelectProducts().FirstOrDefault(product => product.Id == id);
//
//         ProductModel product = dbProduct.ProductType switch {
//             Data.Models.ProductType.Accessories => new AccessoriesModel(),
//             Data.Models.ProductType.Book => new BookModel(),
//             Data.Models.ProductType.Food => new FoodModel()
//         };
//
//         product.Id = product.Id;
//         product.Name = product.Name;
//         product.Description = product.Description;
//         product.Price = product.Price;
//
//         return product;
//     }
//     
//     [HttpPost("{id}")]
//     public ProductModel? UpdateProduct([FromRoute]int id, [FromBody] ProductModel updatedProduct)
//     {
//         updatedProduct.Id = id;
//         var updateResult = context.UpdateProduct(new Data.Models.Product() {
//             Id = updatedProduct.Id,
//             Name = updatedProduct.Name,
//             Description = updatedProduct.Description,
//             Price = updatedProduct.Price,
//             ProductType = updatedProduct.ProductType
//         });
//         return updateResult == 0 ? null : GetProduct(id);
//     }
//
//     [HttpPost("create-product")]
//     public ProductModel? CreateProduct([FromBody] ProductModel createdProduct) {
//         var insertResult = context.InsertProduct(new Data.Models.Product() {
//             Id = createdProduct.Id,
//             Name = createdProduct.Name,
//             Description = createdProduct.Description,
//             Price = createdProduct.Price,
//             ProductType = createdProduct.ProductType
//         });
//         return insertResult == 0 ? null : createdProduct;
//     }
// }