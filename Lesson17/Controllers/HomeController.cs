// using System.Collections.Generic;
// using System.Diagnostics;
// using Lesson17.Data;
// using Lesson17.Data.EF;
// using Lesson17.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
//
// namespace Lesson17.Controllers;
//
// public class HomeController : Controller
// {
//     private readonly ILogger<HomeController> _logger;
//
//     // public IDataContext dataContext = new EFDataContext();
//
//     public HomeController(ILogger<HomeController> logger)
//     {
//         _logger = logger;
//     }
//
//     public IActionResult Index(string firstname, string lastname, string gender)
//     {
//         var model = new IndexModel {
//             Products = dataContext.SelectProducts().Where(product => product != null).Cast<ProductModel>().ToList()
//         };
//         return View(model);
//     }
//
//     [HttpPost("create-product")]
//     public IActionResult CreateProduct([FromForm]ProductModel newProduct)
//     {
//         dataContext.InsertProduct(new Data.Models.Product() {
//             Id = newProduct.Id,
//             Name = newProduct.Name,
//             Description = newProduct.Description,
//             Price = newProduct.Price,
//             ProductType = newProduct.ProductType
//         });
//         return RedirectToAction("Index");
//     }
//
//     public IActionResult Privacy()
//     {
//         return View();
//     }
//
//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }